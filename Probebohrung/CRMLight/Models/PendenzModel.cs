using System;
using System.Data.Linq;

namespace CRMLight
{
    public class PendenzModel
    {
        public int PendenzID { get; set; }
        public int KontaktID { get; set; }
        public DateTime Termin { get; set; }
        public int MitarbeiterID { get; set; }
        public string Beschreibung { get; set; }

    }
}
