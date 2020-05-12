using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class CategoryService : GenericRepository<TblCategory>, ICategoryService
    {
        //private static ConcurrentDictionary<int, TblCategory> _tblCategories = new ConcurrentDictionary<int, TblCategory>();
        //private DatabaseContext db = new DatabaseContext();

        public CategoryService(DatabaseContext context)
            :base(context)
        {
        }

        //public IQueryable<TblCategory> GetAll()
        //{
        //    return _tblCategories.Values.AsQueryable();
        //}

        //public void Add(TblCategory item)
        //{
        //    //item.Id = Guid.NewGuid().ToString();
        //    _tblCategories[item.Id] = item;
        //}

        //public TblCategory Find(string key)
        //{
        //    TblCategory item;
        //    _tblCategories.TryGetValue(key, out item);
        //    return item;
        //}

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            return DbSet.Where(c => c.LastUpdate > lastUpdate).Select(c => new
            {
                c.Id,
                c.CategoryName,
                c.PId ,
                c.Priority,
                c.IconUrl,
                c.State,
                c.LastUpdate,
                
                c.BrandId,
                c.SSGender,
                c.SSSaleOrBuy,
                c.SSStock,
                c.SSTradeType,
                c.SSType,
                c.SSType2,
                c.HelpText

            }).ToList();
        }

        /// <summary>
        /// دریافت شناسه دسته بندی ریشه ی آگهی
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int GetRootOfOneCategory(int categoryId)
        {
            const string st = "WITH tblParent AS ( SELECT * FROM iTblCategory WHERE Id = @p0 UNION ALL SELECT iTblCategory.* FROM iTblCategory  JOIN tblParent"+
                              "  ON iTblCategory.Id = tblParent.PId) SELECT tblParent.Id FROM  tblParent WHERE PId is null OPTION(MAXRECURSION 1000)";
          
            return Context.Database.SqlQuery<int>(st, categoryId).Single();
        }

        /// <summary>
        /// دریافت شناسه دسته بندی های زیر مجموعه ی دسته بندی مورد جستجو
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<int> GetChildOfOneCategory(int categoryId)
        {
            const string sqlQuery = "WITH tblChild AS ( SELECT Id, PId FROM iTblCategory WHERE Id = @p0 " + "" +
                                  "UNION ALL  SELECT iTblCategory.Id, iTblCategory.PId FROM iTblCategory " +
                                  "JOIN tblChild ON iTblCategory.PId = tblChild.Id)" +
                                  "SELECT tblChild.Id FROM  tblChild   OPTION(MAXRECURSION 1000)";
            
            return Context.Database.SqlQuery<int>(sqlQuery, categoryId).ToList();
        }
        
        /// <summary>
        /// دریافت لیست دسته بندی ها به همراه ریشه ی آنها
        /// </summary>
        public List<CategoryIdRoot> GetNodesWithRoot()
        {
            const string sqlQuery =
                "WITH OrganisationChart (Id, [Level], [Root]) AS " +
                "(SELECT Id, 0, Id FROM iTblCategory " +
                "WHERE PId IS NULL UNION ALL " +
                "SELECT emp.Id, [Level] + 1, [Root] " +
                "FROM iTblCategory emp " +
                "INNER JOIN OrganisationChart ON emp.PId = OrganisationChart.Id) " +
                "SELECT* FROM OrganisationChart OPTION(MAXRECURSION 1000)";
                
            var res = Context.Database.SqlQuery<CategoryIdRoot>(sqlQuery).ToList();
            return res;
        }

        public class CategoryIdRoot
        {
            public int Id { get; set; }
            public int Level { get; set; }
            public int Root { get; set; }
        }

    }
}
