using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Antlers.DraftKings.Models;

public sealed class EntityCollectionConverter<T> : JsonConverter<T[]> where T : class
{
    public override T[] ReadJson(
        JsonReader reader,
        Type objectType,
        T[]? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        if (token.Type == JTokenType.Null)
        {
            return [];
        }

        if (token is JArray array)
        {
            return array.ToObject<T[]>(serializer) ?? [];
        }

        if (token is JObject obj)
        {
            if (obj.Property("id", StringComparison.OrdinalIgnoreCase) is not null)
            {
                return [obj.ToObject<T>(serializer)!];
            }

            return obj.Properties()
                .Select(property => property.Value)
                .OfType<JObject>()
                .Select(value => value.ToObject<T>(serializer)!)
                .ToArray();
        }

        throw new JsonSerializationException(
            $"Expected an array or object for a collection of {typeof(T).Name}.");
    }

    public override void WriteJson(JsonWriter writer, T[]? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
