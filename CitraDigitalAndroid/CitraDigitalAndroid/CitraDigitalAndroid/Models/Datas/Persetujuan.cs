using System;

namespace CitraDigitalAndroid.Models
{
    public class Persetujuan
    {
        public int Id { get; set; }

        public int PengajuanItemId { get; set; }

        public int UserId { get; set; }
        public StatusPersetujuan StatusPersetujuan { get; set; }

        public UserType ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

    }
}