namespace ETL_Lib.Models
{
    public class CabTripDto
    {
        public Guid TripID { get; set; }
        public DateTime PickupDateTime { get; set; }

        public DateTime DropoffDateTime { get; set; }

        public int PassengerCount { get; set; }

        public double TripDistance { get; set; }

        public string StoreAndFwdFlag { get; set; }

        public int PULocationID { get; set; }

        public int DOLocationID { get; set; }

        public double FareAmount { get; set; }

        public double TipAmount { get; set; }
    }
}
