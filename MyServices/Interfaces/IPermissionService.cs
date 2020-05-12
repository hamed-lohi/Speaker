using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IPermissionService: IBaseService<TblPermission>
    {
        //IEnumerable<dynamic> GetMyPermissions(int userId);//, long updateDate

        void Delete(int id, int userId);
        //IQueryable<TblPermission> GetAll(
        //    Expression<Func<TblPermission, bool>> filter = null,
        //    Func<IQueryable<TblPermission>, IOrderedQueryable<TblPermission>> orderBy = null,
        //    string includeProperties = "");

        IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, string loginUserRole, int userId, int[] speechFieldIds);

        dynamic GetForEdit(int id, int userId);

        void Insert(TblPermission entity, int userId, string loginUserRole);

        void Update(TblPermission entity, int userId, string loginUserRole);

    }
}