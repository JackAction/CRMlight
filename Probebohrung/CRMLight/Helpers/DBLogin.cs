using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class DBLogin
    {
        private LINQtoSQLDataContext dataContext;
        private crm_AnmeldenResult dbLogin;

        public DBLogin()
        {
            dataContext = new LINQtoSQLDataContext();
        }

        public void Login(string benutzername, string passwort)
        {
            dbLogin = dataContext.crm_Anmelden(benutzername, passwort).ToList()[0];
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
            get { return dbLogin.Fehlermeldung; }
        }
    }
}
