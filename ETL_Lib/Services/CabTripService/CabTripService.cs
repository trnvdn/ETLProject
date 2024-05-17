using ETL_Lib.Models;
using ETL_Lib.Repository;

namespace ETL_Lib.Services.CabTripService
{
    public class CabTripService : ICabTripService
    {
        private readonly ICabTripRepository _cabTripRepository;

        public CabTripService()
        {
            _cabTripRepository = new CabTripRepository();
        }

        public async Task<List<CabTripDto>> Retrieve()
        {
            return await _cabTripRepository.Retrieve();
        }
        public async Task<List<CabTripDto>> RetrieveByPULocation(int puLocationID)
        {
            return await _cabTripRepository.Retrieve(puLocationID);
        }

        public async Task<int> RetrieveHighestAverageTipLocation()
        {
            return await _cabTripRepository.RetrieveHighestAverageTipLocation();
        }

        public async Task<List<CabTripDto>> RetrieveLongestFaresByDistance(int limit = 100)
        {
            return await _cabTripRepository.RetrieveLongestFaresByDistance(limit);
        }

        public async Task<List<CabTripDto>> RetrieveLongestFaresByTimeSpent(int limit = 100)
        {
            return await _cabTripRepository.RetrieveLongestFaresByTimeSpent(limit);
        }
    }
}
