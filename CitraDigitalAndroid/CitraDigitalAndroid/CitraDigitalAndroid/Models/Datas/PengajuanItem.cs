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
                if (Persetujuans.Where(xxx => xxx.ApprovedBy == UserType.Administrator).FirstOrDefault() != null)
                {
                    return StatusPersetujuan.Complete;
                }

                if (Persetujuans.Where(xxx => xxx.ApprovedBy == UserType.Manager).FirstOrDefault() != null)
                {
                    return StatusPersetujuan.Approved;
                }

                if (Persetujuans.Where(xx => xx.StatusPersetujuan == StatusPersetujuan.Reject).Count() > 0)
                    return StatusPersetujuan.Reject;

                return StatusPersetujuan.Proccess;
            }
        }

        public UserType NextApprove
        {
            get
            {
                if (Persetujuans == null || Persetujuans.Count <= 0)
                    return UserType.Approval1;

                var lastPersetujuan = Persetujuans.Last();

                if (lastPersetujuan.StatusPersetujuan == StatusPersetujuan.Reject)
                    return UserType.Company;

                if (lastPersetujuan.ApprovedBy == UserType.Approval1)
                    return UserType.HSE;

                if (lastPersetujuan.ApprovedBy == UserType.HSE)
                    return UserType.Manager;

                if (lastPersetujuan.ApprovedBy == UserType.Manager)
                    return UserType.Administrator;

                if (lastPersetujuan.ApprovedBy == UserType.Company)
                {
                    var index = Persetujuans.IndexOf(lastPersetujuan);
                    var lasApproved = Persetujuans[index - 1];
                    return lasApproved.ApprovedBy;
                }
                return UserType.None;

            }
        }
    }
}