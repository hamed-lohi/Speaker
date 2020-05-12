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
    public class PermissionService : GenericRepository<TblPermission>, IPermissionService
    {

        public PermissionService(DatabaseContext context)
            : base(context)
        {
        }

        //public override dynamic GetForEdit(int id, int userId)
        //{
        //    var consts = _unitOfWork.ConstRepository.GetAll();

        //    var record =
        //        GetAll()
        //            .Where(a => a.Id == id)
        //            .Select(a =>
        //                new
        //                {
        //                    a.Id,
        //                    a.FName,
        //                    a.LName,
        //                    a.SSActivityId,
        //                    a.GroupName,
        //                    a.MobileNumber,
        //                    a.University,
        //                    a.Major,
        //                    a.Grade,
        //                    a.IsApproved,
        //                    a.IsPublished,
        //                    a.ApprovedDate,
        //                    a.PublishDate,
        //                    a.UserId,
        //                    a.TblUser.FullName,
        //                    a.Priority,
        //                    ImageUrl = a.TblFileImage.FileUrl,
        //                    a.ImageFileId,
        //                    a.CityId,
        //                    a.TblCity.CityName,
        //                    ResumeUrl = a.TblFileResume.FileUrl,
        //                    a.ResumeFileId,
        //                    a.State,
        //                    a.Description,

        //                    TblSpeechFieldIds = a.TblSpeechFields
        //                        .Select(s =>
        //                            s.SSSpeechFieldId),

        //                    TblSpeechFieldNames = a.TblSpeechFields
        //                        .Select(s =>
        //                            consts
        //                                .Where(co => co.Id == s.SSSpeechFieldId)
        //                                .Select(co => co.ConstName)
        //                                .FirstOrDefault()),

        //                    a.EducationDescription,
        //                    a.ActivityDescription,
        //                    a.MasterDescription,
        //                    a.RecordsDescription,
        //                    a.ResearchDescription,
        //                    a.TeachingDescription,


        //                }).FirstOrDefault();

        //    if (record == null)
        //    {
        //        throw new Exception("رکوردی یافت نشد");
        //    }

        //    return record;
        //}

        //public IEnumerable<dynamic> GetMyPermissions(int userId)
        //{

        //    var res = DbSet.AsNoTracking().Where(p => p.UserId == userId && p.State != 5)// && (p.UpdateDate >= updateDate)
        //        .Select(c => new
        //        {
        //            c.Id,
        //            //c.Title,
        //            //c.Price, //Price = c.Price?? 0,
        //            //c.ApprovedDate,
        //            //c.SSTradeType,
        //            c.State,
        //            //c.ImageUrl,//.Split(',').FirstOrDefault(),
        //            c.UpdateDate
        //        }).ToList();

        //    var result = res.Select(c =>
        //        new
        //        {
        //            c.Id,
        //            //c.Title,
        //            //c.Price,
        //            //c.ApprovedDate,
        //            //c.SSTradeType,
        //            c.State,
        //            //ImageUrl = c.ImageUrl.Split(',').FirstOrDefault(),
        //            c.UpdateDate
        //        });

        //    return result;
        //}

        //public void Delete(int id, int userId)
        //{
        //    //TblPermission entityToDelete = DbSet.FirstOrDefault(p=> p.Id == id && p.UserId == userId);
        //    //if (entityToDelete == null)
        //    //{
        //    //    throw new Exception("امکان حذف وجود ندارد");
        //    //}
        //    ////if user deleted
        //    //entityToDelete.State = 4; // Deleted
        //    //base.Update(entityToDelete, userId);

        //    //// if admin deleted
        //    ////base.Delete(entityToDelete);

        //    var temp = GetById(id);
        //    if (temp == null)
        //    {
        //        throw new Exception("خطا در حذف اطلاعات");
        //    }

        //    if (temp.ImageFileId.HasValue)
        //    {
        //        _unitOfWork.FileRepository.Delete(temp.ImageFileId.Value, userId);
        //    }
        //    if (temp.ResumeFileId.HasValue)
        //    {
        //        _unitOfWork.FileRepository.Delete(temp.ResumeFileId.Value, userId);
        //    }

        //    Delete(temp);

        //}

        public IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, string loginUserRole, int userId, int[] speechFieldIds)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll();

            if (speechFieldIds != null && speechFieldIds.Any())
            {
                query = query.Where(a =>
                    speechFieldIds == null);
            }

            if (!isFromAdminPanel)
            {
                query = query.Where(a => a.Delete);
            }

            if ( loginUserRole != "AppAdmin" && loginUserRole != "Admin" )
            {
                query = query.Where(a => a.UserId == userId);
            }

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    Activity = consts
                        .Where(co => co.Id == c.SSFormId)
                        .Select(co => co.ConstName)
                        .FirstOrDefault(),

                    c.UserId,
                    c.TblUser.FullName,
                    c.State,

                }).OrderByDescending(a => a.Id);

            return temp;
        }

        public void Insert(TblPermission entity, int userId, string loginUserRole)
        {
            if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            {
                //entity.IsApproved = false;
                //entity.IsPublished = false;
            }

            base.Insert(entity, userId);
        }

        public void Update(TblPermission entity, int userId, string loginUserRole)
        {

            var inDB = GetAll().AsNoTracking().Where(a => a.Id == entity.Id).FirstOrDefault();

            if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            {
                if (inDB.UserId != userId)
                {
                    throw new Exception("عدم دسترسی");
                }

                //entity.IsApproved = false;
                //entity.IsPublished = false;

            }
            base.Update(entity, userId);
        }

    }
}