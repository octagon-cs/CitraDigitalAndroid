using System;

namespace CitraDigitalAndroid.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string NPWP { get; set; }
        public string Logo { get; set; }
        public int UserId { get; set; }

    }
}