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

        private DBLogin dbLogin = new DBLogin();

        public MainViewModel()
        {
            //LoadMitarbeiterFromRepository();
            //LoadPendenzenFromRepository();
        }

        private void LoadPendenzenFromRepository()
        {
            var pendenzenFromDB = pendenzenRepository.GetAll(dbLogin.SessionID,3);
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
