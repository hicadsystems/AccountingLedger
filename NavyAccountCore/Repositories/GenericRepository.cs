﻿using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private readonly INavyAccountDbContext context;
        // protected readonly DbSet<T> _dbSet;
        public GenericRepository(INavyAccountDbContext _context)
        {
            context = _context;
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
        public IQueryable<T> GetAllAsync()
        {
            return context.Instance.Set<T>().AsNoTracking();
        }

        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _dbSet.Where(predicate).ToListAsync();
        //}

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
