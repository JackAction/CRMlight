using MVVM_Framework;

namespace CRMLight
{
    public class FilterViewModel: ObservableObject
    {
        #region ATTRIBUTS

        private FilterModel _filter;

        #endregion

        #region CONSTRUCTORS

        public FilterViewModel()
        {
            _filter = new FilterModel();
        }

        #endregion

        #region PROPERTIES

        public FilterModel Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
            }
        }

        public int ID
        {
            get { return _filter.ID; }
            set
            {
                _filter.ID = value;
            }
        }

        public string Code
        {
            get { return _filter.Code; }
            set
            {
                _filter.Code = value;
            }
        }

        public string Bezeichnung
        {
            get { return _filter.Bezeichnung; }
            set
            {
                _filter.Bezeichnung = value;
            }
        }

        #endregion
    }
}
