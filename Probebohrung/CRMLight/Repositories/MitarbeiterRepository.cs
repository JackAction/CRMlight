using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class MitarbeiterRepository
    {
        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetStammdatenResult, MitarbeiterModel> myMapper;

        public MitarbeiterRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetStammdatenResult, MitarbeiterModel>();
        }

        public List<MitarbeiterModel> GetAll(int sessionID)
        {
            var dbReturn = dataContext.crm_GetStammdaten(sessionID, "Mitarbeiter").ToList();
            return myMapper.mapperFromDB.Map<List<MitarbeiterModel>>(dbReturn);
        }

    }
}
