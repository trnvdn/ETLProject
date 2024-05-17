using ETL_Lib.Services.CabTripService;
using ETL_Lib.Services.CSVService;

namespace ETL_Executable
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            #region Download and parse CSV file, then insert results into the database
            var csvService = new CSVService();

            await csvService.DownloadCSVAsync();
            var cabTrips = await csvService.ParseCSVAsync();
            await csvService.InsertResultsAsync(cabTrips);
            #endregion

            #region Retrieve all trips and statistics
            var cabService = new CabTripService();

            var trips = await cabService.Retrieve();

            var locationID = 90;
            var tripByLocation = await cabService.RetrieveByPULocation(locationID);
            var highestAverageTipLocation = await cabService.RetrieveHighestAverageTipLocation();

            //You alse can introduce a limit to the number of results
            //By default, it retrieves the top 100 longest fares
            var longestFaresByDistance = await cabService.RetrieveLongestFaresByDistance(/*200*/);
            var longestFaresByTimeSpent = await cabService.RetrieveLongestFaresByTimeSpent(/*200*/);
            #endregion
        }
    }
}
