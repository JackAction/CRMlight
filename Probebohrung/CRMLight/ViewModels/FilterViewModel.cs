using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;

namespace CRMLight
{
    public class FilterViewModel: ObservableObject
    {
        private FilterModel _filter;

        public FilterViewModel()
        {
            _filter = new FilterModel();
        }

        public FilterModel Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                //RaisePropertyChanged("ID");
                //RaisePropertyChanged("Code");
                //RaisePropertyChanged("Bezeichnung");
            }
        }

        public int ID
        {
            get { return _filter.ID; }
            set
            {
                _filter.ID = value;
                //RaisePropertyChanged("ID");
            }
        }

        public string Code
        {
            get { return _filter.Code; }
            set
            {
                _filter.Code = value;
                //RaisePropertyChanged("Code");
            }
        }

        public string Bezeichnung
        {
            get { return _filter.Bezeichnung; }
            set
            {
                _filter.Bezeichnung = value;
                //RaisePropertyChanged("Bezeichnung");
            }
        }
    }
}
