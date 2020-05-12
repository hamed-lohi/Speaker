using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Net.Http;
using Microsoft.AspNet.Identity;
//using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;


namespace Iplus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie));//OAuthDefaults.AuthenticationType
            
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //config.Filters.Add(new HostAuthenticationFilter("Bearer"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API routes
            config.MapHttpAttributeRoutes();

            // NEW ROUTE FOR YOUR AREA
            //config.Routes.MapHttpRoute(
            //    name: "API Area Default",
            //    routeTemplate: "api/AREA/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Enforce HTTPS
            config.Filters.Add(new Filters.RequireHttpsAttribute());

            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
