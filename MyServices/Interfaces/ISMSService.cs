using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;

namespace MyServices.Interfaces
{
    public interface ISMSService
    {
        void Send(string tel);
        //void Update(TblSMS item);
        //void Delete(int id);
        //TblSMS GetById(int id);
        //IEnumerable<TblSMS> GetAll(
        //    Expression<Func<TblSMS, bool>> filter = null,
        //    Func<IQueryable<TblSMS>, IOrderedQueryable<TblSMS>> orderBy = null,
        //    string includeProperties = "");
        //IEnumerable<dynamic> GetLast(int lastUpdate, int? userId);
    }
}