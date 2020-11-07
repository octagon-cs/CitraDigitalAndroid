using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CitraDigitalAndroid.Models
{
    public class Truck 
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Merk { get; set; }
        public int CarCreated { get; set; }
        public string TruckType { get; set; }
        public string DriverName { get; set; }
        public DataDocument DriverIDCard { get; set; }
        public DataDocument DriverLicense { get; set; }
        public string DriverPhoto { get; set; }
        public string AssdriverName { get; set; }
        public DataDocument AssdriverIDCard { get; set; }
        public DataDocument AssdriverLicense { get; set; }
        public string AssdriverPhoto { get; set; }
        public DataDocument KeurDLLAJR { get; set; }
        public DataDocument VehicleRegistration { get; set; }
        public int CompanyId { get; set; }
        public CompanyProfile Company { get; set; }
        public List<KIM> Kims { get; set; } = new List<KIM>();

        public KIM KIM
        {
            get
            {
                if (Kims == null || Kims.Count <= 0)
                    return null;
                return Kims.Last();
            }
        }

    }

   
    public class DataDocument
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? Berlaku { get; set; }
        public DateTime? Hingga { get; set; }
        public string Document { get; set; }
    }
}