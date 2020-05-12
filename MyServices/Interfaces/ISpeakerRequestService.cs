using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ISpeakerRequestService : IBaseService<TblSpeakerRequest>
    {
        //IEnumerable<dynamic> GetMySpeakerRequests(int userId);//, long updateDate
        void Delete(int id, int userId);
        //IEnumerable<TblSpeakerRequest> GetAll(
        //    Expression<Func<TblSpeakerRequest, bool>> filter = null,
        //    Func<IQueryable<TblSpeakerRequest>, IOrderedQueryable<TblSpeakerRequest>> orderBy = null,
        //    string includeProperties = "");

        IEnumerable<dynamic> GetDataTable(string loginUserRole, int userId);

        dynamic GetForEdit(int id, int userId);

        dynamic GetDefaultInfo(int userId);

    }
}