using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class FilterRepository
    {
        #region ATTRIBUTS

        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetStammdatenResult, FilterModel> myMapper;

        // Standard String fuer unterbrochene DB Conn. kann bei bedarf geaendert werden
        private readonly string dbErrorConnMessage = string.Format("Verbindungsfehler zur Datenbank!");

        #endregion

        #region CONSTRUCTORS

        public FilterRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetStammdatenResult, FilterModel>();
        }

        #endregion

        #region PUBLIC Methods

        /// <summary>
        /// Fuehrt eine SQL-Prozedur auf SQL Seite aus, welche alle fuer den Benutzer
        /// sichtbaren Filter zurueckliefert.
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns>Liste vom Typ FilterModel</returns>
        public List<FilterModel> GetAll(int sessionID)
        {
            try
            {
                var dbReturn = dataContext.crm_GetStammdaten(sessionID, "Filter").ToList();
                return myMapper.mapperFromDB.Map<List<FilterModel>>(dbReturn);
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }

        }

        #endregion
    }
}
