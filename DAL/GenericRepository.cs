using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public virtual void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual IQueryable<T> GetPage(int page, int num)
        {
            return Context.Set<T>().Skip(num * page).Take(num);
        }

        public virtual T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual T GetBy(Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().FirstOrDefault(filter);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().Where(filter);
        }
    }
}

