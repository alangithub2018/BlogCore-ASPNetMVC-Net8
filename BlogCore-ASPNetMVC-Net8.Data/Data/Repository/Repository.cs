using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        internal DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        void IRepository<T>.Add(T entity)
        {
            _dbSet.Add(entity);
        }

        T IRepository<T>.Get(int id)
        {
            return _dbSet.Find(id);
        }

        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeProperties)
        {
            // It is created an IQueryable starting from the DbSet context
            IQueryable<T> query = _dbSet;

            // It applies the filter if applicable
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // It includes properties if they are provided
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Apply sorting if provided
            if (orderBy != null)
            {
                // orderBy executed and returns a list of elements
                return orderBy(query).ToList();
            }

            // it returns a list of elements
            return query.ToList();
        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>>? filter, string? includeProperties)
        {
            IQueryable<T> query = _dbSet;
            // It applies the filter if applicable
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // It includes properties if they are provided
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        void IRepository<T>.Remove(int id)
        {
            T entityToRemove = _dbSet.Find(id);
        }

        void IRepository<T>.Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
