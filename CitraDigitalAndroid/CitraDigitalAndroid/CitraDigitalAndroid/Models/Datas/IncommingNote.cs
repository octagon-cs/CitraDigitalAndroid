using System;
using System.Collections.Generic;
using System.Text;

namespace CitraDigitalAndroid.Models
{
    public class IncommingNote
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public string NoteAction { get; set; }

        public DateTime? CompensationDeadline { get; set; }
    }
}
