using MVVM_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CRMLight
{
    public class MainViewModel : ObservableObject
    {
        #region Listen + SelectedItems

        private readonly PendenzenRepository pendenzenRepository = new PendenzenRepository();
        ObservableCollection<PendenzViewModel> _pendenzen = new ObservableCollection<PendenzViewModel>();
        public ObservableCollection<PendenzViewModel> Pendenzen
        {
            get { return _pendenzen; }
            set { _pendenzen = value; }
        }

        private readonly MitarbeiterRepository mitarbeiterRepository = new MitarbeiterRepository();
        ObservableCollection<MitarbeiterViewModel> _mitarbeiter = new ObservableCollection<MitarbeiterViewModel>();
        public ObservableCollection<MitarbeiterViewModel> Mitarbeiter
        {
            get { return _mitarbeiter; }
            set { _mitarbeiter = value; }
        }

        private readonly KontaktRepository kontaktRepository = new KontaktRepository();
        ObservableCollection<KontaktViewModel> _kontakte = new ObservableCollection<KontaktViewModel>();
        public ObservableCollection<KontaktViewModel> Kontakte
        {
            get { return _kontakte; }
            set { _kontakte = value; }
        }

        private readonly FilterRepository filterRepository = new FilterRepository();
        ObservableCollection<FilterViewModel> _filter = new ObservableCollection<FilterViewModel>();
        public ObservableCollection<FilterViewModel> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        KontaktViewModel _selectedKontakt;
        public KontaktViewModel SelectedKontakt
        {
            get
            {
                return _selectedKontakt;
            }
            set
            {
                if (_selectedKontakt == value)
                {
                    return;
                }
                _selectedKontakt = value;
                RaisePropertyChanged("SelectedKontakt");
                LoadPendenzenFromRepository(_selectedKontakt.KontaktID);
            }
        }

        PendenzViewModel _selectedPendenz;
        public PendenzViewModel SelectedPendenz
        {
            get
            {
                return _selectedPendenz;
            }
            set
            {
                if (_selectedPendenz == value)
                {
                    return;
                }
                _selectedPendenz = value;
                RaisePropertyChanged("SelectedPendenz");
            }
        }

        FilterViewModel _selectedFilter;
        public FilterViewModel SelectedFilter
        {
            get
            {
                return _selectedFilter;
            }
            set
            {
                if (_selectedFilter == value)
                {
                    return;
                }
                _selectedFilter = value;
                RaisePropertyChanged("SelectedFilter");
                LoadKontakteFromRepository(_selectedFilter.ID);
                RaisePropertyChanged("Kontakte");
            }
        }

        #endregion

        #region Visibility management

        bool _editModeActive = false;

        public bool EditModeActive
        {
            get { return _editModeActive; }

            set
            {
                if (_editModeActive == value)
                {
                    return;
                }
                _editModeActive = value;
                RaisePropertyChanged("EditModeActive");
                RaisePropertyChanged("ListEnabled");
                RaisePropertyChanged("EntryFieldsEnabled");
            }
        }

        public bool ListEnabled => !_editModeActive;

        public bool EntryFieldsEnabled => _editModeActive;

        #endregion

        string _statusmessage;

        private DBLogin dbLogin = new DBLogin();

        public string Fehlermeldung
        {
            get { return dbLogin.Fehlermeldung; }

        }


        public MainViewModel()
        {
            //LoadMitarbeiterFromRepository();
            //LoadPendenzenFromRepository();
        }

        #region Load Data from Repository

        private void LoadPendenzenFromRepository(int kontaktID)
        {
            _pendenzen.Clear();
            var pendenzenFromDB = pendenzenRepository.GetAll(dbLogin.SessionID, kontaktID);
            int pendenzenCount = pendenzenFromDB.Count;
            for (int i = 0; i < pendenzenCount; i++)
            {
                _pendenzen.Add(
                    new PendenzViewModel
                    {
                        Pendenz = pendenzenFromDB[i]
                    });
            }
        }

        private void LoadMitarbeiterFromRepository()
        {
            _mitarbeiter.Clear();
            var mitarbeiterFromDB = mitarbeiterRepository.GetAll(dbLogin.SessionID);
            int mitarbeiterCount = mitarbeiterFromDB.Count;
            for (int i = 0; i < mitarbeiterCount; i++)
            {
                _mitarbeiter.Add(
                    new MitarbeiterViewModel
                    {
                        Mitarbeiter = mitarbeiterFromDB[i]
                    });
            }
        }

        private void LoadKontakteFromRepository(int filterID)
        {
            _kontakte.Clear();
            var kotakteFromDB = kontaktRepository.GetAll(dbLogin.SessionID, filterID);
            int kontakteCount = kotakteFromDB.Count;
            for (int i = 0; i < kontakteCount; i++)
            {
                _kontakte.Add(
                    new KontaktViewModel
                    {
                        Kontakt = kotakteFromDB[i]
                    });
            }
        }

        private void LoadFilterFromRepository()
        {
            _filter.Clear();
            var filterFromDB = filterRepository.GetAll(dbLogin.SessionID);
            int filterCount = filterFromDB.Count;
            for (int i = 0; i < filterCount; i++)
            {
                _filter.Add(
                    new FilterViewModel
                    {
                        Filter = filterFromDB[i]
                    });
            }
        }

        #endregion


        #region Commands

        void ExecuteLogin()
        {
            if (dbLogin == null)
            {
                return;
            }
            dbLogin.Login("uwe.singer", "us8117us");

            LoadMitarbeiterFromRepository();
            LoadFilterFromRepository();
            LoadKontakteFromRepository(0);
            if (SelectedFilter == null)
            {
                SelectedFilter = _filter[0]; 
            }
        }

        bool CanExecuteLogin()
        {
            return true;
        }

        public ICommand Login { get { return new RelayCommand(ExecuteLogin, CanExecuteLogin); } }

        //void ExecuteShowMitarbeiter()
        //{
        //    if (_mitarbeiter == null)
        //    {
        //        return;
        //    }
        //    LoadMitarbeiterFromRepository();
        //    LoadFilterFromRepository();
        //    LoadKontakteFromRepository(0);
        //}

        //bool CanExecuteShowMitarbeiter()
        //{
        //    return true;
        //}

        //public ICommand ShowMitarbeiter { get { return new RelayCommand(ExecuteShowMitarbeiter, CanExecuteShowMitarbeiter); } }

        void ExecuteAddPendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }
            _pendenzen.Add(new PendenzViewModel());


            SelectedPendenz = _pendenzen.Last();
            EditModeActive = true;
        }

        bool CanExecuteAddPendenz()
        {
            return !_editModeActive;
        }

        public ICommand AddPendenz { get { return new RelayCommand(ExecuteAddPendenz, CanExecuteAddPendenz); } }

        void ExecuteEditPendenz()
        {
            EditModeActive = true;
        }

        bool CanExecuteEditPendenz()
        {
            return (_selectedPendenz == null) ? false : !_editModeActive;
        }

        public ICommand EditPendenz { get { return new RelayCommand(ExecuteEditPendenz, CanExecuteEditPendenz); } }

        void ExecuteRemovePendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }
            if (_selectedPendenz.PendenzID != 0)
            {
                pendenzenRepository.Remove(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz, out _statusmessage); 
            }
            _pendenzen.Remove(_selectedPendenz);

            EditModeActive = false;
        }

        bool CanExecuteRemovePendenz()
        {
            return (_selectedPendenz == null) ? false : true;
        }

        public ICommand RemovePendenz { get { return new RelayCommand(ExecuteRemovePendenz, CanExecuteRemovePendenz); } }

        void ExecuteSavePendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }

            if (_selectedPendenz.PendenzID == 0)
            {
                pendenzenRepository.Add(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz, out _statusmessage);
            }
            else
            {
                pendenzenRepository.Update(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz, out _statusmessage);
            }
            EditModeActive = true;
        }

        bool CanExecuteSavePendenz()
        {
            return _editModeActive;
        }

        public ICommand SavePendenz { get { return new RelayCommand(ExecuteSavePendenz, CanExecuteSavePendenz); } }

        void ExecuteCancelPendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }

            if (_selectedPendenz.PendenzID == 0)
            {
                _pendenzen.Remove(_selectedPendenz);
            }
            else
            {
                LoadPendenzenFromRepository(_selectedKontakt.KontaktID);
            }
            EditModeActive = false;
        }

        bool CanExecuteCancelPendenz()
        {
            return _editModeActive;
        }

        public ICommand CancelPendenz { get { return new RelayCommand(ExecuteCancelPendenz, CanExecuteCancelPendenz); } }

        #endregion



    }
}
