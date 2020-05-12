using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ITicketService : IBaseService<TblTicket>
    {
        void Delete(int id, int userId);
        TblTicket GetById(int id, int userId);
        IEnumerable<dynamic> GetLast(long updateDate, int? userId);
    }
}