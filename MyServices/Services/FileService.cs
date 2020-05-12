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
    public class FileService :GenericRepository<TblFile>, IFileService
    {

        public FileService(DatabaseContext context)
            : base(context)
        {
        }

        public IEnumerable<dynamic> GetMyFiles(int userId)
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

        public override void Delete(int id, int userId)
        {
            //TblFile entityToDelete = DbSet.FirstOrDefault(p=> p.Id == id && p.UserId == userId);
            //if (entityToDelete == null)
            //{
            //    throw new Exception("امکان حذف وجود ندارد");
            //}
            //if user deleted
            //entityToDelete.State = 4; // Deleted
            //base.Update(entityToDelete, userId);

            // if admin deleted
            //base.Delete(entityToDelete);

            var temp = GetById(id);
            if (temp == null)
            {
                throw new Exception("امکان حذف وجود ندارد");
            }

            if (!string.IsNullOrEmpty(temp.FilePath))
            {

                if (System.IO.File.Exists(temp.FilePath))
                {
                    System.IO.File.Delete(temp.FilePath);
                }
            }

            temp.TblImageSpeakers.Clear();
            temp.TblResumeSpeakers.Clear();
            temp.TblConsts.Clear();
            temp.TblPosts.Clear();
            temp.TblUserInfos.Clear();

            Delete(temp);

        }

        public IEnumerable<dynamic> GetDataTable()
        {
            var consts = _unitOfWork.ConstRepository.GetAll();

            var temp = DbSet.Select(c => new
            {
                c.Id,
                //c.FName,
                //c.LName,
                //c.MobileNumber,
                //c.University,
                //c.Major,
                //c.Grade,
                //c.IsApproved,
                //c.IsPublished,
                //c.ApprovedDate,
                //c.PublishDate,
                c.UserId,
                c.TblUser.FullName,
                c.Priority,
                //c.ImageUrl,
                //c.CityId,
                //c.TblCity.CityName,
                //c.ResumeUrl,
                c.State,
                c.Description,

                //TblSpeechFieldIds = c.TblSpeechFields
                //    .Select(s=>
                //        s.SSSpeechFieldId),

                //TblSpeechFieldNames = c.TblSpeechFields
                //    .Select(s =>
                //        consts
                //            .Where(co=> co.Id == s.SSSpeechFieldId)
                //            .Select(co=> co.ConstName)
                //            .FirstOrDefault()),

            });

            return temp;
        }

        public override void Insert(TblFile entity, int userId)
        {
            AddSpeechFieldsToFile(entity, userId);
            base.Insert(entity, userId);
        }

        public override void Update(TblFile entity, int userId)
        {
            AddSpeechFieldsToFile(entity, userId);
            base.Update(entity, userId);
        }

        private void AddSpeechFieldsToFile(TblFile entity, int userId)
        {
            //var speechFieldIds = entity.TblSpeechFieldIds;

            //foreach (var item in speechFieldIds)
            //{
            //    var rec = new TblSpeechField()
            //    {
            //        SSSpeechFieldId = item,
            //        State = 1

            //    };
            //    _unitOfWork.SpeechFieldRepository.Insert(rec, userId);
            //    entity.TblSpeechFields.Add(rec);
            //}
        }

    }
}