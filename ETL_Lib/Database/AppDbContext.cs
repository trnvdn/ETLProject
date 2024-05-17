using ETL_Lib.Configations;
using ETL_Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL_Lib.Database
{
    internal class AppDbContext : DbContext
    {
        public DbSet<CabTrip> CabTrips { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.Configurations.DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indexes for the CabTrip table to improve query performance
            modelBuilder.Entity<CabTrip>()
                .HasIndex(c => c.PULocationID)
                .HasDatabaseName("IX_CabTrip_PULocationID");

            modelBuilder.Entity<CabTrip>()
                .HasIndex(c => c.DropoffDateTime)
                .HasDatabaseName("IX_CabTrip_DropoffDateTime");

            modelBuilder.Entity<CabTrip>()
                .HasIndex(c => c.TripDistance)
                .HasDatabaseName("IX_CabTrip_TripDistance");
        }
    }
}
