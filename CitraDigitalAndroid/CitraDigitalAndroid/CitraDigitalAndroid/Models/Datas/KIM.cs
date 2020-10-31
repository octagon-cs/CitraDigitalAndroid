using System;

namespace CitraDigitalAndroid.Models
{
    public class KIM
    {
        public int Id { get; set; }

        public int TruckId { get; set; }

        public Truck Truck { get; set; }

        public string KimNumber { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public ExpireStatus Expired { get; set; }
    }
}