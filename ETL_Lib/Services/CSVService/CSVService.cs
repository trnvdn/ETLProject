using CsvHelper;
using ETL_Lib.Configations;
using ETL_Lib.Coverters;
using ETL_Lib.Models;
using System.Globalization;

namespace ETL_Lib.Services.CSVService
{
    public class CSVService
    {
        private readonly HttpClient _httpClient;
        private readonly Config? _appSettings;
        private readonly string _fileName = "CabTripData.csv";

        public CSVService()
        {
            _httpClient = new HttpClient();
            _appSettings = ConfigurationManager.Configurations;
        }

        public void DownloadCSV()
        {
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), _fileName)))
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), _fileName));
            }
            var response = _httpClient.GetAsync(_appSettings.CSVSourceLink).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            File.WriteAllText(_fileName, content);
        }

        public async Task<List<CabTripCSV>>? ParseCSVAsync()
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

                return records;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                return null;
            }
        }

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
    }
}
