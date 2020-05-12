using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
using MyModels.DAL;
using MyModels.Entity;
using Newtonsoft.Json;

namespace MyModels.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim> //TblUser
    {

        public ApplicationUser()
        {
            TblNotificationss = new List<TblNotifications>();
            TblPosts = new List<TblPost>();
            //TblSMSs = new List<TblSMS>();
            TblReportAbuses = new List<TblReportAbuse>();
            TblPostDeleteReasons = new List<TblPostDeleteReason>();
            TblTicketMessages = new List<TblTicketMessage>();
            TblTickets = new List<TblTicket>();
            TblShoppings = new List<TblShopping>();
            TblCharges = new List<TblCharge>();
            TblSpeakers = new List<TblSpeaker>();
            TblSpeakerRequests = new List<TblSpeakerRequest>();
            TblFiles = new List<TblFile>();
            TblUserInfos = new List<TblUserInfo>();
            TblPermissions = new List<TblPermission>();
        }

        public DateTime? ActiveUntil;
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Actor, "rahilebayan.ir"));
            //userIdentity.AddClaim(new Claim(ClaimTypes.UserData, HttpContext.Current.Request.Browser.Browser));
            //userIdentity.AddClaim(new Claim(ClaimTypes.UserData, HttpContext.Current.Request.Browser.MobileDeviceManufacturer));

            return userIdentity;
        }


        /// <summary>
        /// شناسه
        /// </summary>
        //public int Id { get; set; }

        /// <summary>
        /// شناسه نقش
        /// </summary>
        //public byte RoleId { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string FName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string LName { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        //public string UserName { get; set; }

        /// <summary>
        /// گذرواژه
        /// </summary>
        //public string Password { get;set; }

        /// <summary>
        /// شماره تلفن
        /// </summary>
        //public string Tel { get; set; }

        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        //public string Email { get; set; }

        /// <summary>
        /// عکس
        /// </summary>
        //[StringLength(150)]
        //[DataType(DataType.ImageUrl)]
        //public string Image { get; set; }

        /// <summary>
        /// توکن
        /// </summary>
        //public string Token { get; set; }

        /// <summary>
        /// کد
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// اعتبار
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// تاریخ عضویت
        /// </summary>
        public long JoinDate { get; set; }

        ///// <summary>
        ///// شهر محل سکونت
        ///// </summary>
        //public int? CityId { get; set; }

        /// <summary>
        /// بیوگرافی
        /// </summary>
        [StringLength(2000)]
        [DataType(DataType.Text)]
        public string Biography { get; set; }

        /// <summary>
        /// نوع کاربر
        /// </summary>
        public int? SSUserTypeId { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual TblRole TblRole { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual TblCity TblCity { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblNotifications> TblNotificationss { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblReportAbuse> TblReportAbuses { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPostDeleteReason> TblPostDeleteReasons { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }
        
        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblSMS> TblSMSs { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblTicketMessage> TblTicketMessages { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblTicket> TblTickets { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblShopping> TblShoppings { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblCharge> TblCharges { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeaker> TblSpeakers { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeakerRequest> TblSpeakerRequests { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblFile> TblFiles { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblUserInfo> TblUserInfos { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPermission> TblPermissions { get; set; }

    }



    // New derived classes
    public class UserRole : IdentityUserRole<int>{}
    public class UserClaim : IdentityUserClaim<int>{}
    public class UserLogin : IdentityUserLogin<int>{}

    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DatabaseContext context)
            : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(DatabaseContext context)
            : base(context)
        {
        }
    }

    //// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    //{
    //    public DateTime? ActiveUntil;

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}




    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DatabaseCon", throwIfV1Schema: false)//DefaultConnection
    //    {
    //    }
        
    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}