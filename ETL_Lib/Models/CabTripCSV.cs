using CsvHelper.Configuration.Attributes;

namespace ETL_Lib.Models
{
    internal class CabTripCSV
    {
        [Name("VendorID")]
        public int VendorID { get; set; }

        [Name("tpep_pickup_datetime")]
        public DateTime PickupDateTime { get; set; }

        [Name("tpep_dropoff_datetime")]
        public DateTime DropoffDateTime { get; set; }

        [Name("passenger_count")]
        public int PassengerCount { get; set; }

        [Name("trip_distance")]
        public double TripDistance { get; set; }

        [Name("RatecodeID")]
        public int RatecodeID { get; set; }

        [Name("store_and_fwd_flag")]
        public string StoreAndFwdFlag { get; set; }

        [Name("PULocationID")]
        public int PULocationID { get; set; }

        [Name("DOLocationID")]
        public int DOLocationID { get; set; }

        [Name("payment_type")]
        public int PaymentType { get; set; }

        [Name("fare_amount")]
        public double FareAmount { get; set; }

        [Name("extra")]
        public double Extra { get; set; }

        [Name("mta_tax")]
        public double MTATax { get; set; }

        [Name("tip_amount")]
        public double TipAmount { get; set; }

        [Name("tolls_amount")]
        public double TollsAmount { get; set; }

        [Name("improvement_surcharge")]
        public double ImprovementSurcharge { get; set; }

        [Name("total_amount")]
        public double TotalAmount { get; set; }

        [Name("congestion_surcharge")]
        public double CongestionSurcharge { get; set; }
    }
}
