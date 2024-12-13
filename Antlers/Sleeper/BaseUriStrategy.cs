namespace Antlers.Sleeper
{
    public class SleeperAppBaseUriStrategy : IBaseUriStrategy
    {
        public string GetBaseUri()
        {
            return "https://api.sleeper.app/v1";
        }
    }
    public class SleeperWebBaseUriStrategy : IBaseUriStrategy
    {
        public string GetBaseUri()
        {
            return "https://api.sleeper.com/";
        }
    }
}
