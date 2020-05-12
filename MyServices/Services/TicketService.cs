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
    public class TicketService :GenericRepository<TblTicket>, ITicketService
    {
        public TicketService(DatabaseContext context)
            : base(context)
        {
        }

        public TblTicket GetById(int id , int userId)
        {
            var ticket = DbSet.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            return ticket;
        }

        public IEnumerable<dynamic> GetLast(long updateDate, int? userId)
        {

            //var start = pageNumber * 20;1476377316-1473785853

            // تاریخ یک ماه پیش
            var lastMonth = utility.Date.GetDateTime.CurrentTimeSeconds()-2592000;

            var temp = DbSet.AsNoTracking().Where(c => c.UserId == userId && c.UpdateDate > updateDate && c.UpdateDate > lastMonth)
                //.OrderBy(n => n.Id).Skip(start).Take(20)
                .Select(c => new
            {
                c.Id,
                //c.UserId,
                //c.SSDepartment,
                c.SSSubject,
                c.Title,
                c.Time,
                c.State,
                c.UpdateDate,
            });
            return temp.ToList();
        }

        public override void Insert(TblTicket entity, int userId)
        {
            entity.Time = utility.Date.GetDateTime.CurrentTimeSeconds();
            entity.UserId = userId;
            entity.State = 1;
            
            base.Insert(entity, userId);
        }

        public override void Update(TblTicket entityToUpdate, int userId)
        {

            entityToUpdate.ActionType = 2;// update
            entityToUpdate.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            entityToUpdate.UpdateUserId = userId;// test

            DbSet.Attach(entityToUpdate);
            var record = Context.Entry(entityToUpdate);//.State = EntityState.Modified;
            record.State = EntityState.Modified;

            record.Property(p => p.InsertUserId).IsModified = false;
            record.Property(p => p.InsertDate).IsModified = false;

            record.Property(p => p.Time).IsModified = false;
            record.Property(p => p.UserId).IsModified = false;
            record.Property(p => p.State).IsModified = false;

        }

        public void Delete(int id, int userId)
        {
            var entityForDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            Delete(entityForDelete);
        }
    }
}