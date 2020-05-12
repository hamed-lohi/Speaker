using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
//using Iplus.Models;
using MyModels.Models;
using RestSharp;

namespace Iplus.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        /// <summary>
        /// JWT
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            //if (!user.EmailConfirmed)
            //{
            //    context.SetError("invalid_grant", "User did not confirm email.");
            //    return;
            //}

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, userManager.GetRoles(user.Id).FirstOrDefault()));

            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "UserName", user.UserName
                },
                {
                    "FullName", user.FullName
                },
                {
                    "Role", userManager.GetRoles(user.Id).FirstOrDefault()
                },
            });

            var ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);

        }

        /// <summary>
        /// cookie
        /// </summary>
        //public override async Task GrantResourceOwnerCredentials(
        //    OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

        //    if (user == null)
        //    {
        //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //        return;
        //    }

        //    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
        //        OAuthDefaults.AuthenticationType);
        //    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
        //        CookieAuthenticationDefaults.AuthenticationType);

        //    AuthenticationProperties properties = CreateProperties(user.UserName);
        //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
        //    context.Validated(ticket);
        //    context.Request.Context.Authentication.SignIn(cookiesIdentity);
        //}

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        //    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

        //    ApplicationUser user = await userManager.FindByNameAsync(context.UserName);//context.Password FindAsync(context.UserName, "!Pp"+context.UserName)

        //    // تغییر یا تایید شماره همراه
        //    //var result = await userManager.ChangePhoneNumberAsync(user.Id, context.UserName, context.Password);


        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    //var result2 = await signInManager.TwoFactorSignInAsync("Phone Code", context.Password, isPersistent: true, rememberBrowser: true);
        //    var result2 = await userManager.VerifyTwoFactorTokenAsync(user.Id, "Phone Code", context.Password);//.TwoFactorSignInAsync("Phone Code", context.Password, isPersistent: true, rememberBrowser: true);
        //    //switch (result)
        //    //{
        //    //    case SignInStatus.Success:
        //    //        var result2 = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId<int>(), User.Identity.GetUserName(), model.Code);
        //    //        return Ok();
        //    //    case SignInStatus.LockedOut:
        //    //        return BadRequest("Lockout");
        //    //    case SignInStatus.Failure:
        //    //    default:
        //    //        ModelState.AddModelError("", "Invalid code.");
        //    //        return BadRequest("Invalid code.");


        //    if (user == null || !user.PhoneNumberConfirmed || !(result.Succeeded || result2))//|| context.Password != "7f2d910f-e03b-44ea-bb0c-cf661b970868"
        //    {

        //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //        return;
        //        //context.Response.StatusCode = (int)HttpStatusCode.OK;
        //        //context.Response.Write("اطلاعات وارد شده صحیح نمی باشد");
        //        //return;

        //    }

        //    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
        //    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

        //    AuthenticationProperties properties = CreateProperties(user.UserName);
        //    // ارسال وضعیت ثبت اطلاعات پایه
        //    properties.Dictionary.Add(new KeyValuePair<string, string>("isRegister", user.JoinDate ==0? "False":"True"));
        //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
        //    if (true)
        //    {
        //        ticket.Properties.IssuedUtc = DateTime.UtcNow;
        //        ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(40);
        //    }
        //    context.Validated(ticket);
        //    context.Request.Context.Authentication.SignIn(cookiesIdentity);

        //    //-------------------------------------------

        //    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    //identity.AddClaim(new Claim("sub", context.UserName));
        //    //identity.AddClaim(new Claim("role", "user"));

        //    //context.Validated(identity);






        //    //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        //    ////using (ApplicationUserManager _repo = new ApplicationUserManager())
        //    ////{
        //    //    ApplicationUser user = await userManager.FindByNameAsync(context.UserName);//FindAsync(context.UserName, context.Password)

        //    //    if (user == null)
        //    //    {
        //    //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //    //        return;
        //    //    }
        //    ////}

        //    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    //identity.AddClaim(new Claim("sub", context.UserName));
        //    //identity.AddClaim(new Claim("role", "user"));

        //    //context.Validated(identity);

        //}

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}