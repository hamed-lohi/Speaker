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
    public class BrandApiController : BaseApiController
    {

        public BrandApiController()//IBrandService iBrandService
        {
            //_BrandService = iBrandService;
        }

        public IEnumerable<TblBrand> GetAll()
        {
            var temp = unitOfWork.BrandRepository.GetAll();
            return temp.ToList();
        }

        [Description("دریافت آخرین تغییرات")]
        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = unitOfWork.BrandRepository.GetLast(lastUpdate);
            return temp;
        }

        [ResponseType(typeof(TblBrand))]
        public IHttpActionResult GetById(int id)
        {
            var tblBrand = unitOfWork.BrandRepository.GetById(id);
            if (tblBrand == null)
            {
                return NotFound();
            }

            return Ok(tblBrand);
        }

        [ResponseType(typeof(TblBrand))]
        public IHttpActionResult PostSave(TblBrand newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.BrandRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.BrandRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.BrandRepository.GetById(newRecord.Id)== null)
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

        [ResponseType(typeof(TblBrand))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.BrandRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.BrandRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}