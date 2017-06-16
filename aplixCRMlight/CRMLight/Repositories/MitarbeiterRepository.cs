using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMLight
{
    public class MitarbeiterRepository
    {
        #region ATTRIBUTS

        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetStammdatenResult, MitarbeiterModel> myMapper;

        // Standard String fuer unterbrochene DB Conn. kann bei bedarf geaendert werden
        private readonly string dbErrorConnMessage = string.Format("Verbindungsfehler zur Datenbank!");

        #endregion

        #region CONSTRUCTORS

        public MitarbeiterRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetStammdatenResult, MitarbeiterModel>();
        }

        #endregion

        #region PUBLIC Methods

        /// <summary>
        /// Liefert eine Liste mit allen Mitarbeitern zurueck. Liste
        /// enthaelt alle Infomationen ueber einen Mitarbeiter
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns>Liste vom Typ MitarbeiterModel</returns>
        public List<MitarbeiterModel> GetAll(int sessionID)
        {
            try
            {
                var dbReturn = dataContext.crm_GetStammdaten(sessionID, "Mitarbeiter").ToList();
                return myMapper.mapperFromDB.Map<List<MitarbeiterModel>>(dbReturn);
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        #endregion
    }
}
