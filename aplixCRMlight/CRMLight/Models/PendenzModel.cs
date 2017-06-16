using System;

namespace CRMLight
{
    public class PendenzModel
    {
        public int PendenzID { get; set; }
        public int KontaktID { get; set; }
        public DateTime Termin { get; set; }
        public int MitarbeiterID { get; set; }
        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public string Quelle { get; set; }
        public bool Erledigt { get; set; }

    }
}
