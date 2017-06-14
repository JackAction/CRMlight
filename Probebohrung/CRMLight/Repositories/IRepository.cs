using System.Collections.Generic;

namespace CRMLight
{
    public interface IRepository<T>
    {
        List<T> GetAll(int sessionID, int kontaktID);
        T GetByID(int ID);
        T Add(T model);
        void Remove(T model);
        T Edit(T model);
    }
}