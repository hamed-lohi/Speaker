using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyModels.DAL;
using MyModels.Entity;

namespace Iplus.Controllers
{
    public class HomeController : Controller
    {
        private  DatabaseContext db = new DatabaseContext();
        public ActionResult Index(int? st=0)
        {
            //if (st.HasValue && st.Value!=3)
            //{
            //    Version vs = Environment.Version;
            //    //Response.Write(vs.ToString());

            //    return Content("<h1>Soon</h1>");
            //}
            //db.TblCategories.Add(new TblCategory() {Id = 1,CategoryName = "املاک", LastUpdate = 2, Priority = 1, PId = 3, State = 1, IconUrl = "image.jpg"});
            //db.SaveChanges();
            //db.TblCategories.ToList()

            return File(Server.MapPath("~/ng/") + "index.html", "text/html");
            //return View();
        }

        public ActionResult AdminPanel()
        {
            //return View("AdminPanel");
            return File(Server.MapPath("~/ng/") + "index.html", "text/html");
            //Content(System.IO.File.ReadAllText(Server.MapPath("~/my-app/dist/index.html")));
            //new FilePathResult("~/my-app/dist/index.html", "text/html");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        /// <summary>
        /// شکایات
        /// </summary>
        public ActionResult Complaints()
        {
            return View();
        }

        [Authorize]
        public ActionResult Products()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}