using System.Collections.Generic;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class ReportAbuseService :GenericRepository<TblReportAbuse>, IReportAbuseService
    {
        public ReportAbuseService(DatabaseContext context)
            : base(context)
        {
        }

        public override void Insert(TblReportAbuse item, int userId)
        {
            item.UserId = userId;
            base.Insert(item, userId);
        }

        //public IEnumerable<dynamic> GetLast(int lastUpdate, int? userId)
        //{
        //    var temp = DbSet.AsNoTracking().Where(c => c.LastUpdate > lastUpdate && c.UserId == userId).Select(c => new
        //    {
        //        c.Id,
        //        UserId = c.UserId?? 0,
        //        c.Title,
        //        c.Description,
        //        c.Date,
        //        c.State,
        //        c.LastUpdate,
        //    });
        //    return temp.ToList();
        //}
    }
}