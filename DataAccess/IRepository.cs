// Repository Interface in C#.
using System.Collections.Generic;
using Carservice.Entities;

namespace Carservice.DataAccess
{
    public interface IRepository<T> where T: IEntity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
        IEnumerable<T> findAll();
    }
}