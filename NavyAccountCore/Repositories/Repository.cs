

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly INavyAccountDbContext context;

        public Repository(INavyAccountDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await context.Instance.Set<T>().CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await context.Instance.Set<T>().CountAsync();
        }

        public IEnumerable<T> All()
        {
            
            return context.Instance.Set<T>().AsNoTracking();
        }

        public async Task<List<T>> FindPaged<T>(int page, int pageSize) where T : class
        {
            return await context.Instance.Set<T>().Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public bool Create(T entity)
        {
            try
            {
                context.Instance.Set<T>().Add(entity);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool CreateRange(IEnumerable<T> entities)
        {
            try
            {
                context.Instance.Set<T>().AddRange(entities);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> Find(int id)
        {
            return await context.Instance.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> predicate)
        {
            return context.Instance.Set<T>().Where(predicate);
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                context.Instance.Set<T>().RemoveRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Remove(T entity)
        {
            try
            {
                context.Instance.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(T entity)
        {
            try
            {
                context.Instance.Set<T>().Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                context.Instance.Set<T>().UpdateRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
