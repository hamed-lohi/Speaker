using MyModels.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyModels.DAL;
using MyServices.Services;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface ICategoryService : IBaseService<TblCategory>
    {
        IEnumerable<dynamic> GetLast(int lastUpdate);

        int GetRootOfOneCategory(int categoryId);

        List<int> GetChildOfOneCategory(int categoryId);

        /// <summary>
        /// دریافت لیست دسته بندی ها به همراه ریشه ی آنها
        /// </summary>
        List<CategoryService.CategoryIdRoot> GetNodesWithRoot();

    }
}
