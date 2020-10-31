using System;
using System.Collections.Generic;

namespace CitraDigitalAndroid.Models
{
    public class Pengajuan
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public CompanyProfile Company { get; set; }
        public string LetterNumber { get; set; }

        public StatusPengajuan StatusPengajuan { get; set; }
        public DateTime Created { get; set; }
        public List<PengajuanItem> Items { get; set; }
    }
}