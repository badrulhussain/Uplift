using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int Id)
        {
            return dbSet.Find(Id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var includePropertie in includeProperties.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includePropertie);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList(); ;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includePropertie in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includePropertie);
            }

            return query.FirstOrDefault();
        }

        public void Remove(int Id)
        {
            T removeEntity = dbSet.Find(Id);
            dbSet.Remove(removeEntity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
