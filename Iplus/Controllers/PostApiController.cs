using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyModels.DAL;
using MyModels.Entity;

namespace Iplus.Controllers
{
    //[Authorize(Users = "Alice,Bob")]
    //[Authorize(Roles = "Administrators")]
    //[Authorize]
    public class PostApiController : BaseApiController
    {

        public PostApiController()//IPostService iPostService
        {
            //_PostService = iPostService;
        }

        //public IEnumerable<TblPost> GetAll()
        //{
        //    var temp = unitOfWork.PostRepository.GetAll();
        //    return temp.ToList();
        //}

        public IEnumerable<dynamic> GetPostsList(int SSPostType)
        {
            var temp = unitOfWork.PostRepository.GetPostsList(loginUserId, SSPostType);
            return temp;
        }

        public IEnumerable<dynamic> GetPostsForIndexPage(int SSPostType)
        {
            var temp = unitOfWork.PostRepository.GetPostsForIndexPage(loginUserId, SSPostType);
            return temp;
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public IEnumerable<dynamic> GetDataTable()
        {
            var temp = unitOfWork.PostRepository.GetDataTable(loginUserId);
            return temp;
        }

        public IHttpActionResult GetForEdit(int id)
        {
            try
            {
                var post = unitOfWork.PostRepository.GetForEdit(id, loginUserId);

                return Ok(post);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Authorize]
        [Description("دریافت آگهی های کاربر وارد شده")]
        public IEnumerable<dynamic> GetMyPosts()
        {
            var temp = unitOfWork.PostRepository.GetMyPosts(User.Identity.GetUserId<int>());
            return temp;
        }
        
        [Description("دریافت لیست آگهی ها با مقادیر پیش فرض برای پارامترها")]
        public IEnumerable<dynamic> GetAllDefault()
        {

            var temp = unitOfWork.PostRepository.GetAll();
            return temp;
        }

        public IHttpActionResult GetById(long id)
        {
            try
            {
                var tblPost = unitOfWork.PostRepository.GetById(id);

                unitOfWork.Save();

                return Ok(tblPost);
            }
            catch (Exception e)
            {
                //Code.ManageExceptions.HandleException(e);
                //Code.ManageExceptions.CustomException("عدم وجود", HttpStatusCode.BadRequest);
                return BadRequest(e.Message);
            }
            
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public async Task<IHttpActionResult> Save(TblPost newRecord)
        {

            //newRecord.ViewCount.ApprovedDate = utility.Date.GetDateTime.CurrentTimeSeconds(); // test

            //ModelState.Remove("ViewCount");
            //ModelState.Remove("ApprovedDate");
            //ModelState.Remove("UserId");
            //ModelState.Remove("State");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                newRecord.UserId = loginUserId;

                if (newRecord.Id == 0) // Insert
                {
                    unitOfWork.PostRepository.Insert(newRecord, loginUserId);
                }
                else // Update
                {
                    unitOfWork.PostRepository.Update(newRecord, loginUserId);
                }

                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                unitOfWork.PostRepository.Delete(id, loginUserId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return BadRequest("در عملیات حذف خطایی رخ داده است");
            }

            return Ok();
        }

        [Description("دریافت آگهی های مشابه بر اساس شناسه یک آگهی")]
        public IEnumerable<dynamic> GetSimilar(long id) //   limit = 4
        {
            try
            {
                return unitOfWork.PostRepository.GetSimilar(id);
            }
            catch (Exception)
            {
                return null;
            }

        }


        [Description("دریافت جدیدترین آگهی ها")]
        public IEnumerable<dynamic> GetNewest(int? cityId, int ? pageNumber = null)
        {
            try
            {
                return unitOfWork.PostRepository.GetNewest(pageNumber, cityId);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [Description("دریافت محبوب ترین آگهی ها")]
        public IEnumerable<dynamic> GetMostPopular(int? cityId, int? pageNumber = null)
        {
            try
            {
                return unitOfWork.PostRepository.GetMostPopular(pageNumber, cityId);
            }
            catch (Exception e)
            {
                return null;
            }

        }


        [Authorize]
        //[ResponseType(typeof(TblPost))]
        [Description("فعال کردن آگهی")]
        public async Task<IHttpActionResult> GetActivePost(long id)
        {
            try
            {
                unitOfWork.PostRepository.GetByIdMain(id , User.Identity.GetUserId<int>());
                //tblPost.State = 2; // 2:InQueue
                //await PostSave(tblPost);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }


        [Description("دریافت آگهی های بیلبورد")]
        public IEnumerable<dynamic> Getbillboard(int? cityId, int? categoryId)
        {
            try
            {
                return unitOfWork.PostRepository.Getbillboard(cityId, categoryId);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [Description("دریافت آگهی های تصادفی")]
        public IEnumerable<dynamic> GetLucky(int? cityId)
        {
            try
            {
                return unitOfWork.PostRepository.GetLucky(cityId);
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}