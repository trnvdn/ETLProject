using ETL_Lib.Models;

namespace ETL_Lib.Services.CabTripService
{
    public interface ICabTripService
    {
        /// <summary>
        /// Retrieve all cab trips
        /// </summary>
        /// <returns></returns>
        Task<List<CabTripDto>> Retrieve();

        /// <summary>
        /// Retrieve a cab trip by id
        /// </summary>
        /// <param name="id">CabID</param>
        /// <returns></returns>
        Task<List<CabTripDto>> RetrieveByPULocation(int puLocationID);

        /// <summary>
        /// Retrieve location with the highest average tip
        /// </summary>
        /// <returns></returns>
        Task<int> RetrieveHighestAverageTipLocation();

        /// <summary>
        /// Retrieve the longest fares by distance. By default, it retrieves the top 100 longest fares
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<CabTripDto>> RetrieveLongestFaresByDistance(int limit = 100);

        /// <summary>
        /// Retrieve the longest fares by time spent. By default, it retrieves the top 100 longest fares
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<CabTripDto>> RetrieveLongestFaresByTimeSpent(int limit = 100);
    }
}
