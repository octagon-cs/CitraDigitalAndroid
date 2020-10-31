using System.Collections.Generic;
using System.Linq;

namespace CitraDigitalAndroid.Models
{
    public class PengajuanItem
    {
        public int Id { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }

        public AttackStatus AttackStatus { get; set; }
        public int PengajuanId { get; set; }

        public Pengajuan Pengajuan { get; set; }

        public List<Persetujuan> Persetujuans { get; set; } = new List<Persetujuan>();
        public List<HasilPemeriksaan> HasilPemeriksaan { get; set; } = new List<HasilPemeriksaan>();

        public StatusPersetujuan Status
        {
            get
            {
                if (Persetujuans.Where(xx => xx.StatusPersetujuan == StatusPersetujuan.Reject).Count() > 0)
                    return StatusPersetujuan.Reject;

                if (Persetujuans.Where(xx => xx.StatusPersetujuan == StatusPersetujuan.Approved).Count() >= 3)
                {
                    return StatusPersetujuan.Approved;
                }
                return StatusPersetujuan.Proccess;
            }
        }
    }
}