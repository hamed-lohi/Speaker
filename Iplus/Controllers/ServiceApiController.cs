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
    public class ServiceApiController : BaseApiController
    {

        public ServiceApiController()//IServiceService iServiceService
        {
            //_ServiceService = iServiceService;
        }

        public IHttpActionResult GetAll()
        {
            var temp = unitOfWork.ServiceRepository.GetAll();
            return Ok(temp.ToList());
        }

        /// <summary>
        /// دریافت آخرین تغییرات خدمات
        /// </summary>
        /// <param name="lastUpdate"> مقدار آخرین به روز رسانی</param>
        /// <returns></returns>
        [Description("دریافت آخرین تغییرات")]
        public IHttpActionResult GetLast(int lastUpdate)
        {
            var temp = unitOfWork.ServiceRepository.GetLast(lastUpdate);
            return Ok(temp);
        }

        //[ResponseType(typeof(TblService))]
        //public IHttpActionResult GetById(int id)
        //{
        //    var tblService = unitOfWork.ServiceRepository.GetById(id);
        //    if (tblService == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tblService);
        //}


        public IHttpActionResult GetById(int id)
        {
            var tblService = unitOfWork.ServiceRepository.GetByIdDynamic(id);
            
            if (tblService == null)
            {
                return NotFound();
            }

            return Ok(tblService);
        }

        [ResponseType(typeof(TblService))]
        public IHttpActionResult PostSave(TblService newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.ServiceRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.ServiceRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.ServiceRepository.GetById(newRecord.Id)== null)
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

        [ResponseType(typeof(TblService))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.ServiceRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.ServiceRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}