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
    public class UpdateApiController : BaseApiController
    {

        public UpdateApiController()//IUpdateUpdate iUpdateUpdate
        {
            //_UpdateUpdate = iUpdateUpdate;
        }

        public IHttpActionResult GetAll()
        {
            var temp = unitOfWork.UpdateRepository.GetAll();
            return Ok(temp.ToList());
        }


        [Description("دریافت اطلاعات آخرین نسخه آپدیت")]
        public IHttpActionResult GetLast()
        {
            var temp = unitOfWork.UpdateRepository.GetLast();
            return Ok(temp);
        }

        //[ResponseType(typeof(TblUpdate))]
        //public IHttpActionResult GetById(int id)
        //{
        //    var tblUpdate = unitOfWork.UpdateRepository.GetById(id);
        //    if (tblUpdate == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tblUpdate);
        //}


        public IHttpActionResult GetById(int id)
        {
            var tblUpdate = unitOfWork.UpdateRepository.GetById(id , User.Identity.GetUserId<int>());
            
            if (tblUpdate == null)
            {
                return NotFound();
            }

            return Ok(tblUpdate);
        }

        [ResponseType(typeof(TblUpdate))]
        public IHttpActionResult PostSave(TblUpdate newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.UpdateRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.UpdateRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (unitOfWork.UpdateRepository.GetById(newRecord.Id)== null)
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

        [ResponseType(typeof(TblUpdate))]
        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.UpdateRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            unitOfWork.UpdateRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok();
        }

    }
}