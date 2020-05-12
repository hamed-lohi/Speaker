using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyModels.Entity;
using System.Web;
using System.Threading.Tasks;
using MyModels.Models;
using System.ComponentModel;

namespace Iplus.Controllers
{
    public class SpeakerApiController : BaseApiController
    {

        public SpeakerApiController()//ISpeakerService iSpeakerService
        {
            //_SpeakerService = iSpeakerService;
        }

        public IEnumerable<dynamic> GetPublishedList(int[] speechFieldIds)
        {
            var temp = unitOfWork.SpeakerRepository.GetPublishedList(loginUserRole, loginUserId, speechFieldIds);
            return temp;
        }

        public IEnumerable<dynamic> GetDataTable(int[] speechFieldIds)
        {
            var temp = unitOfWork.SpeakerRepository.GetDataTable(false, loginUserRole, loginUserId, speechFieldIds);
            return temp;
        }

        public IEnumerable<dynamic> GetTopSpeakers()
        {
            var temp = unitOfWork.SpeakerRepository.GetTopSpeakers(loginUserId);
            return temp;
        }

        public IEnumerable<TblSpeaker> GetAll()
        {
            var temp = unitOfWork.SpeakerRepository.GetAll();
            return temp;
        }

        [Description("کمبوی سخنران ها")]
        public IEnumerable<dynamic> GetSelectOptions(int? speechFieldId)
        {
            var temp = unitOfWork.SpeakerRepository.GetSelectOptions(speechFieldId);
            return temp;
        }

        //[ResponseType(typeof(TblSpeaker))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var tblSpeaker = unitOfWork.SpeakerRepository.GetById(id);
                if (tblSpeaker == null)
                {
                    return NotFound();
                }

                return Ok(tblSpeaker);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        //[ResponseType(typeof(TblSpeaker))]
        public IHttpActionResult GetForEdit(int id)
        {
            try
            {
                var tblSpeaker = unitOfWork.SpeakerRepository.GetForEdit(id, loginUserId);

                return Ok(tblSpeaker);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Authorize]
        //[Authorize(Users = "Alice,Bob")]
        //[Authorize(Roles = "AppAdmin,Admin")]
        public async Task<IHttpActionResult> Save(TblSpeaker newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (newRecord.Id == 0) // Insert
                {
                    newRecord.UserId = loginUserId;
                    unitOfWork.SpeakerRepository.Insert(newRecord, loginUserId);
                }
                else // Update
                {
                    unitOfWork.SpeakerRepository.Update(newRecord, loginUserId);
                }
                unitOfWork.Save();            
            }
            catch (Exception ex)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return Ok();
            //return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        //[Authorize]
        [Authorize(Roles = "AppAdmin,Admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                unitOfWork.SpeakerRepository.Delete(id, loginUserId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }

        public IEnumerable<dynamic> GetSiteSearchResult(string searchText)
        {
            var temp = unitOfWork.SpeakerRepository.SiteSearch(searchText);
            return temp;
        }

    }
}