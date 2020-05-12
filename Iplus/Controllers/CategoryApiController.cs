using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
//using Iplus.Models;
using MyModels.Entity;
using MyServices.Interfaces;

namespace Iplus.Controllers
{
    public class CategoryApiController : BaseApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        //private readonly ICategoryService _categoryService;
        //private UnitOfWork unitOfWork = new UnitOfWork();

        public CategoryApiController()//ICategoryService iCategoryService
        {
            //_categoryService = iCategoryService;
        }

        public IEnumerable<TblCategory> GetAll()
        {
            var temp = unitOfWork.CategoryRepository.GetAll();
            return temp.ToList();
        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = unitOfWork.CategoryRepository.GetLast(lastUpdate);
            return temp;
        }

        // GET: api/TblCategories/5
        [ResponseType(typeof(TblCategory))]
        public IHttpActionResult GetById(int id)
        {
            var tblCategory = unitOfWork.CategoryRepository.GetById(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return Ok(tblCategory);
        }

        // POST: api/TblCategories
        [ResponseType(typeof(TblCategory))]
        public IHttpActionResult PostSave(TblCategory newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId<int>();

            if (newRecord.Id == 0) // Insert
            {
                unitOfWork.CategoryRepository.Insert(newRecord, userId);
            }
            else // Update
            {
                unitOfWork.CategoryRepository.Update(newRecord, userId);
            }

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.CategoryRepository.GetById(newRecord.Id)== null)
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

        // DELETE: api/TblCategories/5
        [ResponseType(typeof(TblCategory))]
        public IHttpActionResult Delete(int id)
        {
            var temp = unitOfWork.CategoryRepository.GetById(id);
            if (temp == null)
            {
                return NotFound();
            }

            unitOfWork.CategoryRepository.Delete(id);
            unitOfWork.Save();

            return Ok(temp);
        }

    }
}