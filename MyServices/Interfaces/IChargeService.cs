using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IChargeService:IBaseService<TblCharge>
    {
        void Delete(int id, int userId);
        TblCharge GetById(int id, int userId);

        IEnumerable<dynamic> GetMyCharges(int userId);
    }
}