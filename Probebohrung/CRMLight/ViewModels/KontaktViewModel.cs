using MVVM_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class KontaktViewModel : ObservableObject
    {
        private KontaktModel _kontakt;

        public KontaktViewModel()
        {
            _kontakt = new KontaktModel();
        }

        public KontaktModel Kontakt
        {
            get { return _kontakt; }
            set
            {
                _kontakt = value;
                RaisePropertyChanged("KontaktID");
                RaisePropertyChanged("Firma");
                RaisePropertyChanged("Vorname");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("Strasse");
                RaisePropertyChanged("PLZ");
                RaisePropertyChanged("Ort");
                RaisePropertyChanged("Telefon");
                RaisePropertyChanged("Mobile");
                RaisePropertyChanged("eMail");
                RaisePropertyChanged("Interessen");
                RaisePropertyChanged("Bemerkungen");
                RaisePropertyChanged("AktionTermin");
                RaisePropertyChanged("AktionBeschreibung");
                RaisePropertyChanged("AktionMitarbeiterID");
            }
        }

        public int KontaktID
        {
            get { return _kontakt.KontaktID; }
            set
            {
                _kontakt.KontaktID = value;
                RaisePropertyChanged("KontaktID");
            }
        }

        public string Firma
        {
            get { return _kontakt.Firma; }
            set
            {
                _kontakt.Firma = value;
                RaisePropertyChanged("Firma");
            }
        }

        public string Vorname
        {
            get { return _kontakt.Vorname; }
            set
            {
                _kontakt.Vorname = value;
                RaisePropertyChanged("Vorname");
            }
        }

        public string Name
        {
            get { return _kontakt.Name; }
            set
            {
                _kontakt.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Strasse
        {
            get { return _kontakt.Strasse; }
            set
            {
                _kontakt.Strasse = value;
                RaisePropertyChanged("Strasse");
            }
        }

        public string PLZ
        {
            get { return _kontakt.PLZ; }
            set
            {
                _kontakt.PLZ = value;
                RaisePropertyChanged("PLZ");
            }
        }

        public string Ort
        {
            get { return _kontakt.Ort; }
            set
            {
                _kontakt.Ort = value;
                RaisePropertyChanged("Ort");
            }
        }

        public string Telefon
        {
            get { return _kontakt.Telefon; }
            set
            {
                _kontakt.Telefon = value;
                RaisePropertyChanged("Telefon");
            }
        }

        public string Mobile
        {
            get { return _kontakt.Mobile; }
            set
            {
                _kontakt.Mobile = value;
                RaisePropertyChanged("Mobile");
            }
        }

        public string eMail
        {
            get { return _kontakt.eMail; }
            set
            {
                _kontakt.eMail = value;
                RaisePropertyChanged("eMail");
            }
        }

        public string Interessen
        {
            get { return _kontakt.Interessen; }
            set
            {
                _kontakt.Interessen = value;
                RaisePropertyChanged("Interessen");
            }
        }

        public string Bemerkungen
        {
            get { return _kontakt.Bemerkungen; }
            set
            {
                _kontakt.Bemerkungen = value;
                RaisePropertyChanged("Bemerkungen");
            }
        }

        public DateTime AktionTermin
        {
            get { return _kontakt.AktionTermin; }
            set
            {
                _kontakt.AktionTermin = value;
                RaisePropertyChanged("AktionTermin");
            }
        }

        public string AktionBeschreibung
        {
            get { return _kontakt.AktionBeschreibung; }
            set
            {
                _kontakt.AktionBeschreibung = value;
                RaisePropertyChanged("AktionBeschreibung");
            }
        }

        public int AktionMitarbeiterID
        {
            get { return _kontakt.AktionMitarbeiterID; }
            set
            {
                _kontakt.AktionMitarbeiterID = value;
                RaisePropertyChanged("AktionMitarbeiterID");
            }
        }
    }
}
