using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class CityService : GenericRepository<TblCity> , ICityService
    {
        public CityService(DatabaseContext context) 
            :base(context)
        {
        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = DbSet.Where(c => c.LastUpdate > lastUpdate).Select(c => new
            {
                c.Id,
                c.CityName,
                PId = c.PId ?? 0,
                c.State,
                c.LastUpdate,
                c.Priority
            });
            return temp.ToList();
        }

        /// <summary>
        /// کمبوی استان و شهر
        /// </summary>
        /// <param name="stateId">شناسه استان</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetSelectOptions()
        {
            var temp = DbSet.Where(a=> a.PId.HasValue).OrderBy(a => a.Priority).Select(c => new
            {
                c.Id,
                Text = c.CityName,
            });
            return temp;
        }

    }
}
