using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ISpeakerService: IBaseService<TblSpeaker>
    {
        //IEnumerable<dynamic> GetMySpeakers(int userId);//, long updateDate

        void Delete(int id, int userId);
        //IQueryable<TblSpeaker> GetAll(
        //    Expression<Func<TblSpeaker, bool>> filter = null,
        //    Func<IQueryable<TblSpeaker>, IOrderedQueryable<TblSpeaker>> orderBy = null,
        //    string includeProperties = "");

        IEnumerable<dynamic> GetPublishedList(string loginUserRole, int userId, int[] speechFieldIds);

        IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, string loginUserRole, int userId, int[] speechFieldIds);

        IEnumerable<dynamic> GetTopSpeakers(int userId);

        /// <summary>
        /// کمبوی سخنران ها
        /// </summary>
        /// <param name="speechFieldId">شناسه زمینه سخنرانی</param>
        IEnumerable<dynamic> GetSelectOptions(int? speechFieldId);

        dynamic GetForEdit(int id, int userId);

        void Insert(TblSpeaker entity, int userId, string loginUserRole);

        void Update(TblSpeaker entity, int userId, string loginUserRole);

        IEnumerable<dynamic> SiteSearch(string searchText);
    }
}