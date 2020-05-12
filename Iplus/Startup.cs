//using System;
//using System.Text;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.Owin;
//using Owin;
//using MyServices.Interfaces;
//using MyServices.Services;
//using MyModels;
////using Microsoft.Framework.Logging;
////using Microsoft.Framework.DependencyInjection;

//using Microsoft.AspNetCore.Authentication.JwtBearer;

//[assembly: OwinStartupAttribute(typeof(Iplus.Startup))]
//namespace Iplus
//{
//    public partial class Startup
//    {
//        public void Configuration(IApplicationBuilder app)//IAppBuilder
//        {
//            //ConfigureAuth(app);


//            // secretKey contains a secret passphrase only your server knows

//        }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        //public void ConfigureServices(IServiceCollection services)
//        //{
//        //    // Add framework services.
//        //    services.AddMvc();

//        //    //services.AddLogging();

//        //    // Add our repository type
//        //    services.AddSingleton<ICategoryService, CategoryService>();
//        //}

//    }
//}



using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Iplus.Startup))]

namespace Iplus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}