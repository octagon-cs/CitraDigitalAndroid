using System;
using System.Collections.Generic;
using System.Text;

namespace CitraDigitalAndroid.Models
{
    public class TruckIncomming
    {
        public int Id { get; set; }

        public int TruckId { get; set; }

        public bool Status { get; set; }

        public DateTime Created { get; set; }

        public List<IncommingNote> Notes { get; set; }
    }
   
}
