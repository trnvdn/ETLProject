using AutoMapper;
using ETL_Lib.Models;
using Newtonsoft.Json;

namespace ETL_Lib.Configations
{
    /// <summary>
    /// Configuration manager class. Loads the configuration settings from the config.json file.
    /// </summary>
    public static class ConfigurationManager
    {
        private static readonly Lazy<Config?> _lazyConfig = new Lazy<Config?>(loadConfiguration);
        private static readonly Lazy<IMapper> _lazyMapper = new Lazy<IMapper>(createMapperInstance);

        private static readonly string _configFilePath = "config.json";

        /// <summary>
        /// Property to access the configuration settings.
        /// </summary>
        public static Config? Configurations => _lazyConfig.Value;
        /// <summary>
        /// Property to access the AutoMapper instance.
        /// </summary>
        public static IMapper Mapper => _lazyMapper.Value;

        private static Config? loadConfiguration()
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configFilePath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Configuration file '{_configFilePath}' not found.");
            }

            string json = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<Config>(json);
        }

        private static IMapper createMapperInstance()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CabTripCSV, CabTripDto>().ReverseMap();
                cfg.CreateMap<CabTrip, CabTripDto>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
