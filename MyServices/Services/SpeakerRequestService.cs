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
    public class SpeakerRequestService : GenericRepository<TblSpeakerRequest>, ISpeakerRequestService
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public SpeakerRequestService(DatabaseContext context)
            : base(context)
        {
        }

        public void Delete(int id, int userId)
        {
            //TblSpeaker entityToDelete = DbSet.FirstOrDefault(p=> p.Id == id && p.UserId == userId);
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
                throw new Exception("خطا در ثبت اطلاعات");
            }

            Delete(temp);

        }

        public IEnumerable<dynamic> GetDataTable(string loginUserRole, int userId)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll();

            if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            {
                query = query.Where(a => a.UserId == userId);
            }

            var temp = query.Select(c => new
            {
                c.Id,
                ImageUrl = c.TblSpeaker.TblFileImage.FileUrl,
                SpeakerFullName = (c.TblSpeaker.FName + " " + c.TblSpeaker.LName),
                UserFullName = c.TblUser.FullName,

                c.TblSpeaker.SSActivityId,
                Activity = consts
                    .Where(co => co.Id == c.TblSpeaker.SSActivityId)
                    .Select(co => co.ConstName)
                    .FirstOrDefault(),

                //c.SSSubject,
                c.CompanyName,
                c.ActivityType,
                c.Priority,
                c.TblCity.CityName,
                //c.Adress,
                c.ResponsibleName,
                c.ResponsibleMobile,
                c.PhoneNumber,
                //c.Email,
                //c.SiteUrl,
                c.IndicatorActivity,
                c.ExactSubject,
                c.SpeakDate,
                //c.CooperationCode,
                //c.State,
                //c.Description,
                //c.CityId,


                //TblSpeechFieldIds = c.TblSpeechFields
                //    .Select(s =>
                //        s.SSSpeechFieldId),

                //TblSpeechFieldNames = c.TblSpeechFields
                //    .Select(s =>
                //        consts
                //            .Where(co => co.Id == s.SSSpeechFieldId)
                //            .Select(co => co.ConstName)
                //            .FirstOrDefault()),

            }).OrderByDescending(a=> a.Id);

            return temp;
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
                            ImageUrl = a.TblSpeaker.TblFileImage.FileUrl,
                            SpeakerFullName = (a.TblSpeaker.FName + " " + a.TblSpeaker.LName),
                            UserFullName = a.TblUser.FullName,

                            a.TblSpeaker.SSActivityId,
                            a.SpeakerId,
                            a.SSSubject,
                            a.CompanyName,
                            a.ActivityType,
                            a.Priority,
                            a.TblCity.CityName,
                            a.Adress,
                            a.ResponsibleName,
                            a.ResponsibleMobile,
                            a.PhoneNumber,
                            a.Email,
                            a.SiteUrl,
                            a.IndicatorActivity,
                            a.ExactSubject,
                            a.SpeakDate,
                            a.CooperationCode,
                            a.State,
                            a.Description,
                            a.CityId,


                        }).FirstOrDefault();

            if (record == null)
            {
                throw new Exception("رکوردی یافت نشد");
            }

            return record;
        }

        public dynamic GetDefaultInfo(int userId)
        {

            var info = _unitOfWork.UserInfoRepository.GetAll()
                .Where(a => a.UserId == userId)
                    .Select(a => new
                    {
                        //SpeakerFullName = (a.TblSpeaker.FName + " " + a.TblSpeaker.LName),
                        //UserFullName = a.TblUser.FullName,

                        a.CompanyName,
                        a.ActivityType,
                        a.CityId,
                        //a.TblCity.CityName,
                        a.PhoneNumber,
                        a.Adress,
                        a.ResponsibleName,
                        ResponsibleMobile = a.ResponsibleMobile,
                        a.TblUser.Email,
                        a.SiteUrl,

                    }).FirstOrDefault();

            if (info == null)
            {
                throw new Exception("در صورت تکمیل اطلاعات پروفایل ، فیلدها به صورت خودکار پر می شوند");
            }

            return info;
        }

        public override void Insert(TblSpeakerRequest entity, int userId)
        {
            var hasInfo = _unitOfWork.UserInfoRepository.GetAll().Any(a => a.UserId == userId);

            if (!hasInfo)
            {
                var newInfo = new TblUserInfo
                {
                    UserId = userId,
                    //FName = "",
                    //LName = "",
                    CompanyName = entity.CompanyName,
                    ActivityType = entity.ActivityType,
                    CityId = entity.CityId,
                    //entity.TblCity.CityName,
                    PhoneNumber = entity.PhoneNumber,
                    Adress = entity.Adress,
                    ResponsibleName = entity.ResponsibleName,
                    ResponsibleMobile = entity.ResponsibleMobile,
                    //entity.TblUser.Email,
                    SiteUrl = entity.SiteUrl,
                };

                _unitOfWork.UserInfoRepository.Insert(newInfo, userId);
            }

            //AddSpeechFieldsToSpeaker(entity, userId);
            base.Insert(entity, userId);
        }

        public override void Update(TblSpeakerRequest entity, int userId)
        {
            //AddSpeechFieldsToSpeaker(entity, userId);
            base.Update(entity, userId);
        }

        //public IEnumerable<dynamic> GetMySpeakerRequests(int userId)
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

    }
}