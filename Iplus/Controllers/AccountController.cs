using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
//using Iplus.Models;
using Iplus.Providers;
using Iplus.Results;
using Microsoft.Ajax.Utilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Owin.Infrastructure;
using MyModels.Models;
using Microsoft.Owin.Testing;
using MyModels.Entity;
using Twilio;

namespace Iplus.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [System.Web.Http.Authorize]
        // GET api/Account/UserInfo
        //[HostAuthentication(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)]//ExternalBearer
        //[System.Web.Http.Route("UserInfo")]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            //ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());

            var userInfo = new UserInfoViewModel
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                //HasRegistered = externalLogin == null,
                //LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                IsRegistered = user.JoinDate > 0,
                JoinDate = user.JoinDate,
                //Image = user.Image,
                Credit = user.Credit
            };

            return Ok(userInfo);
        }

        // POST api/Account/Logout
        [System.Web.Http.Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [System.Web.Http.Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser<int, UserLogin, UserRole, UserClaim> user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (UserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }



        // POST api/Account/SetPassword
        [System.Web.Http.Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [System.Web.Http.Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId<int>(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [System.Web.Http.Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId<int>());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId<int>(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [System.Web.Http.OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]//.ExternalCookie
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager, OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager, CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }


        //// POST api/Account/Register
        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Route("Register")]
        //public async Task<IHttpActionResult> Register(PhoneNumberViewModel model) //RegisterBindingModel
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = 
        //        new ApplicationUser()
        //        {
        //            UserName = model.PhoneNumber, TwoFactorEnabled = true, PhoneNumber = model.PhoneNumber, PhoneNumberConfirmed = false//, Code = 0,Credit = 0,JoinDate = 0
        //        }; //{ UserName = model.Email, Email = model.Email };

        //    IdentityResult result = await UserManager.CreateAsync(user, "!Pp"+model.PhoneNumber); //model.Password

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //     //await AddPhoneNumber(new PhoneNumberViewModel() {PhoneNumber = model.PhoneNumber});

        //    //// Generate the token 
        //    //var code = await UserManager.GenerateChangePhoneNumberTokenAsync(user.Id, model.PhoneNumber);//User.Identity.GetUserId<int>()
        //    //if (UserManager.SmsService != null)
        //    //{
        //    //    var message = new IdentityMessage
        //    //    {
        //    //        Destination = model.PhoneNumber,
        //    //        Body = "Your security code is: " + code
        //    //    };
        //    //    // Send token
        //    //    await UserManager.SmsService.SendAsync(message);
        //    //}
        //    //return Ok("کد فعال سازی به شماره " + model.PhoneNumber + "ارسال گردید");

        //    return Ok();
        //}

        // POST api/Account/Register


        /// <summary>
        /// ثبت اطلاعات کاربر جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> AddUserInformation(AddUserInformationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());

                var newImageUrl = "";

                //if (!string.IsNullOrEmpty(model.Images))
                //{
                //    await Request.Content.ReadAsStringAsync();

                //    var path = HttpContext.Current.Server.MapPath("~/Content/UserImages/");
                //    var domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Content/UserImages/";

                //    newImageUrl = (domainName + Guid.NewGuid() + ".jpg");

                //    //await
                //    //    Task.Run(
                //    //        () =>
                //    //            utility.File.Images.SaveImage(new[] {model.Images}, newImageUrl, "", user.Image, path,
                //    //                domainName));

                //}

                //user.FullName = model.FullName;
                user.Email = model.Email;
                //user.Image = newImageUrl;
                user.JoinDate = utility.Date.GetDateTime.CurrentTimeSeconds();

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }


            }
            catch (Exception ms)
            {
                return BadRequest(ms.Message);
            }
            return BadRequest();
        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IHttpActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    /*This will depend totally on how you will get access to the identity provider and get your token, this is just a sample of how it would be done*/
        //    /*Get Access Token Start*/
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://youridentityproviderbaseurl");
        //    var postData = new List<KeyValuePair<string, string>>();
        //    postData.Add(new KeyValuePair<string, string>("UserName", model.Email));
        //    postData.Add(new KeyValuePair<string, string>("Password", model.Password));
        //    HttpContent content = new FormUrlEncodedContent(postData);


        //    HttpResponseMessage response = await httpClient.PostAsync("yourloginapi", content);
        //    response.EnsureSuccessStatusCode();
        //    string AccessToken =
        //        Newtonsoft.Json.JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        //    /*Get Access Token End*/

        //    if (!string.IsNullOrEmpty(AccessToken))
        //    {
        //        var ticket = Startup.OAuthBearerOptions.AccessTokenFormat.Unprotect(AccessToken);
        //        var id = new ClaimsIdentity(ticket.Identity.Claims, DefaultAuthenticationTypes.ApplicationCookie);
        //        AuthenticationManager.SignIn(new AuthenticationProperties() {IsPersistent = true}, id);

        //        return RedirectToLocal(returnUrl);

        //    }

        //    ModelState.AddModelError("Error", "Invalid Authentication");
        //    return View();
        //}


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Login(LoginViewModel model)//, string returnUrl
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user =
                new ApplicationUser()
                {
                    UserName = model.PhoneNumber,
                    TwoFactorEnabled = true,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = false
                    //, Code = 0,Credit = 0,JoinDate = 0
                }; //{ UserName = model.Email, Email = model.Email };

            var currentUser = new ApplicationUser();

            if (!User.Identity.IsAuthenticated)
            {
                IdentityResult Rresult = await UserManager.CreateAsync(user, "!Pp" + model.PhoneNumber); //model.Password
                if (Rresult.Succeeded)
                {
                    //await UserManager.SetLockoutEnabledAsync(user.Id, false);
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    currentUser = user;
                }
                else
                {
                    currentUser = await UserManager.FindByNameAsync(model.PhoneNumber);
                }

            }

            //if (!Rresult.Succeeded)
            //{
            //    return GetErrorResult(Rresult);
            //}

            //// Require the user to have a confirmed email before they can log on.
            //var user = await UserManager.FindByNameAsync(model.PhoneNumber);
            //if (user != null)
            //{
            //    if (!await UserManager.IsPhoneNumberConfirmedAsync(user.Id))//.IsEmailConfirmedAsync(user.Id))
            //    {
            //        //ViewBag.errorMessage = "You must have a confirmed email to log on.";
            //        //return View("Error");
            //        BadRequest();
            //    }
            //}

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.PhoneNumber, "!Pp" + model.PhoneNumber, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    //return null;//RedirectToLocal(returnUrl);
                    //await SendCode(new LoginViewModel() { PhoneNumber = model.PhoneNumber });

                    // Generate the token 
                    var code = await SignInManager.UserManager.GenerateChangePhoneNumberTokenAsync(currentUser.Id, model.PhoneNumber); //User.Identity.GetUserId<int>()
                    var gg = SignInManager.UserManager;
                    if (UserManager.SmsService != null)
                    {
                        var message = new IdentityMessage
                        {
                            Destination = model.PhoneNumber,
                            Body = "Your security code is: " + code
                        };
                        // Send token
                        await SignInManager.UserManager.SmsService.SendAsync(message);
                    }

                    break;
                case SignInStatus.LockedOut:
                    return BadRequest("Lockout"); //View("Lockout");
                case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                    //await SendCode(new LoginViewModel() { PhoneNumber = model.PhoneNumber });

                    // Generate the token and send it
                    var token = await UserManager.GenerateTwoFactorTokenAsync(currentUser.Id, "Phone Code").WithCurrentCulture();
                    // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
                    var ee = await UserManager.NotifyTwoFactorTokenAsync(currentUser.Id, "Phone Code", token).WithCurrentCulture();

                    break;
                //case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return BadRequest("");
            }
            return Ok("کد فعال سازی ارسال گردید");//result

            //var token = Authenticate(model.PhoneNumber, "!Pp" + model.PhoneNumber);
            //return Ok(token);

        }

        // POST api/Account/RegisterExternal
        [System.Web.Http.OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [System.Web.Http.Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

            }

            base.Dispose(disposing);
        }


        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                Random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }


        // POST: /Account/AddPhoneNumber
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [HostAuthentication(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)]
        public async Task<IHttpActionResult> AddPhoneNumber(PhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("شماره وارد شده صحیح نمی باشد");
            }

            // Generate the token 
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), model.PhoneNumber);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.PhoneNumber,
                    Body = "Your security code is: " + code
                };
                // Send token
                await UserManager.SmsService.SendAsync(message);
            }
            return Ok(model.PhoneNumber);
        }

        // POST: /Account/VerifyPhoneNumber
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        //[System.Web.Http.AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)]//ExternalBearer
        public async Task<IHttpActionResult> VerifyPhoneNumber(PhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("شماره وارد شده صحیح نمی باشد");
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId<int>(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {

                HttpResponseMessage res = new HttpResponseMessage();
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);

                    //res = await GenerateToken(User.Identity.GetUserName());

                }
                //return ResponseMessage(res);
                return Ok("شماره با موفقیت ثبت گردید");
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return BadRequest("در ذخیره سازه شماره مشکلی رخ داده است");
        }


        // POST: /Account/SendCode
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.TwoFactorCookie)]//ExternalBearer
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> SendCode(LoginViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync("Phone Code"))
            {
                return BadRequest("Error");
            }
            //return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
            return Ok();
        }

        // POST: /Account/VerifyCode
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync("Phone Code", model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    var result2 = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId<int>(), User.Identity.GetUserName(), model.Code);
                    if (result2.Succeeded)
                    {
                        await GenerateToken(User.Identity.GetUserName());
                    }
                    return Ok();
                // RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return BadRequest("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return BadRequest("Invalid code.");
            }
        }

        // POST api/user/login
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Route("login")]
        private async Task<IHttpActionResult> GenerateToken(string phoneNumber)//LoginUser( PhoneNumberViewModel model )
        {
            //if (model == null)
            //{
            //    return this.BadRequest("Invalid user data");
            //}

            // Invoke the "token" OWIN service to perform the login (POST /api/token)
            var testServer = TestServer.Create<Startup>();

            var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", phoneNumber ),//model.PhoneNumber
                new KeyValuePair<string, string>("password", "7f2d910f-e03b-44ea-bb0c-cf661b970868")//model.Password
            };
            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
            var tokenServiceResponse = testServer.HttpClient.PostAsync("/api/Account/Verify", requestParamsFormUrlEncoded).Result;

            //var tokenResponse = client.PostAsync(baseAddress + "accesstoken", new FormUrlEncodedContent(form)).Result;
            //var token = tokenServiceResponse.Content.ReadAsAsync<object>(new[] { new JsonMediaTypeFormatter() }).Result;

            return this.ResponseMessage(tokenServiceResponse); //this.ResponseMessage(tokenServiceResponse);
        }

        // POST api/user/login
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        //[Route("login")]
        public async Task<IHttpActionResult> LoginUser(UserAccountBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Invalid user data");
            }

            // Invoke the "token" OWIN service to perform the login (POST /api/token)
            var testServer = TestServer.Create<Startup>();
            var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.Username),
                new KeyValuePair<string, string>("password", model.Password)
            };
            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
            var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                "/api/Account/Verify", requestParamsFormUrlEncoded);

            return this.ResponseMessage(tokenServiceResponse);
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            return;
            // Clear the temporary cookies used for external and two factor sign ins
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = isPersistent
            },
               await user.GenerateUserIdentityAsync(UserManager, OAuthDefaults.AuthenticationType));
        }

        // POST: /Manage/EnableTFA
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> EnableTFA()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return Ok("فعال گردید"); // RedirectToAction("Index", "Manage");
        }



        public String Authenticate(string user, string password)
        {

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                return "failed";
            var userIdentity = UserManager.FindAsync(user, password).Result;
            if (userIdentity != null)
            {
                var identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);//.OAuthBearerOptions
                identity.AddClaim(new Claim(ClaimTypes.Name, user));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentity.Id.ToString()));
                AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                var currentUtc = new SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(5));
                string AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);//OAuthBearerOptions
                return AccessToken;
            }
            return "failed";
        }

        #endregion

        /// <summary>
        /// دریافت لیست 10 کاربر فعال ماه گذشته
        /// </summary>
        [System.Web.Http.AllowAnonymous]
        public async Task<IHttpActionResult> GetActiveUsers()
        {
            //var period = utility.Date.GetDateTime.GetLastMonthPeriod();

            var lastMonth = utility.Date.GetDateTime.DifferenceMonth(-1); //utility.Date.GetDateTime.CurrentTimeSeconds() - 2592000 ;

            var activeUsersList = UserManager.Users
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    //u.Image,

                    //score = u.TblPosts.Count(p => period.Key < p.ApprovedDate && p.ApprovedDate < period.Value) * 10 
                    score = (u.TblPosts.Count(p => p.ApprovedDate > lastMonth && p.State == 1) * 10) +
                            (u.TblCharges.Any(t => t.ChargeTime > lastMonth) ?
                            u.TblCharges.Where(t => t.ChargeTime > lastMonth).Sum(t => t.Amount) * 0.020M
                            : 0M)
                    //u..Count(p => period.Key < p.ApprovedDate && p.ApprovedDate < period.Value) * 10
                }).Where(o => o.score > 0).OrderByDescending(o => o.score).Take(10).ToList();
            //.Select(u=> new
            //{
            //    u.user.Id,
            //    u.user.FullName,
            //    u.user.Image,
            //    //u.user.JoinDate,
            //    //PostsCount = u.user.TblPosts.Count,
            //    u.score
            //});
            return Ok(activeUsersList);
        }


        #region UsedMethods

        /// <summary>
        /// تغییر اطلاعات کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> ChangeMyProfile(AddUserInformationViewModel model)
        {

            try
            {
                var user = await UserManager.FindByIdAsync(loginUserId);
                var info = user.TblUserInfos.FirstOrDefault();

                if (info == null)
                {
                    var newInfo = new TblUserInfo
                    {
                        //FName = model.FName,
                        //LName = model.LName,
                        CityId = model.CityId,
                        ImageFileId = model.ImageFileId,
                        CompanyName = model.CompanyName,
                        ActivityType = model.ActivityType,
                        Adress = model.Adress,
                        ResponsibleName = model.ResponsibleName,
                        ResponsibleMobile = model.ResponsibleMobile,
                        PhoneNumber = model.PhoneNumber,
                        SiteUrl = model.SiteUrl,
                    };

                    user.TblUserInfos.Add(newInfo);
                }
                else
                {
                    //info.FName = model.FName;
                    //info.LName = model.LName;
                    info.CityId = model.CityId;
                    info.ImageFileId = model.ImageFileId;
                    info.CompanyName = model.CompanyName;
                    info.ActivityType = model.ActivityType;
                    info.Adress = model.Adress;
                    info.ResponsibleName = model.ResponsibleName;
                    info.ResponsibleMobile = model.ResponsibleMobile;
                    info.PhoneNumber = model.PhoneNumber;
                    info.SiteUrl = model.SiteUrl;

                }

                user.PhoneNumber = model.UserPhoneNumber;
                user.UserName = model.UserPhoneNumber;

                user.FName = model.FName ?? user.FName;
                user.LName = model.LName ?? user.LName;
                user.FullName = user.FName + " " + user.LName;
                user.Email = model.Email ?? user.Email;
                //user.Image = string.IsNullOrEmpty(model.Images) ? user.Image : model.Images.Equals("0") ? null : newImageUrl;

                unitOfWork.Save();
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault());
                }


            }
            catch (Exception ms)
            {
                return BadRequest(ms.Message);
            }
            return BadRequest();
        }

        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                var msg = ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault().ErrorMessage;

                return BadRequest(msg);
            }

            var user = new ApplicationUser() { UserName = model.PhoneNumber, PhoneNumber = model.PhoneNumber, Email = model.Email, FName = model.FName, LName = model.LName, FullName = model.FName + " " + model.LName };

            //IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            IdentityResult result = UserManager.Create(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            UserManager.AddToRole(user.Id, "NormalUser");

            return Ok();
        }

        // POST api/Account/ChangePassword
        //[System.Web.Http.Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(loginUserId, model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [System.Web.Http.Authorize(Roles = "AppAdmin,Admin")]
        public async Task<IHttpActionResult> ResetPasswordByAdmin(ResetPasswordBindingModelAdmin model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Specker");
                //UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser,int>(provider.Create("EmailConfirmation"));
                //var resetToken = await UserManager.GeneratePasswordResetTokenAsync(model.Id);
                //IdentityResult result = await UserManager.ResetPasswordAsync(model.Id, resetToken, model.NewPassword);

                //var oldPassword = await UserManager.pass(model.Id, model.NewPassword);
                //IdentityResult result = await UserManager.AddPasswordAsync(model.Id, model.NewPassword);

                IdentityResult result1 = UserManager.RemovePassword(model.Id);
                if (!result1.Succeeded)
                {
                    return GetErrorResult(result1);
                }

                IdentityResult result2 = UserManager.AddPassword(model.Id, model.NewPassword);
                if (!result2.Succeeded)
                {
                    return GetErrorResult(result2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Ok();
        }

        public async Task<IHttpActionResult> GetMyProfile()
        {
            //ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            var user = await UserManager.FindByIdAsync(loginUserId);
            var info = user.TblUserInfos.FirstOrDefault();

            var userInfo = new
            {
                //user.FullName,
                UserPhoneNumber = user.PhoneNumber,
                user.FName,
                user.LName,
                user.Email,
                //IsRegistered = user.JoinDate > 0,
                user.JoinDate,
                //user.Image,
                //user.Credit,
                //user.TblCity.CityName,
                //user.Biography,
                //info?.FName,
                //info?.LName,
                info?.ImageFileId,
                ImageUrl = info?.TblFileImage?.FileUrl,
                info?.CompanyName,
                info?.ActivityType,
                info?.CityId,
                info?.Adress,
                info?.ResponsibleName,
                info?.ResponsibleMobile,
                info?.PhoneNumber,
                info?.SiteUrl,

                MySpeakers = user.TblSpeakers,

                MySpeakerRequests = user.TblSpeakerRequests,

            };

            return Ok(userInfo);
        }

        [System.Web.Http.Authorize(Roles = "AppAdmin,Admin")]
        public IEnumerable<dynamic> GetDataTable()
        {
            var list = UserManager.Users
                .Where(a=> a.Id > 1)
                .Select(a => new
                {
                    a.Id,
                    a.UserName,
                    a.FullName,
                    a.PhoneNumber,
                    a.Email,
                });

            return list;
        }

        #endregion UsedMethods


    }
}
