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
    public class StatisticService :GenericRepository<TblStatistic>, IStatisticService
    {
        public StatisticService(DatabaseContext context)
            : base(context)
        {
        }

        public TblStatistic GetById(int id , int userId)
        {
            var statistic = DbSet.SingleOrDefault(t => t.Id == id);
            return statistic;
        }

        public override void Insert(TblStatistic entity, int userId)
        {
            //entity.Date = utility.Date.GetDateTime.CurrentTimeSeconds();
            //entity.UserId = userId;
            //entity.State = 1;
            
            base.Insert(entity, userId);
        }

        public override void Update(TblStatistic entityToStatistic, int userId)
        {

            //entityToStatistic.ActionType = 2;// Statistic
            //entityToStatistic.StatisticDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            //entityToStatistic.StatisticUserId = userId;// test

            //DbSet.Attach(entityToStatistic);
            //var record = Context.Entry(entityToStatistic);//.State = EntityState.Modified;
            //record.State = EntityState.Modified;

            //record.Property(p => p.InsertUserId).IsModified = false;
            //record.Property(p => p.InsertDate).IsModified = false;

            //record.Property(p => p.Time).IsModified = false;
            //record.Property(p => p.UserId).IsModified = false;
            //record.Property(p => p.State).IsModified = false;

            base.Update(entityToStatistic, userId);

        }

        public void Delete(int id, int userId)
        {
            //var entityForDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            base.Delete(id);
        }

        //public dynamic GetLast()
        //{
        //    var temp = DbSet.AsNoTracking().OrderByDescending(u=> u.Date).Select(c => new
        //    {
        //        c.Id,
        //        c.VersionName,
        //        c.VersionCode,
        //        c.ChangeLog,
        //        c.IsCritical,
        //        c.Date,
        //    }).FirstOrDefault();
        //    return temp;
        //}
    }
}