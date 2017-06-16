using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMLight
{
    public class KontaktRepository
    {
        #region ATTRIBUTS

        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetKontakteResult, KontaktModel> myMapper;

        // Standard String fuer unterbrochene DB Conn. kann bei bedarf geaendert werden
        private readonly string dbErrorConnMessage = string.Format("Verbindungsfehler zur Datenbank!");

        #endregion

        #region CONSTRUCTORS

        public KontaktRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetKontakteResult, KontaktModel>();
        }

        #endregion

        #region PUBLIC Methods

        /// <summary>
        /// Fuehrt eine Prozedur auf SQl Seite aus, welche per Filter-Angabe
        /// eine Liste mit allen Kontakten zurueck liefert. Die Filter Liste ist
        /// in FilterModel gespeichert
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="filterID"></param>
        /// <returns>Liste vom Typ KontakteModel</returns>
        public List<KontaktModel> GetAll(int sessionID, int filterID)
        {
            try
            {
                var dbReturn = dataContext.crm_GetKontakte(sessionID, filterID).ToList();
                return myMapper.mapperFromDB.Map<List<KontaktModel>>(dbReturn);
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        #endregion
    }
}
