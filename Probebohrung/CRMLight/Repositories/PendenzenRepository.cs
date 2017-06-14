using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    class PendenzenRepository : IRepository<PendenzModel>
    {
        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetPendenzenResult, PendenzModel> myMapper;

        public PendenzenRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetPendenzenResult, PendenzModel>();
        }

        public PendenzModel Add(PendenzModel model)
        {
            throw new NotImplementedException();
        }

        public PendenzModel Edit(PendenzModel model)
        {
            throw new NotImplementedException();
        }

        public List<PendenzModel> GetAll(int sessionID, int kontaktID)
        {
            var dbReturn = dataContext.crm_GetPendenzen(sessionID, kontaktID).ToList();
            return myMapper.mapperFromDB.Map<List<PendenzModel>>(dbReturn);
        }

        public PendenzModel GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Remove(PendenzModel model)
        {
            throw new NotImplementedException();
        }
    }
}
