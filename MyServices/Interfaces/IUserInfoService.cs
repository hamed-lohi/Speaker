using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{

    /// <summary>
    /// اطلاعات کاربر - سرویس
    /// </summary>
    public interface IUserInfoService: IBaseService<TblUserInfo>
    {
        IEnumerable<dynamic> GetMyUserInfos(int userId);//, long updateDate

        //void Delete(int id, int userId);
        //IQueryable<TblUserInfo> GetAll(
        //    Expression<Func<TblUserInfo, bool>> filter = null,
        //    Func<IQueryable<TblUserInfo>, IOrderedQueryable<TblUserInfo>> orderBy = null,
        //    string includeProperties = "");

        //IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, int userId, int[] speechFieldIds);

        //IEnumerable<dynamic> GetCurrentUserInfo(int userId);

        /// <summary>
        /// کمبوی سخنران ها
        /// </summary>
        /// <param name="speechFieldId">شناسه زمینه سخنرانی</param>
        //IEnumerable<dynamic> GetSelectOptions(int? speechFieldId);

        dynamic GetForEditMyInfo(int userId);
    }
}