using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyModels.Entity;
using MyServices.Base;

namespace MyServices.Interfaces
{
    public interface IPostService : IBaseService<TblPost>
    {

        IEnumerable<dynamic> GetPostsList(int userId, int SSPostType);
        IEnumerable<dynamic> GetPostsForIndexPage(int userId, int SSPostType);
        IEnumerable<dynamic> GetDataTable(int userId);
        dynamic GetForEdit(int id, int userId);
        IEnumerable<dynamic> GetMyPosts(int userId);//, long updateDate
        void Delete(int id, int userId);
        dynamic GetById(long id);
        TblPost GetByIdMain(long id, int userId);

        //IEnumerable<dynamic> GetAll();

        IEnumerable<dynamic> GetSimilar(long id);
        IEnumerable<dynamic> GetNewest(int? pageNumber, int? cityId);
        IEnumerable<dynamic> GetMostPopular(int? pageNumber, int? cityId);
        IEnumerable<dynamic> Getbillboard(int? cityId, int? categoryId);

        /// <summary>
        /// دریافت خوش شانس ترین آگهی ها
        /// </summary>
        IEnumerable<dynamic> GetLucky(int? cityId);
    }
}