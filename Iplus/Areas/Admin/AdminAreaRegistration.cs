using System.Web.Http;
using System.Web.Mvc;

namespace Iplus.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            //****************=======Default Api Route=========*******************
            context.Routes.MapHttpRoute(
                name: "AdminApiAction",
                routeTemplate: "Admin/api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                name: "AdminApi",
                routeTemplate: "Admin/api/{controller}"
            );

            //****************=======Default Route=========*******************
            //context.MapRoute(
            //    "Admin_dashboard",
            //    "Admin/{controller}/{action}/{id}",
            //    new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


            //context.MapRoute(
            //    "Admin_default",
            //    "Admin/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional },
            //    new string[] { "Iplus.Areas.Admin.Controllers" }
            //);
        }
    }
}