using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Configuration;
using MyModels.DAL;
using MyServices.DAL;
using MyServices.Interfaces;

namespace MyServices.Base
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        internal DatabaseContext Context;
        internal DbSet<TEntity> DbSet;
        internal UnitOfWork _unitOfWork;

        //internal UnitOfWork _unitOfWork;

        public GenericRepository(DatabaseContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            _unitOfWork = new UnitOfWork(context);
        }

        public virtual IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual dynamic GetForEdit(int id, int userId)
        {
            return GetById(id);
        }

        public virtual void Insert(TEntity entity, int userId)
        {
            var currentDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            entity.ActionType = 1; // insert
            entity.InsertDate = currentDate;
            entity.InsertUserId = userId; // test
            entity.UpdateDate = currentDate;
            entity.UpdateUserId = userId; // test

            DbSet.Add(entity);
        }

        public virtual void Delete(int id, int userId)
        {
            Delete(id);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate, int userId)
        {
            entityToUpdate.ActionType = 2;// update
            entityToUpdate.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            entityToUpdate.UpdateUserId = userId;// test

            DbSet.Attach(entityToUpdate);
            var record = Context.Entry(entityToUpdate);//.State = EntityState.Modified;
            
            record.State=EntityState.Modified;

            record.Property(p => p.InsertUserId).IsModified = false;
            record.Property(p => p.InsertDate).IsModified = false;

        }

    }
}
