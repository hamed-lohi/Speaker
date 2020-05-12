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
    public class ShoppingApiController : BaseApiController
    {

        public ShoppingApiController()//IShoppingService iShoppingService
        {
            //_ShoppingService = iShoppingService;
        }

        public IEnumerable<TblShopping> GetAll()
        {
            var temp = unitOfWork.ShoppingRepository.GetAll();
            return temp.ToList();
        }

        /// <summary>
        /// دریافت آخرین تغییرات خریدها
        /// </summary>
        /// <param name="updateDate"> مقدار آخرین به روز رسانی</param>
        [Authorize]
        [Description("دریافت آخرین تغییرات خریدها")]
        public IEnumerable<dynamic> GetMyShoppings(int updateDate = 0)
        {
            var temp = unitOfWork.ShoppingRepository.GetMyShopping(User.Identity.GetUserId<int>(), updateDate);
            return temp;
        }

        [Authorize]
        [ResponseType(typeof(TblShopping))]
        public IHttpActionResult GetById(int id)
        {
            var tblShopping = unitOfWork.ShoppingRepository.GetByIdDynamic(id, User.Identity.GetUserId<int>());
            if (tblShopping == null)
            {
                return NotFound();
            }

            return Ok(tblShopping);
        }

        [Authorize]
        [ResponseType(typeof(TblShopping))]
        public IHttpActionResult PostSave(TblShopping newRecord)
        {
            try
            {
                ModelState.Remove("UserId");
                ModelState.Remove("State");
                ModelState.Remove("ExpirationTime");
                ModelState.Remove("ShoppingTime");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.Identity.GetUserId<int>();

                if (newRecord.Id == 0) // Insert
                {
                    unitOfWork.ShoppingRepository.Insert(newRecord, userId);
                }
                else // Update
                {
                    unitOfWork.ShoppingRepository.Update(newRecord, userId);
                }


                unitOfWork.Save();
            }
            //catch (DbUpdateConcurrencyException)
            //{
            //    //if (unitOfWork.ShoppingRepository.GetById(newRecord.Id, userId )== null)
            //    //{
            //    //    return NotFound();
            //    //}
            //    //else
            //    //{
            //    //    throw;
            //    //}
            //}
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(HttpStatusCode.OK);
            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [Authorize]
        [ResponseType(typeof(TblShopping))]
        public IHttpActionResult Delete(int id)
        {
            unitOfWork.ShoppingRepository.Delete(id, User.Identity.GetUserId<int>());
            unitOfWork.Save();

            return Ok(); //temp
        }

    }
}