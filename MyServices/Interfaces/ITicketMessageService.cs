using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ITicketMessageService : IBaseService<TblTicketMessage>
    {
        void Delete(int id, int userId);
        TblTicketMessage GetById(int id, int userId);
        IEnumerable<dynamic> GetLast(long updateDate, int? userId);

        IEnumerable<dynamic> GetAllByTicketId(int userId, int ticketId , int updateDate);
    }
}