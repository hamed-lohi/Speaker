using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ICityService : IBaseService<TblCity>
    {
        IEnumerable<dynamic> GetLast(int lastUpdate);

        /// <summary>
        /// کمبوی استان و شهر
        /// </summary>
        /// <param name="stateId">شناسه استان</param>
        /// <returns></returns>
        IEnumerable<dynamic> GetSelectOptions();
    }
}