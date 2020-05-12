using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IStatisticService : IBaseService<TblStatistic>
    {
        void Delete(int id, int userId);
        TblStatistic GetById(int id, int userId);
        //IEnumerable<dynamic> GetLast();
    }
}