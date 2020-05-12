using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyServices.DAL;
using Microsoft.AspNet.Identity;

namespace Iplus.Controllers
{
    public class BaseApiController:ApiController
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected int loginUserId => User.Identity.GetUserId<int>();

        protected string loginUserRole => ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirstValue(System.Security.Claims.ClaimTypes.Role);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}