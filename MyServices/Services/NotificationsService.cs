using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class NotificationsService :GenericRepository<TblNotifications>, INotificationsService
    {
        public NotificationsService(DatabaseContext context)
            : base(context)
        {
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
                UserId = c.UserId?? 0,
                //PostId = c.PostId?? 0,
                c.Title,
                c.Description,
                c.Date,
                c.State,
                c.UpdateDate,
            });
            return temp.ToList();
        }

        public void Delete(int id, int userId)
        {
            var entityToDelete = DbSet.FirstOrDefault(n => n.Id == id && n.UserId == userId);
            Delete(entityToDelete);
        }
    }
}