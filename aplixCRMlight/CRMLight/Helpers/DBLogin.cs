using System;
using System.Linq;

namespace CRMLight
{
    public class DBLogin
    {
        #region ATTRIBUTS

        private LINQtoSQLDataContext dataContext;
        private crm_AnmeldenResult dbLogin;

        // Standard String fuer unterbrochene DB Conn. kann bei bedarf geaendert werden
        private readonly string dbErrorConnMessage = string.Format("Verbindungsfehler zur Datenbank!");

        #endregion

        #region PUBLIC Methods

        public DBLogin()
        {
            dataContext = new LINQtoSQLDataContext();
        }

        public void Login(string benutzername, string passwort)
        {
            try
            {
                dbLogin = dataContext.crm_Anmelden(benutzername, passwort).ToList()[0];
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        public int SessionID
        {
            get { return dbLogin.SessionID.GetValueOrDefault(); }
        }

        public int Fehler
        {
            get { return dbLogin.Fehler.GetValueOrDefault(); }
        }

        public string Fehlermeldung
        {
            get
            {
                if (dbLogin == null)
                {
                    return "";
                }
                return dbLogin.Fehlermeldung;
            }
        }

        #endregion
    }
}
