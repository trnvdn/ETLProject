using Newtonsoft.Json;

namespace ETL_Lib.Configations
{
    public static class ConfigurationManager
    {
        private static readonly Lazy<Config> _lazyConfig = new Lazy<Config>(loadConfiguration);

        public static Config Instance => _lazyConfig.Value;

        private static Config loadConfiguration()
        {
            string configFilePath = "config.json";
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Configuration file '{configFilePath}' not found.");
            }

            string json = File.ReadAllText(configFilePath);
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
