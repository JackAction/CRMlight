using System;
using MVVM_Framework;

namespace CRMLight
{
    public class PendenzViewModel : ObservableObject
    {
        private PendenzModel _pendenz;

        public PendenzViewModel()
        {
            _pendenz = new PendenzModel();
        }

        public PendenzModel Pendenz
        {
            get { return _pendenz; }
            set
            {
                _pendenz = value;
                RaisePropertyChanged("PendenzID");
                RaisePropertyChanged("KontaktID");
                RaisePropertyChanged("Termin");
                RaisePropertyChanged("MitarbeiterID");
                RaisePropertyChanged("Beschreibung");
                RaisePropertyChanged("Quelle");
                RaisePropertyChanged("Erledigt");
            }
        }

        public int PendenzID
        {
            get { return _pendenz.PendenzID; }
            set
            {
                _pendenz.PendenzID = value;
                RaisePropertyChanged("PendenzID");
            }
        }

        public int KontaktID
        {
            get { return _pendenz.KontaktID; }
            set
            {
                _pendenz.KontaktID = value;
                RaisePropertyChanged("KontaktID");
            }
        }

        public DateTime Termin
        {
            get { return _pendenz.Termin; }
            set
            {
                _pendenz.Termin = value;
                RaisePropertyChanged("Termin");
            }
        }

        public int MitarbeiterID
        {
            get { return _pendenz.MitarbeiterID; }
            set
            {
                _pendenz.MitarbeiterID = value;
                RaisePropertyChanged("MitarbeiterID");
            }
        }

        public string Titel
        {
            get { return _pendenz.Titel; }
            set
            {
                _pendenz.Titel = value;
                RaisePropertyChanged("Titel");
            }
        }

        public string Beschreibung
        {
            get { return _pendenz.Beschreibung; }
            set
            {
                _pendenz.Beschreibung = value;
                RaisePropertyChanged("Beschreibung");
            }
        }
        public string Quelle
        {
            get { return _pendenz.Quelle; }
            set
            {
                _pendenz.Quelle = value;
                RaisePropertyChanged("Quelle");
            }
        }

        public bool Erledigt
        {
            get { return _pendenz.Erledigt; }
            set
            {
                _pendenz.Erledigt = value;
                RaisePropertyChanged("Erledigt");
            }
        }
    }
}
