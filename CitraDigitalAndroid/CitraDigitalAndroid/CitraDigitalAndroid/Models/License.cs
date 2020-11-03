using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CitraDigitalAndroid.Models
{
   public class License  :BaseNotify
    {
        private string _number;

        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number , value); }
        }


        private DateTime berlaku;

        public DateTime Berlaku
        {
            get { return berlaku; }
            set { SetProperty(ref berlaku, value); }
        }

        private DateTime hingga;

        public DateTime Hingga
        {
            get { return berlaku; }
            set { SetProperty(ref hingga, value); }
        }

        [JsonIgnore]
        public string Name { get;  set; }

        [JsonIgnore]
        public string NumberID { get; set; }

        [JsonIgnore]
        public ImageSource Photo { get; set; }
    }
}
