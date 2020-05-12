using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyModels.Entity;

namespace Iplus.Areas.Admin.Controllers
{
    [Authorize(Roles = "AppAdmin,Admin")]
    public class ASpeakerRequestApiController : BaseApiController
    {

        public ASpeakerRequestApiController()//ISpeakerRequestService iSpeakerRequestService
        {
            //_SpeakerRequestService = iSpeakerRequestService;
        }


        public IEnumerable<dynamic> GetDataTable()
        {
            var temp = unitOfWork.SpeakerRequestRepository.GetDataTable(loginUserRole, loginUserId);
            return temp;
        }

        //[Authorize]
        [Authorize(Roles = "Admin")]
        public IEnumerable<TblSpeakerRequest> GetAll()
        {
            var temp = unitOfWork.SpeakerRequestRepository.GetAll();
            return temp.ToList();
        }

        [ResponseType(typeof(TblSpeakerRequest))]
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
        [ResponseType(typeof(TblSpeakerRequest))]
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
            catch (Exception)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [ResponseType(typeof(TblSpeakerRequest))]
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