﻿using MVVM_Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                if (_selectedKontakt != null)
                {
                    LoadPendenzenFromRepository(_selectedKontakt.KontaktID); 
                }
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
                if (value != null)
                {
                    LoadKontakteFromRepository(_selectedFilter.ID);
                }
                else
                {
                    LoadKontakteFromRepository(0);
                }
                RaisePropertyChanged("Kontakte");
            }
        }

        #endregion

        #region Visibility management

        // Nachfolgend die Steuer-Funktionalitaeten fuer diverse Komponenten
        // (Listen, Buttons etc.) im Edit-Mode
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

        #region TabChange

        int _selectedTab;
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                if (_selectedTab == value)
                {
                    return;
                }
                _selectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }

        #endregion

        #region Message Handling

        // Steuert die Kontrolle fuer das Nachrichten-Anzeige im Message Feld im GUI
        private string _currentMsgFromDb;
        public string CurrentMsgFromDb
        {
            get
            {
                return _currentMsgFromDb;
            }
            set
            {
                if (_currentMsgFromDb == value)
                {
                    return;
                }
                _currentMsgFromDb = value;
                RaisePropertyChanged("CurrentMsgFromDb");
            }
        }

        #endregion

        #region DBLogin and Session Handling

        private DBLogin dbLogin = new DBLogin();

        public string LoginErrorMsg
        {
            get
            {
                return dbLogin.Fehlermeldung;
            }
        }

        public string Password { private get; set; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username == value)
                {
                    return;
                }
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        #endregion

        #region Load Data from Repository

        // Laden der Liste mit den Pendenzen
        private void LoadPendenzenFromRepository(int kontaktID)
        {
            _pendenzen.Clear();

            try
            {
                var pendenzenFromDB = pendenzenRepository.GetAll(dbLogin.SessionID, kontaktID);
                int pendenzenCount = pendenzenFromDB.Count;
                for (int i = 0; i < pendenzenCount; i++)
                {
                    string currentMitarbeiterName = string.Empty;
                    foreach (var item in _mitarbeiter)
                    {
                        if (item.ID == pendenzenFromDB[i].MitarbeiterID)
                        {
                            currentMitarbeiterName = item.Bezeichnung;
                        }
                    }
                    // Mitarbeiter Name ist nur im ViewModel vorhanden (nicht im Model).
                    // Code unten dient dem Suchen des Mitarbeiters in MitarbeiterViewModel
                    // und Setzen ins PendenzViewModel zwecks beserer Benutzerfreundlichkeit
                    PendenzViewModel pendenzViewModel = new PendenzViewModel();
                    pendenzViewModel.Pendenz = pendenzenFromDB[i];
                    pendenzViewModel.MitarbeiterName = currentMitarbeiterName;

                    _pendenzen.Add(pendenzViewModel);
                }
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        // Laden der Liste mit den Mitarbeitern
        private void LoadMitarbeiterFromRepository()
        {
            _mitarbeiter.Clear();

            try
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
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        // Laden der Liste mit den Kontakten
        private void LoadKontakteFromRepository(int filterID)
        {
            _kontakte.Clear();

            try
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
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        // Laden der Liste mit den Filtern
        private void LoadFilterFromRepository()
        {
            _filter.Clear();

            try
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
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        #endregion


        #region Commands

        #region ICommand-Button Login

        void ExecuteLogin()
        {
            try
            {
                dbLogin.Login(_username, Password);


                if (dbLogin.Fehler == 0)
                {
                    LoadMitarbeiterFromRepository();
                    LoadFilterFromRepository();
                    LoadKontakteFromRepository(0);
                    if (SelectedFilter == null)
                    {
                        SelectedFilter = _filter[0];
                    }
                    SelectedTab = 1;
                }
                else
                {
                    RaisePropertyChanged("LoginErrorMsg");
                }
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        bool CanExecuteLogin()
        {
            return true;
        }

        public ICommand Login { get { return new RelayCommand(ExecuteLogin, CanExecuteLogin); } }

        #endregion

        #region ICommand-Button Add-Pendenz

        void ExecuteAddPendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }
            _pendenzen.Add(new PendenzViewModel() { Termin = DateTime.Now });


            SelectedPendenz = _pendenzen.Last();
            EditModeActive = true;
        }

        bool CanExecuteAddPendenz()
        {
            return !_editModeActive;
        }

        public ICommand AddPendenz { get { return new RelayCommand(ExecuteAddPendenz, CanExecuteAddPendenz); } }

        #endregion

        #region ICommand-Button Edit-Pendenz

        void ExecuteEditPendenz()
        {
            EditModeActive = true;
        }

        bool CanExecuteEditPendenz()
        {
            return (_selectedPendenz == null) ? false : !_editModeActive;
        }

        public ICommand EditPendenz { get { return new RelayCommand(ExecuteEditPendenz, CanExecuteEditPendenz); } }

        #endregion

        #region ICommand-Button Remove-Pendenz

        void ExecuteRemovePendenz()
        {
            if (_pendenzen == null)
            {
                return;
            }

            try
            {
                if (_selectedPendenz.PendenzID != 0)
                {
                    DbReturnStatus result = pendenzenRepository.Remove(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz);

                    if (result.ReturnCode == 0)
                    {
                        _pendenzen.Remove(_selectedPendenz);
                    }
                    else if (result.ReturnCode == 1)
                    {
                        SelectedTab = 0;
                    }

                    CurrentMsgFromDb = result.ReturnMsg;
                }
                else if (_selectedPendenz.PendenzID == 0)
                {
                    _pendenzen.Remove(_selectedPendenz);
                }

                EditModeActive = false;
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        bool CanExecuteRemovePendenz()
        {
            return (_selectedPendenz == null) ? false : true;
        }

        public ICommand RemovePendenz { get { return new RelayCommand(ExecuteRemovePendenz, CanExecuteRemovePendenz); } }

        #endregion

        #region ICommand-Button Save-Pendenz

        void ExecuteSavePendenz()
        {
            try
            {
                DbReturnStatus result;

                if (_pendenzen == null)
                {
                    return;
                }

                if (_selectedPendenz.PendenzID == 0)
                {
                    result = pendenzenRepository.Add(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz);
                }
                else
                {
                    result = pendenzenRepository.Update(dbLogin.SessionID, _selectedKontakt.KontaktID, _selectedPendenz.Pendenz);
                }

                if (result.ReturnCode == 0)
                {
                    LoadPendenzenFromRepository(_selectedKontakt.KontaktID);
                    EditModeActive = false;
                }
                else if (result.ReturnCode == 1)
                {
                    SelectedTab = 0;
                    _editModeActive = false;
                }

                CurrentMsgFromDb = result.ReturnMsg;
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        bool CanExecuteSavePendenz()
        {
            return _editModeActive;
        }

        public ICommand SavePendenz { get { return new RelayCommand(ExecuteSavePendenz, CanExecuteSavePendenz); } }

        #endregion

        #region ICommand-Button Cancel-Pendenz

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

        #region ICommand-Button Finisheded-Pendenz

        private void ExecuteFinishedPendenz()
        {
            try
            {
                DbReturnStatus result = pendenzenRepository.SetFinished(dbLogin.SessionID, _selectedKontakt.KontaktID, SelectedPendenz.Pendenz);
                CurrentMsgFromDb = result.ReturnMsg;
                LoadPendenzenFromRepository(_selectedKontakt.KontaktID);

                
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        private bool CanExecuteFinishedPendenz()
        {
            return ((_selectedPendenz == null) ? false : !_editModeActive) && _selectedPendenz.Erledigt == false;
        }

        public ICommand FinishPendenz { get { return new RelayCommand(ExecuteFinishedPendenz, CanExecuteFinishedPendenz); } }

        #endregion

        #region ICommand-Button Unfinished-Pendenz

        private void ExecuteUnFinishedPendenz()
        {
            try
            {
                DbReturnStatus result = pendenzenRepository.SetUnfinished(dbLogin.SessionID, _selectedKontakt.KontaktID, SelectedPendenz.Pendenz);
                CurrentMsgFromDb = result.ReturnMsg;
                LoadPendenzenFromRepository(_selectedKontakt.KontaktID);
            }
            catch (ArgumentException e)
            {
                CurrentMsgFromDb = e.Message;
            }
        }

        private bool CanExecuteUnFinishedPendenz()
        {
            return ((_selectedPendenz == null) ? false : !_editModeActive) && _selectedPendenz.Erledigt == true;
        }

        public ICommand UnfinishPendenz { get { return new RelayCommand(ExecuteUnFinishedPendenz, CanExecuteUnFinishedPendenz); } }

        #endregion

        #region ICommand-Button Navigate To Tab Pendenzen

        private void ExecuteNavigateToPendenzen()
        {
            SelectedTab = 2;
            CurrentMsgFromDb = "";
        }

        private bool CanExecuteNavigateToPendenzen()
        {
            return ((_selectedKontakt == null) ? false : true);
        }

        public ICommand NavigateToPendenzen { get { return new RelayCommand(ExecuteNavigateToPendenzen, CanExecuteNavigateToPendenzen); } }

        #endregion

        #region ICommand-Button Navigate To Tab Kontakte

        private void ExecuteNavigateToKontakte()
        {
            SelectedTab = 1;
            CurrentMsgFromDb = "";
        }

        private bool CanExecuteNavigateToKontakte()
        {
            return !_editModeActive;
        }

        public ICommand NavigateToKontakte { get { return new RelayCommand(ExecuteNavigateToKontakte, CanExecuteNavigateToKontakte); } }

        #endregion

        #endregion

    }
}
