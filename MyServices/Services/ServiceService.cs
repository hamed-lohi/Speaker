using System.Collections.Generic;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class ServiceService :GenericRepository<TblService>, IServiceService
    {
        public ServiceService(DatabaseContext context)
            : base(context)
        {
        }

        public IEnumerable<dynamic> GetLast(int updateDate)
        {
            var temp = DbSet.AsNoTracking().Where(c => c.UpdateDate > updateDate).Select(c => new
            {
                c.Id,
                c.ServiceName,
                c.IconUrl,
                //c.Cost,
                //c.State,
                c.LastUpdate,
                //c.Description
            });
            return temp.ToList();
        }

        public dynamic GetByIdDynamic(int id)
        {
            var temp = base.GetById(id);
            return new
            {
                temp.Id,
                temp.ServiceName,
                temp.IconUrl,
                temp.Description,
                temp.Cost,
                temp.HelpImageUrl,
                temp.Time,
            };
        }
    }
}