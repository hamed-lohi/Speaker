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

    public class SpeakerRequestApiController : BaseApiController
    {

        public SpeakerRequestApiController()//ISpeakerRequestService iSpeakerRequestService
        {
            //_SpeakerRequestService = iSpeakerRequestService;
        }

        [Authorize]
        //[Authorize(Roles = "AppAdmin,Admin")]
        public IEnumerable<dynamic> GetDataTable()
        {
            var temp = unitOfWork.SpeakerRequestRepository.GetDataTable(loginUserRole, loginUserId);
            return temp;
        }

        //[Authorize]
        [Authorize(Roles = "AppAdmin,Admin")]
        public IEnumerable<TblSpeakerRequest> GetAll()
        {
            var temp = unitOfWork.SpeakerRequestRepository.GetAll();
            return temp.ToList();
        }

        [Authorize]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var tblSpeakerRequest = unitOfWork.SpeakerRequestRepository.GetById(id);
                if (tblSpeakerRequest == null)
                {
                    return NotFound();
                }

                return Ok(tblSpeakerRequest);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Authorize]
        public IHttpActionResult GetForEdit(int id)
        {
            try
            {
                var request = unitOfWork.SpeakerRequestRepository.GetForEdit(id, loginUserId);

                return Ok(request);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        /// <summary>
        /// دریافت اطلاعات پیش فرض
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult GetDefaultInfo()
        {
            try
            {
                var request = unitOfWork.SpeakerRequestRepository.GetDefaultInfo(loginUserId);

                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        //[ResponseType(typeof(TblSpeakerRequest))]
        public IHttpActionResult Save(TblSpeakerRequest newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                newRecord.UserId = loginUserId;

                if (newRecord.Id == 0) // Insert
                {
                    unitOfWork.SpeakerRequestRepository.Insert(newRecord, loginUserId);
                }
                else // Update
                {
                    unitOfWork.SpeakerRequestRepository.Update(newRecord, loginUserId);
                }
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.SpeakerRequestRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.SpeakerRequestRepository.Delete(id, loginUserId);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}