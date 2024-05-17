namespace ETL_Lib.Configations
{
    /// <summary>
    /// Configuration class. Stores the configuration settings for the 
    /// application such as the database connection string and the CSV source link.
    /// </summary>
    public class Config
    {
        public string? DbConnectionString { get; set; }
        public string? CSVSourceLink { get; set; }
    }
}
