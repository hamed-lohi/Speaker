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
    public class ChargeService :GenericRepository<TblCharge>, IChargeService
    {
        public ChargeService(DatabaseContext context)
            : base(context)
        {
        }

        public TblCharge GetById(int id , int userId)
        {
            var transaction = DbSet.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            return transaction;
        }

        public override void Insert(TblCharge entity, int userId)
        {
            entity.ChargeTime = utility.Date.GetDateTime.CurrentTimeSeconds();
            entity.UserId = userId;
            //entity.State = 1;
            
            base.Insert(entity, userId);
        }

        public override void Update(TblCharge entityToTransaction, int userId)
        {

            //entityToTransaction.ActionType = 2;// Transaction
            //entityToTransaction.TransactionDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            //entityToTransaction.TransactionUserId = userId;// test

            //DbSet.Attach(entityToTransaction);
            //var record = Context.Entry(entityToTransaction);//.State = EntityState.Modified;
            //record.State = EntityState.Modified;

            //record.Property(p => p.InsertUserId).IsModified = false;
            //record.Property(p => p.InsertDate).IsModified = false;

            //record.Property(p => p.Time).IsModified = false;
            //record.Property(p => p.UserId).IsModified = false;
            //record.Property(p => p.State).IsModified = false;

            base.Update(entityToTransaction, userId);

        }

        public override void Delete(int id, int userId)
        {
            var entityForDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            base.Delete(entityForDelete);
        }

        public IEnumerable<dynamic> GetMyCharges(int userId)
        {
            var temp = DbSet.AsNoTracking().Where(c => c.UserId == userId).Select(c => new
            {
                c.Id,
                c.ChargeOptionId,
                c.Amount,
                c.Code,
                c.ChargeTime
            });
            return temp.ToList();
        }
    }
}