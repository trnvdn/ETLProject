using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace ETL_Lib.Coverters
{
    /// <summary>
    /// Custom converter for int type
    /// </summary>
    public class CustomIntConverter : DefaultTypeConverter
    {
        /// <summary>
        /// Convert string to int
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
