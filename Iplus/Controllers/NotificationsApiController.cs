using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyModels.Entity;
using MyServices.Interfaces;

namespace Iplus.Controllers
{
    public class NotificationsApiController : BaseApiController
    {

        public NotificationsApiController()//INotificationsService iNotificationsService
        {
            //_NotificationsService = iNotificationsService;
        }

        public IEnumerable<TblNotifications> GetAll()
        {
            var temp = unitOfWork.NotificationsRepository.GetAll();
            return temp.ToList();
        }

        /// <summary>
        /// دریافت آخرین تغییرات اطلاعیه ها
        /// </summary>
        /// <param name="updateDate"> مقدار آخرین به روز رسانی</param>
        [Description("دریافت آخرین تغییرات اطلاعیه ها")]
        public IEnumerable<dynamic> GetLastPublic(int updateDate)
        {
            var temp = unitOfWork.NotificationsRepository.GetLast(updateDate, null);
            return temp;
        }

        /// <summary>
        /// دریافت آخرین تغییرات صندوق پیام
        /// </summary>
        /// <param name="updateDate"> مقدار آخرین به روز رسانی</param>
        [Authorize]
        [Description("دریافت آخرین تغییرات صندوق پیام")]
        public IEnumerable<dynamic> GetLastPrivate(int updateDate)
        {
            var temp = unitOfWork.NotificationsRepository.GetLast(updateDate, User.Identity.GetUserId<int>());
            return temp;
        }

        [ResponseType(typeof(TblNotifications))]
        public IHttpActionResult GetById(int id)
        {
            var tblNotifications = unitOfWork.NotificationsRepository.GetById(id);
            if (tblNotifications == null)
            {
                return NotFound();
            }

            return Ok(tblNotifications);
        }

        [Authorize]//(Roles = "Admin")
        [ResponseType(typeof(TblNotifications))]
        public IHttpActionResult PostSave(TblNotifications newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.NotificationsRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.NotificationsRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.NotificationsRepository.GetById(newRecord.Id)== null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [Authorize]
        [ResponseType(typeof(TblNotifications))]
        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.NotificationsRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            unitOfWork.NotificationsRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok(); //temp
        }

    }
}