using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.DAL;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class UpdateService :GenericRepository<TblUpdate>, IUpdateService
    {
        public UpdateService(DatabaseContext context)
            : base(context)
        {
        }

        public TblUpdate GetById(int id , int userId)
        {
            var update = DbSet.SingleOrDefault(t => t.Id == id);
            return update;
        }

        public override void Insert(TblUpdate entity, int userId)
        {
            //entity.Time = utility.Date.GetDateTime.CurrentTimeSeconds();
            //entity.UserId = userId;
            //entity.State = 1;
            
            base.Insert(entity, userId);
        }

        public override void Update(TblUpdate entityToUpdate, int userId)
        {

            //entityToUpdate.ActionType = 2;// update
            //entityToUpdate.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            //entityToUpdate.UpdateUserId = userId;// test

            //DbSet.Attach(entityToUpdate);
            //var record = Context.Entry(entityToUpdate);//.State = EntityState.Modified;
            //record.State = EntityState.Modified;

            //record.Property(p => p.InsertUserId).IsModified = false;
            //record.Property(p => p.InsertDate).IsModified = false;

            //record.Property(p => p.Time).IsModified = false;
            //record.Property(p => p.UserId).IsModified = false;
            //record.Property(p => p.State).IsModified = false;

            base.Update(entityToUpdate, userId);

        }

        public void Delete(int id, int userId)
        {
            //var entityForDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            base.Delete(id);
        }

        public dynamic GetLast()
        {
            var temp = DbSet.AsNoTracking().OrderByDescending(u=> u.Date).Select(c => new
            {
                c.Id,
                c.VersionName,
                c.VersionCode,
                c.ChangeLog,
                c.IsCritical,
                c.Date,
            }).FirstOrDefault();
            return temp;
        }
    }
}