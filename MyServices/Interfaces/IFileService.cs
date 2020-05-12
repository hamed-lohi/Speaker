using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IFileService: IBaseService<TblFile>
    {
        IEnumerable<dynamic> GetMyFiles(int userId);//, long updateDate
        void Delete(int id, int userId);
        IEnumerable<dynamic> GetDataTable();
    }
}