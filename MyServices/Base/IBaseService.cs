using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyModels.Configuration;

namespace MyServices.Base
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {

        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(int id);
        void Insert(TEntity entity, int userId);
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate, int userId);
    }
}