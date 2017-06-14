using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMLight
{
    public class FilterRepository
    {
        private LINQtoSQLDataContext dataContext;
        private MyMapper<crm_GetStammdatenResult, FilterModel> myMapper;

        public FilterRepository()
        {
            dataContext = new LINQtoSQLDataContext();
            myMapper = new MyMapper<crm_GetStammdatenResult, FilterModel>();
        }

        public List<FilterModel> GetAll(int sessionID)
        {

            var dbReturn = dataContext.crm_GetStammdaten(sessionID, "Filter").ToList();
            return myMapper.mapperFromDB.Map<List<FilterModel>>(dbReturn);

        }
    }
}
