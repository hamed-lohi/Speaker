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
    public class TicketMessageApiController : BaseApiController
    {

        public TicketMessageApiController()//ITicketMessageService iTicketMessageService
        {
            //_TicketMessageService = iTicketMessageService;
        }

        public IEnumerable<TblTicketMessage> GetAll()
        {
            var temp = unitOfWork.TicketMessageRepository.GetAll();
            return temp.ToList();
        }

        public IHttpActionResult GetAllByTicketId(int ticketId, int updateDate = 0)
        {
            var temp = unitOfWork.TicketMessageRepository.GetAllByTicketId(User.Identity.GetUserId<int>(), ticketId, updateDate);
            return Ok(temp);
        }

        /// <summary>
        /// دریافت آخرین تغییرات تیکت ها
        /// </summary>
        /// <param name="updateDate"> مقدار آخرین به روز رسانی</param>
        [Authorize]
        [Description("دریافت آخرین تغییرات تیکت ها")]
        public IEnumerable<dynamic> GetLast(int updateDate = 0)
        {
            var temp = unitOfWork.TicketMessageRepository.GetLast(updateDate, User.Identity.GetUserId<int>());
            return temp;
        }

        [Authorize]
        [ResponseType(typeof(TblTicketMessage))]
        public IHttpActionResult GetById(int id)
        {
            var tblTicketMessage = unitOfWork.TicketMessageRepository.GetById(id, User.Identity.GetUserId<int>());
            if (tblTicketMessage == null)
            {
                return NotFound();
            }

            return Ok(tblTicketMessage);
        }

        [Authorize]//(Roles = "Admin")
        [ResponseType(typeof(TblTicketMessage))]
        public IHttpActionResult PostSave(TblTicketMessage newRecord)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("State");
            ModelState.Remove("Time");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.TicketMessageRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.TicketMessageRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.TicketMessageRepository.GetById(newRecord.Id, userId )== null)
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
        [ResponseType(typeof(TblTicketMessage))]
        public IHttpActionResult Delete(int id)
        {
            unitOfWork.TicketMessageRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok(); //temp
        }

    }
}