using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IServiceService : IBaseService<TblService>
    {
        IEnumerable<dynamic> GetLast(int updateDate);

        dynamic GetByIdDynamic(int id);
    }
}