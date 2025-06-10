using Antlers.DraftKings.Models;
using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Market
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("displayOrder")]
        public int? DisplayOrder { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("display")]
        public bool? Display { get; set; }
        [JsonProperty("tradedInPlay")]
        public bool? TradedInPlay { get; set; }
        [JsonProperty("templateId")]
        public string? TemplateId { get; set; }
        [JsonProperty("templateName")]
        public string? TemplateName { get; set; }
        [JsonProperty("templateType")]
        public string? TemplateType { get; set; }
        [JsonProperty("spOffered")]
        public bool? SpOffered { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("DisplayName")]
        public string? DisplayName { get; set; }
        [JsonProperty("selections")]
        public Selection[]? Selections { get; set; }
        [JsonProperty("placeholder")]
        public bool? Placeholder { get; set; }
        [JsonProperty("keyMarket")]
        public bool? KeyMarket { get; set; }
        [JsonProperty("sixPackView")]
        public bool? SixPackView { get; set; }
        [JsonProperty("cashoutAvailable")]
        public bool? CashoutAvailable { get; set; }
        [JsonProperty("byoEligible")]
        public bool? ByoEligible { get; set; }
        [JsonProperty("movingLines")]
        public string? MovingLines { get; set; }
        [JsonProperty("hasMovingLines")]
        public bool? HasMovingLines { get; set; }
        [JsonProperty("collectionDisplayOrder")]
        public int? CollectionDisplayOrder { get; set; }
        [JsonProperty("marketTemplateDisplayOrder")]
        public int? MarketTemplateDisplayOrder { get; set; }
        [JsonProperty("gtpMarketClass")]
        public string? GtpMarketClass { get; set; }

    }
}
