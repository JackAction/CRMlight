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
        #region Listen

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

        #endregion

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

        string _statusmessage;

        private DBLogin dbLogin = new DBLogin();

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

        }

        bool CanExecuteLogin()
        {
            return true;
        }

        public ICommand Login { get { return new RelayCommand(ExecuteLogin, CanExecuteLogin); } }

        void ExecuteShowMitarbeiter()
        {
            if (_mitarbeiter == null)
            {
                return;
            }
            LoadMitarbeiterFromRepository();
            LoadFilterFromRepository();
            LoadKontakteFromRepository(0);
        }

        bool CanExecuteShowMitarbeiter()
        {
            return true;
        }

        public ICommand ShowMitarbeiter { get { return new RelayCommand(ExecuteShowMitarbeiter, CanExecuteShowMitarbeiter); } }

        void ExecuteAddPendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }
            _pendenzen.Add(new PendenzViewModel());


            SelectedPendenz = _pendenzen.Last();
            //EditModeActive = true;


            //Pendenz = pendenzenRepository.Add(dbLogin.SessionID, _selectedKontakt.KontaktID, new PendenzModel(), out _statusmessage)
        }

        bool CanExecuteAddPendenz()
        {
            return true;
        }

        public ICommand AddPendenz { get { return new RelayCommand(ExecuteAddPendenz, CanExecuteAddPendenz); } }

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
            //EditModeActive = true;
        }

        bool CanExecuteSavePendenz()
        {
            return true;
        }

        public ICommand SavePendenz { get { return new RelayCommand(ExecuteSavePendenz, CanExecuteSavePendenz); } }

        #endregion



    }
}
