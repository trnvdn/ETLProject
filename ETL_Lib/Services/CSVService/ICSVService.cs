using ETL_Lib.Models;

namespace ETL_Lib.Services.CSVService
{
    public interface ICSVService
    {
        /// <summary>
        /// Downloads the CSV file from the URL, based on the configuration
        /// </summary>
        /// <returns></returns>
        Task DownloadCSVAsync();

        /// <summary>
        /// Parses the CSV file and returns a list of CabTripDto
        /// </summary>
        /// <returns></returns>
        Task<List<CabTripDto>>? ParseCSVAsync();

        /// <summary>
        /// Inserts the results into the database
        /// </summary>
        /// <param name="cabTrips"></param>
        /// <returns></returns>
        Task<bool> InsertResultsAsync(List<CabTripDto>? cabTrips);
    }
}
