using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyModels.Entity;

namespace Iplus.Controllers
{
    public class ChargeApiController : BaseApiController
    {

        public ChargeApiController()//IChargeCharge iChargeCharge
        {
            //_ChargeCharge = iChargeCharge;
        }

        public IHttpActionResult GetAll()
        {
            var temp = unitOfWork.ChargeRepository.GetAll();
            return Ok(temp.ToList());
        }


        [Description("دریافت لیست تراکنش های کاربر")]
        public IHttpActionResult GetMyCharges()
        {
            var temp = unitOfWork.ChargeRepository.GetMyCharges(User.Identity.GetUserId<int>());
            return Ok(temp);
        }


        public IHttpActionResult GetById(int id)
        {
            var tblCharge = unitOfWork.ChargeRepository.GetById(id , User.Identity.GetUserId<int>());
            
            if (tblCharge == null)
            {
                return NotFound();
            }

            return Ok(tblCharge);
        }

        [ResponseType(typeof(TblCharge))]
        public IHttpActionResult PostSave(TblCharge newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.ChargeRepository.Insert(newRecord, userId);
            }
            else // Charge
            {
                unitOfWork.ChargeRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (unitOfWork.ChargeRepository.GetById(newRecord.Id)== null)
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [ResponseType(typeof(TblCharge))]
        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.ChargeRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            unitOfWork.ChargeRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok();
        }

    }
}