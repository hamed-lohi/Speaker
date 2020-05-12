using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyModels.Entity;

namespace Iplus.Controllers
{
    public class PostDeleteReasonApiController : BaseApiController
    {

        public PostDeleteReasonApiController()//IPostDeleteReasonService iPostDeleteReasonService
        {
            //_PostDeleteReasonService = iPostDeleteReasonService;
        }

        public IEnumerable<TblPostDeleteReason> GetAll()
        {
            var temp = unitOfWork.PostDeleteReasonRepository.GetAll();
            return temp.ToList();
        }

        [ResponseType(typeof(TblPostDeleteReason))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var tblPostDeleteReason = unitOfWork.PostDeleteReasonRepository.GetById(id);
                if (tblPostDeleteReason == null)
                {
                    return NotFound();
                }

                return Ok(tblPostDeleteReason);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Authorize]
        [ResponseType(typeof(TblPostDeleteReason))]
        public IHttpActionResult PostSave(TblPostDeleteReason newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            try
            {
                if (newRecord.Id == 0) // Insert
                {
                    unitOfWork.PostDeleteReasonRepository.Insert(newRecord, userId);
                }
                else // Update
                {
                    unitOfWork.PostDeleteReasonRepository.Update(newRecord, userId);
                }
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [ResponseType(typeof(TblPostDeleteReason))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.PostDeleteReasonRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.PostDeleteReasonRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}