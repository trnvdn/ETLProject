using AutoMapper;
using CsvHelper;
using ETL_Lib.Configations;
using ETL_Lib.Coverters;
using ETL_Lib.Models;
using ETL_Lib.Repository;
using System.Globalization;

namespace ETL_Lib.Services.CSVService
{
    public class CSVService : ICSVService
    {
        private readonly HttpClient _httpClient;
        private readonly Config? _appSettings;
        private readonly IMapper _mapper;
        private readonly ICabTripRepository _repository;

        private readonly string _fileName = "CabTripData.csv";

        public CSVService()
        {
            _httpClient = new HttpClient();
            _appSettings = ConfigurationManager.Configurations;
            _mapper = ConfigurationManager.Mapper;
            _repository = new CabTripRepository();
        }

        public async Task DownloadCSVAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.CSVSourceLink);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            File.WriteAllText(_fileName, content);
        }

        public async Task<List<CabTripDto>>? ParseCSVAsync()
        {
            try
            {
                var records = new List<CabTripCSV>();
                var duplicates = new List<CabTripCSV>();

                using (var reader = new StreamReader(_fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.TypeConverterCache.AddConverter<int>(new CustomIntConverter());
                    csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats =
                        ["MM/dd/yyyy hh:mm:ss tt"];

                    var recordsWithDuplicates = new Dictionary<string, CabTripCSV>();

                    await foreach (var record in csv.GetRecordsAsync<CabTripCSV>())
                    {
                        record.StoreAndFwdFlag = record.StoreAndFwdFlag switch
                        {
                            "N" => "No",
                            "Y" => "Yes",
                            _ => record.StoreAndFwdFlag
                        };

                        trimStringProperties(record);

                        convertToUTC(record);

                        var key = $"{record.PickupDateTime}_{record.DropoffDateTime}_{record.PassengerCount}";

                        if (recordsWithDuplicates.ContainsKey(key))
                        {
                            duplicates.Add(record);
                        }
                        else
                        {
                            recordsWithDuplicates.Add(key, record);
                            records.Add(record);
                        }
                    }
                }

                writeDuplicatesToFile(duplicates);

                return _mapper.Map<List<CabTripDto>>(records);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                return null;
            }
        }
        public async Task<bool> InsertResultsAsync(List<CabTripDto>? cabTrips)
        {
            return await _repository.Insert(cabTrips);
        }

        #region Cab trip handlers
        private void trimStringProperties(CabTripCSV record)
        {
            var properties = record.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var value = (string)property.GetValue(record);
                if (value != null)
                {
                    value = value.Trim();
                    property.SetValue(record, value);
                }
            }
        }

        private void convertToUTC(CabTripCSV record)
        {
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            record.PickupDateTime = TimeZoneInfo.ConvertTimeToUtc(record.PickupDateTime, estTimeZone);
            record.DropoffDateTime = TimeZoneInfo.ConvertTimeToUtc(record.DropoffDateTime, estTimeZone);
        }

        private void writeDuplicatesToFile(List<CabTripCSV> duplicates)
        {
            var duplicatesFilePath = "duplicates.csv";
            using (var writer = new StreamWriter(duplicatesFilePath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(duplicates);
            }
        }
        #endregion
    }
}
