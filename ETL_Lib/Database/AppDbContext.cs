using ETL_Lib.Configations;
using Microsoft.EntityFrameworkCore;

namespace ETL_Lib.Database
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.Configurations.DbConnectionString);
        }
    }
}
