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
    public class CityApiController : BaseApiController
    {

        public CityApiController()
        {
        }

        public IEnumerable<TblCity> GetAll()
        {
            var temp = unitOfWork.CityRepository.GetAll();
            return temp.ToList();
        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = unitOfWork.CityRepository.GetLast(lastUpdate);
            return temp;
        }

        [Description("کمبوی استان و شهر")]
        public IEnumerable<dynamic> GetSelectOptions()
        {
            var temp = unitOfWork.CityRepository.GetSelectOptions();
            return temp;
        }

        [ResponseType(typeof(TblCity))]
        public IHttpActionResult GetById(int id)
        {
            var tblCity = unitOfWork.CityRepository.GetById(id);
            if (tblCity == null)
            {
                return NotFound();
            }

            return Ok(tblCity);
        }

        [ResponseType(typeof(TblCity))]
        public IHttpActionResult PostSave(TblCity newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.CityRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.CityRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.CityRepository.GetById(newRecord.Id)== null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(TblCity))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.CityRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.CityRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}