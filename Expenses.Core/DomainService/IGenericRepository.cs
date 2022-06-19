using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.DomainService
{
    public interface IGenericRepository <TEntity>
        where TEntity : class
    {
        //El get sería para obtener las entidades para modificarlas
        //IQueryable<TEntity> GetAll();
        //IQueryable<TEntity> FindAll();
        //IQueryable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> filter = null, 
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        ////Hacemos el find para cuando queramos obtener el objeto de solo lectura
        ////Llevará el método AsNoTracking para no trackear los cambios
        //IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> match);

        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteByIdAsync(object id);
        //void Delete(TEntity entity);

        //int Count();
    }
}
