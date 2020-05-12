using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyModels.Models;
using MyModels.ViewModel;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IConstService: IBaseService<TblConst>
    {

        void Save(TblConst newRecord, int userId);

        /// <param name="pId">شناسه دسته بندی</param>
        IEnumerable<dynamic> GetDataTable(int? pId);

        /// <summary>
        /// کمبوی ثابت ها
        /// </summary>
        /// <param name="stateId">شناسه استان</param>
        /// <returns></returns>
        IEnumerable<dynamic> GetSelectOptions(int pId);
        //void Delete(int[] ids);

        ResponsePagingViewModel GetAllConst(RequestPagingViewModel con);
        IEnumerable<dynamic> GetLast(int lastUpdate);
    }
}