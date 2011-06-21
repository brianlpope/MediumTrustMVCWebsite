using System;
using System.Linq;
using ThreeRoads.Data.Contracts;

namespace ThreeRoads.Data.SqlServerCe
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected ThreeRoadsDb _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork as ThreeRoadsDb;
        }

        public System.Linq.IQueryable<T> GetAll()
        {
            return _unitOfWork.Set<T>();
        }

        public T GetById(int id)
        {
            return _unitOfWork.Set<T>().Find(id);
        }
        public T GetByIndex(int index)
        {
            return _unitOfWork.Set<T>().ElementAt(index);
        }

        public void Add(T entity)
        {
            _unitOfWork.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _unitOfWork.Set<T>().Remove(entity);
        }
    }
}
