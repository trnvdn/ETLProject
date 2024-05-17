using ETL_Lib.Configations;
using ETL_Lib.Models;
using ETL_Lib.Services.CSVService;

namespace ETL_Executable
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var mapper = ConfigurationManager.Mapper;
            var service = new CSVService();

            var records = service.ParseCSVAsync().GetAwaiter().GetResult();
            var cabTripDtos = mapper.Map<List<CabTripDto>>(records);
        }
    }
}
