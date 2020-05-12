using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IUpdateService : IBaseService<TblUpdate>
    {
        void Delete(int id, int userId);
        TblUpdate GetById(int id, int userId);
        dynamic GetLast();
    }
}