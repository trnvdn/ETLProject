using AutoMapper;
using ETL_Lib.Models;
using Newtonsoft.Json;

namespace ETL_Lib.Configations
{
    public static class ConfigurationManager
    {
        private static readonly Lazy<Config?> _lazyConfig = new Lazy<Config?>(loadConfiguration);
        private static readonly Lazy<IMapper> _lazyMapper = new Lazy<IMapper>(createMapperInstance);

        public static Config? Instance => _lazyConfig.Value;
        public static IMapper Mapper => _lazyMapper.Value;

        private static Config? loadConfiguration()
        {
            string configFilePath = "config.json";
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Configuration file '{configFilePath}' not found.");
            }

            string json = File.ReadAllText(configFilePath);
            return JsonConvert.DeserializeObject<Config>(json);
        }

        private static IMapper createMapperInstance()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CabTripCSV, CabTripDto>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
