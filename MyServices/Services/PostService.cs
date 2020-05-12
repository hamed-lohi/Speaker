using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using MyModels.DAL;
using MyModels.Entity;
using MyServices.Base;
using MyServices.DAL;
using MyServices.Interfaces;

namespace MyServices.Services
{
    public class PostService :GenericRepository<TblPost>, IPostService
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public PostService(DatabaseContext context)
            : base(context)
        {
        }

        public IEnumerable<dynamic> GetPostsList(int userId, int SSPostType)
        {

            var query =
                GetAll(a => a.IsPublished && a.SSPostType == SSPostType)
                    .OrderByDescending(a => a.InsertDate)
                    .ThenByDescending(a => a.Priority);

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    ImageUrl = c.TblFileImage.FileUrl,
                    c.Summary,

                    //c.IsPublished,
                    //c.PublishDate,
                    //c.TblUser.FullName,
                    //c.Priority,
                    //c.ViewCount,


                });

            return temp;
        }

        public IEnumerable<dynamic> GetPostsForIndexPage(int userId, int SSPostType)
        {

            var query = 
                GetAll(a=> a.IsPublished && a.SSPostType == SSPostType)
                    .OrderByDescending(a=> a.InsertDate)
                    .ThenByDescending(a=> a.Priority).Take(5);

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    ImageUrl = c.TblFileImage.FileUrl,

                    //c.IsPublished,
                    //c.PublishDate,
                    //c.TblUser.FullName,
                    //c.Priority,
                    //c.ViewCount,

                    
                });

            return temp;
        }

        public IEnumerable<dynamic> GetDataTable(int userId)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll();

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    ImageUrl = c.TblFileImage.FileUrl,

                    c.IsPublished,
                    //c.PublishDate,
                    c.TblUser.FullName,
                    c.Priority,
                    c.ViewCount,

                    PostType = consts
                                .Where(co => co.Id == c.SSPostType)
                                .Select(co => co.ConstName)
                                .FirstOrDefault(),

                });

            return temp;
        }

        public dynamic GetById(long id)
        {
            var mainPost = 
                DbSet
                .Include(d => d.TblUser)
                .Include(d=> d.TblFileImage)
                    .FirstOrDefault(p => p.Id == id);//&& p.State == 1

            if (mainPost == null)
            {
                throw new Exception(utility.ResourceMessage.Message.PostNotFound);
            }

            mainPost.ViewCount++;

            var post = new
            {
                mainPost.Id,
                ImageUrl = mainPost.TblFileImage.FileUrl,
                mainPost.Title,
                mainPost.ApprovedDate,
                mainPost.PublishDate,
                mainPost.State,
                mainPost.Summary,
                mainPost.Text,
                mainPost.ViewCount,
                //mainPost.Priority,
                //mainPost.TblUser.FullName,
            };

            return post;

        }

        public override dynamic GetForEdit(int id, int userId)
        {
            var record =
                GetAll()
                    .Where(a => a.Id == id)
                    .Select(a =>
                        new
                        {
                            a.Id,
                            a.Title,
                            a.Summary,
                            a.Text,
                            a.IsPublished,
                            a.ApprovedDate,
                            a.PublishDate,
                            //a.UserId,
                            a.TblUser.FullName,
                            a.Priority,
                            ImageUrl = a.TblFileImage.FileUrl,
                            a.ImageFileId,
                            a.State,
                            a.ViewCount,

                            a.SSPostType

                        }).FirstOrDefault();

            if (record == null)
            {
                throw new Exception("رکوردی یافت نشد");
            }

            return record;
        }

        public IEnumerable<dynamic> GetMyPosts(int userId)
        {

            var res = DbSet.AsNoTracking().Where(p => p.UserId == userId && p.State != 5)// && (p.UpdateDate >= updateDate)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.ApprovedDate,
                    c.State,
                    c.UpdateDate
                }).ToList();

            var result = res.Select(c =>
                new
                {
                    c.Id,
                    c.Title,
                    c.ApprovedDate,
                    c.State,
                    c.UpdateDate
                });

            return result;
        }

        public override void Insert(TblPost entity, int userId)
        {

            //// روز گذشته
            //var lastDay = utility.Date.GetDateTime.CurrentTimeSeconds() - 86400;
            //// اگر تعداد آگهی های ثبت شده توسط کاربر بیش از 3 عدد در 24 ساعت گذشته باشد خطا رخ خواهد داد
            //var postLimitation = DbSet.Count(p => p.UserId == userId && p.InsertDate > lastDay) > 2;
            //if (postLimitation)
            //{
            //    throw new Exception(utility.ResourceMessage.Message.SavePostLimitation);
            //}


            entity.UserId = userId;
            base.Insert(entity, userId);
        }

        public override void Update(TblPost entity, int userId)
        {
            var entityToUpdate = entity;
            entityToUpdate.State = 2; // InQueue
            entityToUpdate.UserId = userId;

            entityToUpdate.ActionType = 2;// update
            entityToUpdate.UpdateDate = utility.Date.GetDateTime.CurrentTimeSeconds();
            entityToUpdate.UpdateUserId = userId;

            DbSet.Attach(entityToUpdate);
            var record = Context.Entry(entityToUpdate);//.State = EntityState.Modified;
            record.State = EntityState.Modified;

            record.Property(p => p.InsertUserId).IsModified = false;
            record.Property(p => p.InsertDate).IsModified = false;

            record.Property(p => p.ApprovedDate).IsModified = false;
            record.Property(p => p.ViewCount).IsModified = false;
            record.Property(p => p.Priority).IsModified = false;

        }

        public TblPost GetByIdMain(long id, int userId)
        {
            var mainPost = DbSet.SingleOrDefault(p => p.Id == id && p.UserId == userId && (p.State == 4  || p.State == 3));

            if (mainPost == null)
            {
                throw new Exception(utility.ResourceMessage.Message.PostNotFound);
            }
            mainPost.State = 2;
            return mainPost;

        }

        public void Delete(int id, int userId)
        {

            var temp = base.GetById(id);
            if (temp == null)
            {
                throw new Exception("خطا در حذف اطلاعات");
            }

            if (temp.ImageFileId.HasValue)
            {
                _unitOfWork.FileRepository.Delete(temp.ImageFileId.Value, userId);
            }

            Delete(temp);

            // if admin deleted
            //base.Delete(entityToDelete);

        }

        //public IEnumerable<dynamic> GetAll()
        //{

        //    var temp = DbSet.AsNoTracking().Where(p => p.State == 1);// پستهای تایید شده
            
        //    // مرتب سازی بر اساس فیلد اولویت و تاریخ آپدیت
        //    var res = temp.OrderByDescending(a=> a.Priority).ThenByDescending(a=> a.UpdateDate).Select(c => new
        //    {
        //        c.Id,
        //        c.Title,
        //    }).ToList();

        //    var result = res.Select(c => new
        //    {
        //        c.Id,
        //        c.Title,
        //    });


        //    return result;
        //}

        /// <summary>
        /// دریافت آگهی های مشابه
        /// </summary>
        /// <param name="id"> شناسه آگهی</param>
        public IEnumerable<dynamic> GetSimilar(long id)
        {
            var record = DbSet.Find(id);
            return DbSet.AsNoTracking()
                .Where(p=> p.Id != id && p.State == 1 && 
                        p.Title.Contains(record.Title))
                .Take(4).AsEnumerable().Select(p => new
                {
                    p.Id,
                    p.Title,
                });

        }

        /// <summary>
        /// دریافت جدیدترین آگهی ها
        /// </summary>
        /// <param name="pageNumber"> شماره صفحه ، اگر خالی باشد 4 تا برمیگرداند</param>
        public IEnumerable<dynamic> GetNewest(int? pageNumber, int? cityId)
        {

            var result = DbSet.Where(p=> p.State == 1).OrderByDescending(p => p.ApprovedDate)
                .Select(c => new
            {
                c.Id,
                c.Title,
                });


            if (!pageNumber.HasValue)
            {
                var res10 = result.Take(10).ToList()
                    .Select(c => new
                    {
                        c.Id,
                        c.Title,
                    });

                return res10;
            }

            var start = pageNumber.Value * 20;
            var res20 = result.Skip(start).Take(20).ToList()
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                });

            return res20;
        }

        /// <summary>
        /// دریافت آگهی های پر بازدید
        /// </summary>
        /// <param name="pageNumber"> شماره صفحه ، اگر خالی باشد 10 تا برمیگرداند</param>
        public IEnumerable<dynamic> GetMostPopular(int? pageNumber, int? cityId)
        {
            var lastDay = utility.Date.GetDateTime.DifferenceDay(-1); //utility.Date.GetDateTime.CurrentTimeSeconds()-86400; // 24 ساعت گذشته

            var result = DbSet.Where(p => p.State == 1 && p.ApprovedDate >= lastDay).OrderByDescending(p => p.ViewCount)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                });


            if (!pageNumber.HasValue)
            {
                var res10 = result.Take(10).ToList()
                    .Select(c => new
                    {
                        c.Id,
                        c.Title,
                    });

                return res10;
            }

            var start = pageNumber.Value * 20;
            var res20 = result.Skip(start).Take(20).ToList()
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                });

            return res20;
        }


        /// <summary>
        /// دریافت محبوب ترین آگهی ها
        /// </summary>
        /// <param name="pageNumber"> شماره صفحه ، اگر خالی باشد 4 تا برمیگرداند</param>
        public IEnumerable<dynamic> Getbillboard( int? cityId , int? categoryId)//int? pageNumber,
        {
            var currentTime = utility.Date.GetDateTime.CurrentTimeSeconds();

            var result = DbSet.Where(p => p.State == 1  )
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                });

            if (categoryId.HasValue)// بیلبورد دسته بندی
            {
                //// دریافت شناسه دسته بندی های زیر مجموعه ی دسته بندی مورد جستجو
                var childIds = _unitOfWork.CategoryRepository.GetChildOfOneCategory(categoryId.Value);

                result =
                    result
                    .OrderByDescending(b => b.Id);
                
                var res20 = result.Take(20).ToList()
                        .Select(c => new
                        {
                            c.Id,
                            c.Title,
                        });

                    return res20;
                

            }
            else // بیلبورد اصلی
            {

                result = result.OrderByDescending(b => b.Id);
                
                    var resC20 = result.Take(20).ToList()
                        .Select(c => new
                        {
                            c.Id,
                            c.Title,
                        });

                    return resC20;
                
            }

        }

        /// <summary>
        /// دریافت خوش شانس ترین آگهی ها
        /// </summary>
        public IEnumerable<dynamic> GetLucky(int? cityId)
        {
            var lastDay = utility.Date.GetDateTime.DifferenceDay(-1);//utility.Date.GetDateTime.CurrentTimeSeconds() - 86400; // 24 ساعت گذشته

            // دریافت لیست دسته بندی ها به همراه ریشه
            var childIdsAndRoot = _unitOfWork.CategoryRepository.GetNodesWithRoot();

            var result = DbSet.Where(p => p.State == 1 && p.ApprovedDate >= lastDay )
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                }).ToList();

            var list = new List<dynamic>();
            
            // املاک
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 1)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // وسائط نقلیه
            list.Add(result.OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // الکترونیکی
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 11)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // لوازم خانه
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 16)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // لوازم شخصی
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 21)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // استخدام
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 28)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // کسب و کار
            list.Add(result.Where(s=> childIdsAndRoot.Any(a=> a.Root == 41)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
            {
                c.Id,
                c.Title,
            })
            );

            // گیاهان و حیوانات
            list.Add(result.Where(s => childIdsAndRoot.Any(a => a.Root == 50)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                })
            );

            // ورزش و سرگرمی
            list.Add(result.Where(s => childIdsAndRoot.Any(a => a.Root == 55)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                })
            );

            // خدمات
            list.Add(result.Where(s => childIdsAndRoot.Any(a => a.Root == 1047)).OrderBy(r => Guid.NewGuid()).Take(1)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                })
            );

            return list;

        }

    }
}