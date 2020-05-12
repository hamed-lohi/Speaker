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

    /// <summary>
    /// اطلاعات کاربر - سرویس
    /// </summary>
    public class UserInfoService : GenericRepository<TblUserInfo>, IUserInfoService
    {

        public UserInfoService(DatabaseContext context)
            : base(context)
        {
        }

        public dynamic GetForEditMyInfo(int userId)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var record =
                GetAll()
                    .Where(a => a.UserId == userId)
                    .Select(a =>
                        new
                        {
                            a.Id,
                            a.TblUser.FullName,
                            //a.FName,
                            //a.LName,
                            a.ImageFileId,
                            a.CompanyName,
                            a.ActivityType,
                            a.CityId,
                            a.Adress,
                            a.ResponsibleName,
                            a.PhoneNumber,
                            a.SiteUrl,
                            a.UserId,
                            ImageUrl = a.TblFileImage.FileUrl,

                        })
                    .FirstOrDefault();

            //if (record == null)
            //{
            //    throw new Exception("کاربری یافت نشد");
            //}

            return record;
        }

        public IEnumerable<dynamic> GetMyUserInfos(int userId)
        {

            var res = DbSet.AsNoTracking().Where(p => p.UserId == userId && p.State != 5)// && (p.UpdateDate >= updateDate)
                .Select(c => new
                {
                    c.Id,
                    //c.Title,
                    //c.Price, //Price = c.Price?? 0,
                    //c.ApprovedDate,
                    //c.SSTradeType,
                    c.State,
                    //c.ImageUrl,//.Split(',').FirstOrDefault(),
                    c.UpdateDate
                }).ToList();

            var result = res.Select(c =>
                new
                {
                    c.Id,
                    //c.Title,
                    //c.Price,
                    //c.ApprovedDate,
                    //c.SSTradeType,
                    c.State,
                    //ImageUrl = c.ImageUrl.Split(',').FirstOrDefault(),
                    c.UpdateDate
                });

            return result;
        }

        public void Delete(int id, int userId)
        {
            //TblUserInfo entityToDelete = DbSet.FirstOrDefault(p=> p.Id == id && p.UserId == userId);
            //if (entityToDelete == null)
            //{
            //    throw new Exception("امکان حذف وجود ندارد");
            //}
            ////if user deleted
            //entityToDelete.State = 4; // Deleted
            //base.Update(entityToDelete, userId);

            //// if admin deleted
            ////base.Delete(entityToDelete);

            var temp = GetById(id);
            if (temp == null)
            {
                throw new Exception("خطا در حذف اطلاعات");
            }

            if (temp.ImageFileId.HasValue)
            {
                _unitOfWork.FileRepository.Delete(temp.ImageFileId.Value, userId);
            }

            Delete(temp);

        }

        //public IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, int userId, int[] speechFieldIds)
        //{
        //    var consts = _unitOfWork.ConstRepository.GetAll();

        //    var query = GetAll();

        //    if (speechFieldIds != null && speechFieldIds.Any())
        //    {
        //        query = query.Where(a =>
        //            speechFieldIds == null || a.TblSpeechFields.Any(s => speechFieldIds.Contains(s.SSSpeechFieldId)));
        //    }

        //    if (!isFromAdminPanel)
        //    {
        //        query = query.Where(a => a.IsPublished);
        //    }

        //    var temp = query
        //        .Select(c => new
        //        {
        //            c.Id,
        //            c.FName,
        //            c.LName,
        //            c.MobileNumber,
        //            c.University,
        //            c.Major,
        //            c.Grade,
        //            c.IsApproved,
        //            c.IsPublished,
        //            c.ApprovedDate,
        //            c.PublishDate,
        //            c.UserId,
        //            c.TblUser.FullName,
        //            c.Priority,
        //            ImageUrl = c.TblFileImage.FileUrl,
        //            c.ImageFileId,
        //            c.CityId,
        //            c.TblCity.CityName,
        //            ResumeUrl = c.TblFileResume.FileUrl,
        //            c.ResumeFileId,
        //            c.State,
        //            c.Description,

        //            TblSpeechFieldIds = c.TblSpeechFields
        //            .Select(s =>
        //                s.SSSpeechFieldId),

        //            TblSpeechFieldNames = c.TblSpeechFields
        //            .Select(s =>
        //                consts
        //                    .Where(co => co.Id == s.SSSpeechFieldId)
        //                    .Select(co => co.ConstName)
        //                    .FirstOrDefault()),

        //        });

        //    return temp;
        //}

        /// <summary>
        /// کمبوی سخنران ها
        /// </summary>
        /// <param name="speechFieldId">شناسه زمینه سخنرانی</param>
        //public IEnumerable<dynamic> GetSelectOptions(int? speechFieldId)
        //{
        //    var temp = GetAll();

        //    if (speechFieldId.HasValue)
        //    {
        //        temp = temp.Where(a => a.TblSpeechFields.Any(b => b.SSSpeechFieldId == speechFieldId));
        //    }


        //    var res = temp.OrderBy(a => a.Priority).Select(c => new
        //    {
        //        c.Id,
        //        Text = c.FName + " " + c.LName,
        //        ImageUrl = c.TblFileImage.FileUrl
        //    });

        //    return res;
        //}

        public override void Insert(TblUserInfo entity, int userId)
        {
            base.Insert(entity, userId);
        }

        public override void Update(TblUserInfo entity, int userId)
        {
            base.Update(entity, userId);
        }

    }
}