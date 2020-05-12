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
using System.Web.UI;
using Microsoft.AspNet.Identity;
using MyModels.Entity;
using MyModels.Models;
using MyModels.ViewModel;
using MyServices.Interfaces;
using System.ComponentModel;

namespace Iplus.Controllers
{
    public class ConstApiController : BaseApiController
    {

        public ConstApiController() //IConstService iConstService
        {
            //_ConstService = iConstService;
        }

        [Description("کمبوی ثابت ها")]
        public IEnumerable<dynamic> GetSelectOptions(int pId)
        {
            var temp = unitOfWork.ConstRepository.GetSelectOptions(pId);
            return temp;
        }

        public IEnumerable<dynamic> GetDataTable(int? pId = null)
        {
            var temp = unitOfWork.ConstRepository.GetDataTable(pId);
            return temp;
        }

        //public IHttpActionResult GetAll(int pageNumber, int pageSize, string constName , string parent)
        [HttpPost]
        public IHttpActionResult GetAll(RequestPagingViewModel con)
        {

            var temp = unitOfWork.ConstRepository.GetAllConst(con);

            return Ok(temp);

        }

        public IEnumerable<dynamic> GetLast(int lastUpdate)
        {
            var temp = unitOfWork.ConstRepository.GetLast(lastUpdate);
            return temp;
        }

        [ResponseType(typeof(TblConst))]
        public IHttpActionResult GetById(int id)
        {
            var tblConst = unitOfWork.ConstRepository.GetById(id);
            if (tblConst == null)
            {
                return NotFound();
            }

            return Ok(tblConst);
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        [ResponseType(typeof(TblConst))]
        public IHttpActionResult Save(TblConst newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                unitOfWork.ConstRepository.Save(newRecord, loginUserId);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = newRecord.Id }, newRecord);
        }

        [Authorize(Roles = "AppAdmin,Admin")]
        [ResponseType(typeof(TblConst))]
        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.ConstRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            //var id = ids.Split(',').Select(int.Parse).ToArray();

            try
            {

                unitOfWork.ConstRepository.Delete(id);
                unitOfWork.Save();
            }
            catch (Exception e)
            {

                return BadRequest();
            }

            return Ok();
        }

    }
}