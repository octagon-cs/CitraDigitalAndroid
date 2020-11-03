using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitraDigitalAndroid.Models
{
  public  class GroupPemeriksaan :BaseNotify
    {
        public int PemeriksaanId { get; set; }

        public string Name { get; set; }

        public List<HasilPemeriksaan> Items { get; set; } = new List<HasilPemeriksaan>();


        [JsonIgnore]
        public bool Complete { 
            get {
                var dataFalse = Items.Where(x => !x.Hasil).FirstOrDefault();
                return dataFalse == null ? true : false;
            }
            set
            {
                SetProperty(ref _complete, value);
            }
        }

        private bool _complete;

    }
}
