using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IBrandService: IBaseService<TblBrand>
    {
        IEnumerable<dynamic> GetLast(int lastUpdate);
    }
}