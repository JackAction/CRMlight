using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    class PendenzenRepository
    {
        #region ATTRIBUTS

        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetPendenzenResult, PendenzModel> myMapper;

        // Standard String fuer unterbrochene DB Conn. kann bei bedarf geaendert werden
        private readonly string dbErrorConnMessage = string.Format("Verbindungsfehler zur Datenbank!");

        #endregion

        #region CONSTRUCTOR

        public PendenzenRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetPendenzenResult, PendenzModel>();
        }

        #endregion

        #region PRIVATE Methods

        private bool WasActionSuccessful(crm_SetPendenzResult dataRow, out string returnResult)
        {
            string dbErrorMsg = dataRow.Fehlermeldung;
            int dbErrorNr = dataRow.Fehler.GetValueOrDefault();
            string returnMsg = string.Format(dbErrorMsg + " - Status Code: " + dbErrorNr);

            //returnResult = returnMsg;

            if (dbErrorNr != 0)
            {
                returnResult = returnMsg;
                return false;
            }
            else
            {
                returnResult = "Erfolgreich - Status Code: 0";
                return true;
            }
        }

        #endregion

        #region CRUD Operation Methods

        /// <summary>
        /// Uebernimmt das Setzen der Parameter und den Prozedur Aufruf auf der DB,
        /// um eine neue Pendenz zu erfassen
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="kontaktId"></param>
        /// <param name="model"></param>
        /// <returns>Bool mit Aktionsstatus + DB Msg per OUTPUT Param</returns>
        public bool Add(int sessionId, int kontaktId, PendenzModel model, out string returnMsg)
        {
            string _modus = "INSERT";
            string _returnMsg = string.Empty;
            
            try
            {
                ISingleResult<crm_SetPendenzResult> result = dataContext.crm_SetPendenz(
                        sessionId,
                        kontaktId,
                        0,
                        _modus,
                        model.Termin,
                        model.MitarbeiterID,
                        model.Titel,
                        model.Beschreibung
                        );

                bool returnStatus = WasActionSuccessful(result.Single(), out _returnMsg);
                returnMsg = _returnMsg;
                return returnStatus;
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        /// <summary>
        /// Uebernimmt das Setzen der Parameter fuer den Prozeduraufruf,
        /// zum updaten einer bereits exisierenden Pendenz
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="kontaktId"></param>
        /// <param name="model"></param>
        /// <returns>Bool mit Aktionsstatus + DB Msg per OUTPUT Param</returns>
        public bool Update(int sessionId, int kontaktId, PendenzModel model, out string returnMsg)
        {
            string _modus = "UPDATE";
            string _returnMsg = string.Empty;

            try
            {
                ISingleResult<crm_SetPendenzResult> result = dataContext.crm_SetPendenz(
                        sessionId,
                        kontaktId,
                        model.PendenzID,
                        _modus,
                        model.Termin,
                        model.MitarbeiterID,
                        model.Titel,
                        model.Beschreibung
                        );

                bool returnStatus = WasActionSuccessful(result.Single(), out _returnMsg);
                returnMsg = _returnMsg;
                return returnStatus;
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        /// <summary>
        /// Uebernimmt das Setzen von Parametern fuer den Prozeduraufruf,
        /// um eine bestehende Pendenz zu loeschen
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="kontaktId"></param>
        /// <param name="model"></param>
        /// <returns>Bool mit Aktionsstatus + DB Msg per OUTPUT Param</returns>
        public bool Remove(int sessionId, int kontaktId, PendenzModel model, out string returnMsg)
        {
            string _modus = "DELETE";
            string _returnMsg = string.Empty;

            try
            {
                ISingleResult<crm_SetPendenzResult> result = dataContext.crm_SetPendenz(
                        sessionId,
                        kontaktId,
                        model.PendenzID,
                        _modus,
                        model.Termin,
                        model.MitarbeiterID,
                        model.Titel,
                        model.Beschreibung
                        );

                bool returnStatus = WasActionSuccessful(result.Single(), out _returnMsg);
                returnMsg = _returnMsg;
                return returnStatus;
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        /// <summary>
        /// Uebernimmt das Setzen von Parametern fuer den Prozeduraufruf,
        /// um eine bestehende Pendenz auf ERLEDIGT zu setzen
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="kontaktId"></param>
        /// <param name="model"></param>
        /// <param name="returnMsg"></param>
        /// <returns>Bool mit Aktionsstatus + DB Msg per OUTPUT Param</returns>
        public bool SetFinished(int sessionId, int kontaktId, PendenzModel model, out string returnMsg)
        {
            string _modus = "FINISH";
            string _returnMsg = string.Empty;

            try
            {
                ISingleResult<crm_SetPendenzResult> result = dataContext.crm_SetPendenz(
                        sessionId,
                        kontaktId,
                        model.PendenzID,
                        _modus,
                        model.Termin,
                        model.MitarbeiterID,
                        model.Titel,
                        model.Beschreibung
                        );

                bool returnStatus = WasActionSuccessful(result.Single(), out _returnMsg);
                returnMsg = _returnMsg;
                return returnStatus;
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        /// <summary>
        /// Uebernimmt das Setzen von Parametern fuer den Prozeduraufruf,
        /// um eine bestehende Pendenz auf UNERLEDIGT zu setzen
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="kontaktId"></param>
        /// <param name="model"></param>
        /// <param name="returnMsg"></param>
        /// <returns>Bool mit Aktionsstatus + DB Msg per OUTPUT Param</returns>
        public bool SetUnfinished(int sessionId, int kontaktId, PendenzModel model, out string returnMsg)
        {
            string _modus = "UNFINISH";
            string _returnMsg = string.Empty;

            try
            {
                ISingleResult<crm_SetPendenzResult> result = dataContext.crm_SetPendenz(
                    sessionId,
                    kontaktId,
                    model.PendenzID,
                    _modus,
                    model.Termin,
                    model.MitarbeiterID,
                    model.Titel,
                    model.Beschreibung
                    );

                bool returnStatus = WasActionSuccessful(result.Single(), out _returnMsg);
                returnMsg = _returnMsg;
                return returnStatus;
            }
            catch (Exception)
            {
                throw new ArgumentException(dbErrorConnMessage);
            }
        }

        #endregion

        #region Loader Methods

        public List<PendenzModel> GetAll(int sessionID, int kontaktID)
        {
            var dbReturn = dataContext.crm_GetPendenzen(sessionID, kontaktID).ToList();
            return myMapper.mapperFromDB.Map<List<PendenzModel>>(dbReturn);
        }

        #endregion
    }
}
