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
    public class StatisticApiController : BaseApiController
    {

        public StatisticApiController()//IStatisticStatistic iStatisticStatistic
        {
            //_StatisticStatistic = iStatisticStatistic;
        }

        public IHttpActionResult GetAll()
        {
            var temp = unitOfWork.StatisticRepository.GetAll();
            return Ok(temp.ToList());
        }


        //[Description("دریافت اطلاعات آخرین نسخه آپدیت")]
        //public IHttpActionResult GetLast()
        //{
        //    var temp = unitOfWork.StatisticRepository.GetLast();
        //    return Ok(temp);
        //}


        public IHttpActionResult GetById(int id)
        {
            var tblStatistic = unitOfWork.StatisticRepository.GetById(id , User.Identity.GetUserId<int>());
            
            if (tblStatistic == null)
            {
                return NotFound();
            }

            return Ok(tblStatistic);
        }

        [ResponseType(typeof(TblStatistic))]
        public IHttpActionResult PostSave(TblStatistic newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.StatisticRepository.Insert(newRecord, userId);
            }
            else // Statistic
            {
                unitOfWork.StatisticRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (unitOfWork.StatisticRepository.GetById(newRecord.Id)== null)
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

        [ResponseType(typeof(TblStatistic))]
        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.StatisticRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            unitOfWork.StatisticRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok();
        }

    }
}