using System;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Iplus.ServiceReference;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyModels.DAL;
//using Iplus.Models;
using MyModels.Models;
using Twilio;
using TimeSpan = System.TimeSpan;

namespace Iplus
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser,int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser,int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)        
        {
            var manager = new ApplicationUserManager(new UserStore(context.Get<DatabaseContext>()));//<ApplicationUser, IdentityRole<int, IdentityUserRole<int>>, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false, // true
                
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false; // true
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, int>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});


            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();


            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("IPlus"))
            //        {
            //            TokenLifespan = TimeSpan.FromMinutes(5)
            //        };
            //}
            return manager;
        }


        //=========================================================


       //public class ApplicationRoleManager : RoleManager<ApplicationRole>
        //{
        //    public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
        //        : base(roleStore)
        //    {
        //    }

        //    public static ApplicationRoleManager Create(
        //        IdentityFactoryOptions<ApplicationRoleManager> options,
        //        IOwinContext context)
        //    {
        //        return new ApplicationRoleManager(
        //            new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
        //    }
        //}


        //public class ApplicationDbInitializer
        //    : DropCreateDatabaseAlways<ApplicationDbContext>
        //{
        //    protected override void Seed(ApplicationDbContext context)
        //    {
        //        InitializeIdentityForEF(context);
        //        base.Seed(context);
        //    }

        //    //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        //    public static void InitializeIdentityForEF(ApplicationDbContext db)
        //    {
        //        var userManager = HttpContext.Current
        //            .GetOwinContext().GetUserManager<ApplicationUserManager>();

        //        var roleManager = HttpContext.Current
        //            .GetOwinContext().Get<ApplicationRoleManager>();

        //        const string name = "admin@example.com";
        //        const string password = "Admin@123456";
        //        const string roleName = "Admin";

        //        //Create Role Admin if it does not exist
        //        var role = roleManager.FindByName(roleName);
        //        if (role == null)
        //        {
        //            role = new ApplicationRole(roleName);
        //            var roleresult = roleManager.Create(role);
        //        }

        //        var user = userManager.FindByName(name);
        //        if (user == null)
        //        {
        //            user = new ApplicationUser { UserName = name, Email = name };
        //            var result = userManager.Create(user, password);
        //            result = userManager.SetLockoutEnabled(user.Id, false);
        //        }

        //        // Add user admin to Role Admin if not already added
        //        var rolesForUser = userManager.GetRoles(user.Id);
        //        if (!rolesForUser.Contains(role.Name))
        //        {
        //            var result = userManager.AddToRole(user.Id, role.Name);
        //        }
        //    }
        //}

    }


    public class ApplicationRoleManager : RoleManager<Role, int>
    {

        public ApplicationRoleManager(IRoleStore<Role, int> store) : base(store) { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Role, int, UserRole>(context.Get<DatabaseContext>()));
        }
    }

    // Configure the application sign-in manager which is used in this application .
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);//.ApplicationCookie
        }

        //public override async Task<bool> SendTwoFactorCodeAsync(string provider)
        //{
        //    var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
        //    //var userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();

        //    if (userId == 0)
        //    {
        //        return false;
        //    }

        //    var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
        //    // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
        //    await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
        //    return true;
        //}

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }


    public static class Keys
    {
        public static string SMSAccountIdentification = "09142206390"; //"DEMO"; 
        public static string SMSAccountPassword = "1382227906"; //"DEMO";
        public static string SMSAccountFrom = "30004554550543";
    }


    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your SMS service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            // ASPSMS Begin 
            // var soapSms = new WebApplication1.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            // soapSms.SendSimpleTextSMS(
            //   Keys.SMSAccountIdentification,
            //   Keys.SMSAccountPassword,
            //   message.Destination,
            //   Keys.SMSAccountFrom,
            //   message.Body);
            // soapSms.Close();
            // return Task.FromResult(0);
            // ASPSMS End



            //WebServiceSmsSend[] aa = { new WebServiceSmsSend() { IsFlash = false, MobileNo = long.Parse(message.Destination), MessageBody = message.Body } };

            //aa[0].IsFlash = false;
            //aa[0].MobileNo = long.Parse(message.Destination);
            //aa[0].MessageBody = message.Body;

            var url = "http://ip.sms.ir/SendMessage.ashx?user=" +
                      Keys.SMSAccountIdentification +
                      "&pass=" + Keys.SMSAccountPassword +
                      "&lineNo=" + Keys.SMSAccountFrom +
                      "&to=" + message.Destination +
                      "&text=" + message.Body;

            //WebRequest request = WebRequest.Create(url);
            //request.Credentials = CredentialCache.DefaultCredentials;
            //request.ContentType = "application/json";
            //((HttpWebRequest)request).UserAgent = "Zangole.net";
            //request.Method = "POST";
            //WebResponse response = request.GetResponse();//HttpWebResponse


            ////ASCIIEncoding encoder = new ASCIIEncoding();
            ////byte[] data = encoder.GetBytes(serializedObject); // a json object, or xml, whatever...
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.Method = "POST";
            //request.ContentType = "application/json";
            ////request.ContentLength = data.Length;
            //request.ContentLength = 0;
            //request.Expect = "application/json";

            ////request.GetRequestStream().Write(data, 0, data.Length);

            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;


            //var client = new WebClient();
            //var content = client.DownloadString(url);


            var soapSms = new SendReceiveSoapClient();

            var st = "";
            ArrayOfLong num = new ArrayOfLong() { long.Parse(message.Destination) };
            ArrayOfString msg = new ArrayOfString() { message.Body };

            soapSms.SendMessageWithLineNumber(
                Keys.SMSAccountIdentification,
                Keys.SMSAccountPassword,
                num,
                msg,
                Keys.SMSAccountFrom,
                DateTime.Now,
                ref st);

            ////var nn = soapSms.GetDefualtLineNumber(Keys.SMSAccountIdentification, Keys.SMSAccountPassword, ref st);

            ////if (nn != null)
            ////    soapSms.SendMessage(
            ////        Keys.SMSAccountIdentification,
            ////        Keys.SMSAccountPassword,
            ////        aa,
            ////        nn.Value,
            ////        DateTime.Now,
            ////        ref st);

            soapSms.Close();
            return Task.FromResult(0);
        }
    }

}
