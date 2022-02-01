using Microsoft.Extensions.Configuration;

namespace Aircon.Business.Helper
{
    public interface IConfigValueProvider
    {
        string GetValue(string name);
    }

    public class AppConfigValueProvider : IConfigValueProvider
    {
        private readonly IConfiguration Config;
        public AppConfigValueProvider(IConfiguration config)
        {
            Config = config;
        }
        public string GetValue(string name)
        {
            return Config[name];
        }
    }

}
