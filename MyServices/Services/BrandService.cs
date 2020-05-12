using System.Collections.Generic;
using System.Linq;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class BrandService :GenericRepository<TblBrand>, IBrandService
    {
        public BrandService(DatabaseContext context)
            : base(context)
        {
        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = DbSet.AsNoTracking().Where(c => c.LastUpdate > lastUpdate).Select(c => new
            {
                c.Id,
                PId = c.PId ?? 0,
                SSModel = c.SSModel ?? 0,
                c.BrandName,
                //c.CategoryId,
                c.Priority,
                c.IconUrl,
                c.State,
                c.LastUpdate
            });
            return temp.ToList();
        }

    }
}