using AutoMapper;
using ETL_Lib.Configations;
using ETL_Lib.Database;
using ETL_Lib.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace ETL_Lib.Repository
{
    internal class CabTripRepository : ICabTripRepository
    {
        private readonly IMapper _mapper;

        public CabTripRepository()
        {
            _mapper = ConfigurationManager.Mapper;
        }

        public async Task<bool> Insert(List<CabTripDto> cabTrips)
        {
            if (cabTrips == null)
            {
                return false;
            }

            var cabTripEntities = _mapper.Map<List<CabTrip>>(cabTrips);

            using (var context = new AppDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await context.CabTrips.AddRangeAsync(cabTripEntities);
                        var result = await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();

                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
        }

        public async Task<List<CabTripDto>> Retrieve()
        {
            using(var context = new AppDbContext())
            {
                var cabTrips = await context.CabTrips.ToListAsync();
                return _mapper.Map<List<CabTripDto>>(cabTrips);
            }
        }

        public async Task<List<CabTripDto>> Retrieve(int puLocationID)
        {
            using(var context = new AppDbContext())
            {
                var cabTrips = await context.CabTrips.Where(x=>x.PULocationID == puLocationID).ToListAsync();
                return _mapper.Map<List<CabTripDto>>(cabTrips);
            }
        }

        public async Task<int> RetrieveHighestAverageTipLocation()
        {
            using (var context = new AppDbContext())
            {
                var result = await context.Database
                    .SqlQuery<int>(@$"SELECT TOP 1 PULocationID AS Value
                             FROM [dbo].[CabTrips]
                             GROUP BY PULocationID
                             ORDER BY AVG([TipAmount]) DESC")
                    .ToListAsync();

                return result.FirstOrDefault();
            }
        }

        public async Task<List<CabTripDto>> RetrieveLongestFaresByDistance(int limit = 100)
        {
            using (var context = new AppDbContext())
            {
                var result = await context.CabTrips
                    .FromSqlRaw($@"SELECT TOP {limit} * FROM [dbo].[CabTrips] ORDER BY TripDistance DESC")
                    .ToListAsync();

                return _mapper.Map<List<CabTripDto>>(result);
            }
        }

        public async Task<List<CabTripDto>> RetrieveLongestFaresByTimeSpent(int limit = 100)
        {
            using (var context = new AppDbContext())
            {
                var result = await context.CabTrips
                    .FromSqlRaw($@"SELECT TOP {limit} *, 
                           DATEDIFF(MINUTE, PickupDateTime, DropoffDateTime) AS TravelTimeInMinutes 
                           FROM [dbo].[CabTrips]
                           ORDER BY TravelTimeInMinutes DESC")
                    .ToListAsync();

                return _mapper.Map<List<CabTripDto>>(result);
            }
        }
    }
}
