using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class KontaktRepository
    {
        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetKontakteResult, KontaktModel> myMapper;

        public KontaktRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetKontakteResult, KontaktModel>();
        }

        public List<KontaktModel> GetAll(int sessionID, int filterID)
        {
            var dbReturn = dataContext.crm_GetKontakte(sessionID, filterID).ToList();
            return myMapper.mapperFromDB.Map<List<KontaktModel>>(dbReturn);
        }
    }
}
