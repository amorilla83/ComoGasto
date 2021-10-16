using Expenses.Core.DomainService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Expenses.Infrastructure.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected readonly ExpensesContext _context;

        public GenericRepository(ExpensesContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task DeleteByIdAsync(object id)
        {
            TEntity entityToDelete = await _context.Set<TEntity>().FindAsync(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(match);
        }

        public IQueryable<TEntity> FindAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsNoTracking();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
