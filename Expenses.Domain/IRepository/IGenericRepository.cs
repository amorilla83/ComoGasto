using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expenses.Domain
{
    public interface IGenericRepository <TEntity>
        where TEntity : class
    {
        //El get sería para obtener las entidades para modificarlas
        ICollection<TEntity> GetAll();
        ICollection<TEntity> FindAll();
        ICollection<TEntity> GetAllBy(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        //Hacemos el find para cuando queramos obtener el objeto de solo lectura
        //Llevará el método AsNoTracking para no trackear los cambios
        ICollection<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate);

        TEntity GetBy(Expression<Func<TEntity, bool>> match);

        TEntity GetById(object id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void DeleteById(object id);
        void Delete(TEntity entity);

        int Count();
    }
}
