using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IShoppingService : IBaseService<TblShopping>
    {
        void Insert(TblShopping item, int userId);
        void Update(TblShopping item, int userId);
        void Delete(int id, int userId);
        TblShopping GetById(int id);
        IEnumerable<dynamic> GetMyShopping(int userId, long updateDate);
        dynamic GetByIdDynamic(int id, int userId);
    }
}