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
    public class SpeakerService : GenericRepository<TblSpeaker>, ISpeakerService
    {

        public SpeakerService(DatabaseContext context)
            : base(context)
        {
        }

        public override dynamic GetForEdit(int id, int userId)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var record =
                GetAll()
                    .Where(a => a.Id == id)
                    .Select(a =>
                        new
                        {
                            a.Id,
                            a.FName,
                            a.LName,
                            a.SSActivityId,
                            a.GroupName,
                            a.MobileNumber,
                            a.University,
                            a.Major,
                            a.Grade,
                            a.IsApproved,
                            a.IsPublished,
                            a.ApprovedDate,
                            a.PublishDate,
                            a.UserId,
                            a.TblUser.FullName,
                            a.Priority,
                            ImageUrl = a.TblFileImage.FileUrl,
                            a.ImageFileId,
                            a.CityId,
                            a.TblCity.CityName,
                            ResumeUrl = a.TblFileResume.FileUrl,
                            a.ResumeFileId,
                            a.State,
                            a.Description,

                            TblSpeechFieldIds = a.TblSpeechFields
                                .Select(s =>
                                    s.SSSpeechFieldId),

                            TblSpeechFieldNames = a.TblSpeechFields
                                .Select(s =>
                                    consts
                                        .Where(co => co.Id == s.SSSpeechFieldId)
                                        .Select(co => co.ConstName)
                                        .FirstOrDefault()),

                            a.EducationDescription,
                            a.ActivityDescription,
                            a.MasterDescription,
                            a.RecordsDescription,
                            a.ResearchDescription,
                            a.TeachingDescription,


                        }).FirstOrDefault();

            if (record == null)
            {
                throw new Exception("رکوردی یافت نشد");
            }

            return record;
        }

        //public IEnumerable<dynamic> GetMySpeakers(int userId)
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
                throw new Exception("خطا در حذف اطلاعات");
            }

            if (temp.ImageFileId.HasValue)
            {
                _unitOfWork.FileRepository.Delete(temp.ImageFileId.Value, userId);
            }
            if (temp.ResumeFileId.HasValue)
            {
                _unitOfWork.FileRepository.Delete(temp.ResumeFileId.Value, userId);
            }

            Delete(temp);

        }

        public IEnumerable<dynamic> GetPublishedList(string loginUserRole, int userId, int[] speechFieldIds)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll().Where(a => a.IsPublished);

            if (speechFieldIds != null && speechFieldIds.Any())
            {
                query = query.Where(a =>
                    speechFieldIds == null || a.TblSpeechFields.Any(s => speechFieldIds.Contains(s.SSSpeechFieldId)));
            }

            //if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            //{
            //    query = query.Where(a => a.UserId == userId);
            //}

            var temp = query.OrderByDescending(a => a.Priority)
                .Select(c => new
                {
                    c.Id,
                    c.FName,
                    c.LName,
                    c.SSActivityId,
                    Activity = consts
                        .Where(co => co.Id == c.SSActivityId)
                        .Select(co => co.ConstName)
                        .FirstOrDefault(),
                    //c.MobileNumber,
                    c.GroupName,
                    c.University,
                    c.Major,
                    c.Grade,
                    //c.IsApproved,
                    //c.IsPublished,
                    c.ApprovedDate,
                    c.PublishDate,
                    //c.UserId,
                    c.TblUser.FullName,
                    //c.Priority,
                    ImageUrl = c.TblFileImage.FileUrl,
                    //c.ImageFileId,
                    //c.CityId,
                    c.TblCity.CityName,
                    //ResumeUrl = c.TblFileResume.FileUrl,
                    //c.ResumeFileId,
                    //c.State,
                    //c.Description,

                    TblSpeechFieldIds = c.TblSpeechFields
                    .Select(s =>
                        s.SSSpeechFieldId),

                    TblSpeechFieldNames = c.TblSpeechFields
                    .Select(s =>
                        consts
                            .Where(co => co.Id == s.SSSpeechFieldId)
                            .Select(co => co.ConstName)
                            .FirstOrDefault()),

                    //TblSpeakerRequests = c.TblSpeakerRequests
                    //    .Select(r=> new
                    //    {
                    //        r.Id,
                    //        r.ActivityType,

                    //    })

                });

            return temp;
        }

        public IEnumerable<dynamic> GetDataTable(bool isFromAdminPanel, string loginUserRole, int userId, int[] speechFieldIds)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll();

            if (speechFieldIds != null && speechFieldIds.Any())
            {
                query = query.Where(a =>
                    speechFieldIds == null || a.TblSpeechFields.Any(s => speechFieldIds.Contains(s.SSSpeechFieldId)));
            }

            if (!isFromAdminPanel)
            {
                query = query.Where(a => a.IsPublished);
            }

            if ( loginUserRole != "AppAdmin" && loginUserRole != "Admin" )
            {
                query = query.Where(a => a.UserId == userId);
            }

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    c.FName,
                    c.LName,

                    c.SSActivityId,
                    Activity = consts
                        .Where(co => co.Id == c.SSActivityId)
                        .Select(co => co.ConstName)
                        .FirstOrDefault(),

                    c.MobileNumber,
                    c.University,
                    c.Major,
                    c.Grade,
                    c.IsApproved,
                    c.IsPublished,
                    c.ApprovedDate,
                    c.PublishDate,
                    c.UserId,
                    c.TblUser.FullName,
                    c.Priority,
                    ImageUrl = c.TblFileImage.FileUrl,
                    c.ImageFileId,
                    c.CityId,
                    c.TblCity.CityName,
                    ResumeUrl = c.TblFileResume.FileUrl,
                    c.ResumeFileId,
                    c.State,
                    c.Description,

                    TblSpeechFieldIds = c.TblSpeechFields
                    .Select(s =>
                        s.SSSpeechFieldId),

                    TblSpeechFieldNames = c.TblSpeechFields
                    .Select(s =>
                        consts
                            .Where(co => co.Id == s.SSSpeechFieldId)
                            .Select(co => co.ConstName)
                            .FirstOrDefault()),

                    //TblSpeakerRequests = c.TblSpeakerRequests
                    //    .Select(r=> new
                    //    {
                    //        r.Id,
                    //        r.ActivityType,

                    //    })

                }).OrderByDescending(a => a.Id);

            return temp;
        }

        public IEnumerable<dynamic> GetTopSpeakers(int userId)
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var query = GetAll();

            query = 
                query.Where(a => a.IsApproved && a.IsPublished)
                    .OrderByDescending(a=> a.Priority);

            var temp = query
                .Select(c => new
                {
                    c.Id,
                    c.FName,
                    c.LName,

                    c.SSActivityId,
                    Activity = consts
                        .Where(co => co.Id == c.SSActivityId)
                        .Select(co => co.ConstName)
                        .FirstOrDefault(),

                    c.GroupName,

                    //c.MobileNumber,
                    //c.University,
                    //c.Major,
                    //c.Grade,
                    //c.IsApproved,
                    //c.IsPublished,
                    //c.ApprovedDate,
                    //c.PublishDate,
                    //c.UserId,
                    c.TblUser.FullName,
                    //c.Priority,
                    ImageUrl = c.TblFileImage.FileUrl,
                    //c.ImageFileId,
                    //c.CityId,
                    c.TblCity.CityName,
                    //ResumeUrl = c.TblFileResume.FileUrl,
                    //c.ResumeFileId,
                    //c.State,
                    //c.Description,

                    //TblSpeechFieldIds = c.TblSpeechFields
                    //.Select(s =>
                    //    s.SSSpeechFieldId),

                    TblSpeechFieldNames = c.TblSpeechFields
                    .Select(s =>
                        consts
                            .Where(co => co.Id == s.SSSpeechFieldId)
                            .Select(co => co.ConstName)
                            .FirstOrDefault()),

                    //TblSpeakerRequests = c.TblSpeakerRequests
                    //    .Select(r=> new
                    //    {
                    //        r.Id,
                    //        r.ActivityType,

                    //    })

                });

            return temp;
        }


        /// <summary>
        /// کمبوی سخنران ها
        /// </summary>
        /// <param name="speechFieldId">شناسه زمینه سخنرانی</param>
        public IEnumerable<dynamic> GetSelectOptions(int? speechFieldId)
        {
            var temp = GetAll().Where(a=> a.IsPublished);

            if (speechFieldId.HasValue)
            {
                temp = temp.Where(a => a.TblSpeechFields.Any(b => b.SSSpeechFieldId == speechFieldId));
            }

            var res = temp.OrderByDescending(a => a.Priority).Select(c => new
            {
                c.Id,
                Text = c.SSActivityId == 306 ? (c.GroupName) : (c.FName + " " + c.LName),
                ImageUrl = c.TblFileImage.FileUrl
            });

            return res;
        }

        public void Insert(TblSpeaker entity, int userId, string loginUserRole)
        {
            if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            {
                entity.IsApproved = false;
                entity.IsPublished = false;
            }

            AddSpeechFieldsToSpeaker(entity, userId);
            base.Insert(entity, userId);
        }

        public void Update(TblSpeaker entity, int userId, string loginUserRole)
        {

            var inDB = GetAll().AsNoTracking().Where(a => a.Id == entity.Id).FirstOrDefault();

            if (loginUserRole != "AppAdmin" && loginUserRole != "Admin")
            {
                if (inDB.UserId != userId)
                {
                    throw new Exception("عدم دسترسی");
                }

                entity.IsApproved = false;
                entity.IsPublished = false;

            }

            DeleteSpeakerSpeechFields(entity, userId);
            AddSpeechFieldsToSpeaker(entity, userId);
            base.Update(entity, userId);
        }

        private void DeleteSpeakerSpeechFields(TblSpeaker entity, int userId)
        {

            var fields = Context.TblSpeechFields.Where(a => a.SpeakerId == entity.Id).ToList();

            foreach (var item in fields)
            {
                _unitOfWork.SpeechFieldRepository.Delete(item);
            }
            _unitOfWork.Save();
        }

        private void AddSpeechFieldsToSpeaker(TblSpeaker entity, int userId)
        {
            var speechFieldIds = entity.TblSpeechFieldIds;

            foreach (var item in speechFieldIds)
            {
                var rec = new TblSpeechField()
                {
                    SpeakerId = entity.Id,
                    SSSpeechFieldId = item,
                    State = 1

                };
                _unitOfWork.SpeechFieldRepository.Insert(rec, userId);
                entity.TblSpeechFields.Add(rec);
            }
        }

        
            public IEnumerable<dynamic> SiteSearch(string searchText)
        {

            var consts = _unitOfWork.ConstRepository.GetAll();

            var query1 = 
                GetAll()
                    .Where(a=> a.IsPublished && (a.FName.Contains(searchText) || a.LName.Contains(searchText)) )
                    .Select(a=> new
                    {
                        a.Id,
                        Name = a.FName +" "+ a.LName,
                        ImageUrl = a.TblFileImage.FileUrl,

                        Activity = consts
                            .Where(co => co.Id == a.SSActivityId)
                            .Select(co => co.ConstName)
                            .FirstOrDefault(),
                        Type = 1,// همکار فرهنگی
                    });

            var query2 =
                    _unitOfWork.PostRepository.GetAll()
                    .Where(a => a.IsPublished && a.Title.Contains(searchText))
                    .Select(a => new
                    {
                        a.Id,
                        Name = a.Title,
                        ImageUrl = a.TblFileImage.FileUrl,
                        Activity=  (a.SSPostType == 203? "فیلم" : "خبر"),
                        Type = 2,// اخبار
                    });

            var resQuery = query1.Union(query2);

            return resQuery;
        }

    }
}