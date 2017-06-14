using MVVM_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class MitarbeiterViewModel : ObservableObject
    {
        private MitarbeiterModel _mitarbeiter;

        public MitarbeiterViewModel()
        {
            _mitarbeiter = new MitarbeiterModel();
        }

        public MitarbeiterModel Mitarbeiter
        {
            get { return _mitarbeiter; }
            set
            {
                _mitarbeiter = value;
                RaisePropertyChanged("ID");
                RaisePropertyChanged("Code");
                RaisePropertyChanged("Bezeichnung");
            }
        }

        public int ID
        {
            get { return _mitarbeiter.ID; }
            set
            {
                _mitarbeiter.ID = value;
                RaisePropertyChanged("ID");
            }
        }

        public string Code
        {
            get { return _mitarbeiter.Code; }
            set
            {
                _mitarbeiter.Code = value;
                RaisePropertyChanged("Code");
            }
        }

        public string Bezeichnung
        {
            get { return _mitarbeiter.Bezeichnung; }
            set
            {
                _mitarbeiter.Bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }
    }
}
