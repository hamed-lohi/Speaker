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
    public class TicketApiController : BaseApiController
    {

        public TicketApiController()//ITicketService iTicketService
        {
            //_TicketService = iTicketService;
        }

        public IEnumerable<TblTicket> GetAll()
        {
            var temp = unitOfWork.TicketRepository.GetAll();
            return temp.ToList();
        }

        /// <summary>
        /// دریافت آخرین تغییرات تیکت ها
        /// </summary>
        /// <param name="updateDate"> مقدار آخرین به روز رسانی</param>
        [Authorize]
        [Description("دریافت آخرین تغییرات تیکت ها")]
        public IEnumerable<dynamic> GetLast(int updateDate = 0)
        {
            var temp = unitOfWork.TicketRepository.GetLast(updateDate, User.Identity.GetUserId<int>());
            return temp;
        }

        [Authorize]
        [ResponseType(typeof(TblTicket))]
        public IHttpActionResult GetById(int id)
        {
            var tblTicket = unitOfWork.TicketRepository.GetById(id, User.Identity.GetUserId<int>());
            if (tblTicket == null)
            {
                return NotFound();
            }

            return Ok(tblTicket);
        }

        [Authorize]//(Roles = "Admin")
        [ResponseType(typeof(TblTicket))]
        public IHttpActionResult PostSave(TblTicket newRecord)
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
                var newR = newRecord.TicketMessage;
                unitOfWork.TicketMessageRepository.Insert(newR, userId);
                newRecord.TblTicketMessages.Add(newR);
                unitOfWork.TicketRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.TicketRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.TicketRepository.GetById(newRecord.Id, userId )== null)
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
        [ResponseType(typeof(TblTicket))]
        public IHttpActionResult Delete(int id)
        {
            unitOfWork.TicketRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok(); //temp
        }

    }
}