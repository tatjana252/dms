using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetBy(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter);

    }
}
