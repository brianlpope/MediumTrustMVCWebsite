using System.Linq;

namespace ThreeRoads.Data.Contracts
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        T GetByIndex(int index);
        T GetById(int id);
        void Add(T TEntity);
        void Remove(T TEntity);
        
    }
}
