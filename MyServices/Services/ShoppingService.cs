using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using MyModels.DAL;
using MyModels.Entity;
using MyModels.Models;
using MyServices.Base;
using MyServices.DAL;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class ShoppingService : GenericRepository<TblShopping>, IShoppingService
    {

        public ShoppingService(DatabaseContext context)
            : base(context)
        {
        }

        public override void Insert(TblShopping entity, int userId)
        {
            var user = Context.Users.Find(userId);
            var service = Context.TblServices.Find(entity.ServiceId);
            //var post = Context.TblPosts.Find(entity.PostId);

            if (user.Credit < service.Cost)
            {
                throw new Exception("کمبود اعتبار");
            }

            if (service.Id == 18) // نگین
            {
                
            }
            else if (service.Id == 19) // بالون
            {
                //post.Priority = 1;
                //post.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            }
            else if (service.Id == 2 || service.Id == 6 ) // بیلبورد
            {
                var lastWeek = utility.Date.GetDateTime.DifferenceDay(-7); //utility.Date.GetDateTime.CurrentTimeSeconds() -604800;

                if (DbSet.Any(s=> s.ServiceId == entity.ServiceId && s.ShoppingTime >= lastWeek))
                {

                    throw new Exception("استفاده از این محصول در هر هفته یکبار امکان پذیر است");
                }
                
            }

            entity.ShoppingTime = utility.Date.GetDateTime.CurrentTimeSeconds();
            entity.UserId = userId;
            entity.ExpirationTime = utility.Date.GetDateTime.DifferenceHour(service.Time ?? 0); //entity.ShoppingTime + (service.Time*60);
            entity.State = 1;

            base.Insert(entity, userId);
            //post.ServiceId = service.Id;
            user.Credit -= service.Cost;

        }

        public override void Update(TblShopping entityToUpdate, int userId)
        {

            entityToUpdate.ActionType = 2;// update
            entityToUpdate.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            entityToUpdate.UpdateUserId = userId;// test

            DbSet.Attach(entityToUpdate);
            var record = Context.Entry(entityToUpdate);//.State = EntityState.Modified;
            record.State = EntityState.Modified;

            record.Property(p => p.InsertUserId).IsModified = false;
            record.Property(p => p.InsertDate).IsModified = false;

            record.Property(p => p.ShoppingTime).IsModified = false;
            record.Property(p => p.UserId).IsModified = false;
            record.Property(p => p.State).IsModified = false;
            record.Property(p => p.ExpirationTime).IsModified = false;

        }


        public IEnumerable<dynamic> GetMyShopping(int userId , long updateDate)
        {
            var temp = DbSet.AsNoTracking().Where(c=> c.UserId == userId && c.UpdateDate > updateDate).Select(c => new
            {
                c.Id,
                c.TblService.ServiceName,
                c.TblService.IconUrl,
                //c.PostId,
                //c.TblPost.Title,
                c.State,
                c.UpdateDate,
                //c.Description
            });
            return temp.ToList();
        }

        public dynamic GetByIdDynamic(int id, int userId)
        {
            var temp = DbSet.AsNoTracking().Where(c=> c.UserId == userId && c.Id == id).Include(r=> r.TblService).SingleOrDefault();
            return new
            {
                temp.Id,
                temp.TblService.ServiceName,
                temp.TblService.IconUrl,
                temp.Description,
                //temp.TblPost.Title,
                temp.State,
                temp.ShoppingTime,
                temp.ExpirationTime,
                //temp.PostId,
                temp.ServiceId
            };
        }

        public void Delete(int id, int userId)
        {
            var entityToDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            Delete(entityToDelete);
        }

    }
}