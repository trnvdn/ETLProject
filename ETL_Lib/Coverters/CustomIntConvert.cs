using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace ETL_Lib.Coverters
{
    public class CustomIntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            if (int.TryParse(text, out int result))
            {
                return result;
            }
            else
            {
                throw new Exception($"Error occurred `cuz of unsuccessful try for convert '{text}' in {memberMapData.Type} type.");
            }
        }
    }
}
