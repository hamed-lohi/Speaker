using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Iplus.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public int Port { get; set; }

        public RequireHttpsAttribute()
        {
            //Port = 443;
            Port = 44300; // added
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            base.OnAuthorization(actionContext);// added

        //    if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
        //    {
        //        var response = new HttpResponseMessage();

        //        if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head )// added post || request.Method == HttpMethod.Post
        //        {
        //            var uri = new UriBuilder(request.RequestUri)
        //            {
        //                Scheme = Uri.UriSchemeHttps,
        //                Port = this.Port
        //            };

        //            response.StatusCode = HttpStatusCode.Found;
        //            response.Headers.Location = uri.Uri;
        //        }
        //        else
        //        {
        //            response.StatusCode = HttpStatusCode.Forbidden;
        //        }

        //        actionContext.Response = response;
        //    }
        //    else
        //    {
        //        base.OnAuthorization(actionContext);
        //    }
        }
    }

}