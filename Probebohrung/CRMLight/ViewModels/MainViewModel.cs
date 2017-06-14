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

        private DBLogin dbLogin = new DBLogin();

        public MainViewModel()
        {
            //LoadMitarbeiterFromRepository();
            //LoadPendenzenFromRepository();
        }

        private void LoadPendenzenFromRepository(int kontaktID)
        {
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

        private void LoadFilterFromRepositor()
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

        }

        bool CanExecuteShowMitarbeiter()
        {
            return true;
        }

        public ICommand ShowMitarbeiter { get { return new RelayCommand(ExecuteShowMitarbeiter, CanExecuteShowMitarbeiter); } }


    }
}
