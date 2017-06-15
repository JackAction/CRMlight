using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class KontaktModel
    {
        public int KontaktID { get; set; }
        public string Firma { get; set; }
        public string Vorname { get; set; }
        public string Name { get; set; }
        public string Strasse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }
        public string Mobile { get; set; }
        public string eMail { get; set; }
        public string Bemerkung { get; set; }
        public string Interessen { get; set; }
        public DateTime AktionTermin { get; set; }
        public string AktionBeschreibung { get; set; }
        public int AktionMitarbeiterID { get; set; }
    }
}
