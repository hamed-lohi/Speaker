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
    public class ReportAbuseApiController : BaseApiController
    {

        public ReportAbuseApiController()//IReportAbuseService iReportAbuseService
        {
            //_ReportAbuseService = iReportAbuseService;
        }

        public IEnumerable<TblReportAbuse> GetAll()
        {
            var temp = unitOfWork.ReportAbuseRepository.GetAll();
            return temp.ToList();
        }

        [ResponseType(typeof(TblReportAbuse))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var tblReportAbuse = unitOfWork.ReportAbuseRepository.GetById(id);
                if (tblReportAbuse == null)
                {
                    return NotFound();
                }

                return Ok(tblReportAbuse);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Authorize]
        [ResponseType(typeof(TblReportAbuse))]
        public IHttpActionResult PostSave(TblReportAbuse newRecord)
        {

            ModelState.Remove("UserId");
            //ModelState.Remove("UserViolationId");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            try
            {
                if (newRecord.Id == 0) // Insert
                {
                    unitOfWork.ReportAbuseRepository.Insert(newRecord, userId);
                }
                else // Update
                {
                    unitOfWork.ReportAbuseRepository.Update(newRecord, userId);
                }
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok();

            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [ResponseType(typeof(TblReportAbuse))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.ReportAbuseRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.ReportAbuseRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}