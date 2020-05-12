using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyModels.Configuration;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{

    /// <summary>
    /// اطلاعات کاربر
    /// </summary>
    public class TblUserInfo : BaseEntity
    {
        public TblUserInfo()
        {
            //TblNotificationss = new List<TblNotifications>();

        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        //public string FName { get; set; }
        
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        //public string LName { get; set; }

        /// <summary>
        /// توکن
        /// </summary>
        //public string Token { get; set; }


        /// <summary>
        /// فایل تصویر
        /// </summary>
        public int? ImageFileId { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }


        /// <summary>
        /// نام مجموعه یا موسسه
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// نوع فعالیت مجموعه یا موسسه
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// نام مسئول
        /// </summary>
        public string ResponsibleName { get; set; }

        /// <summary>
        /// شماره همراه مسئول
        /// </summary>
        public string ResponsibleMobile { get; set; }

        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// آدرس سایت
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// مونث میباشد؟
        /// </summary>
        public bool IsFemale { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblCity TblCity { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblFile TblFileImage { get; set; }


        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblNotifications> TblNotificationss { get; set; }

    }
}
