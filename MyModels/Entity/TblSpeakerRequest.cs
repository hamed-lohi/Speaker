using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using Newtonsoft.Json;
using MyModels.Models;

namespace MyModels.Entity
{

    /// <summary>
    /// درخواست سخنران
    /// </summary>
    public class TblSpeakerRequest : BaseEntity
    {
        public TblSpeakerRequest()
        {
            //TblCategories = new List<TblCategory>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر ثبت کننده
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// شناسه سخنران
        /// </summary>
        public int? SpeakerId { get; set; }

        /// <summary>
        /// موضوع سخنرانی
        /// </summary>
        public int SSSubject { get; set; }

        /// <summary>
        /// نام مجموعه یا موسسه درخواست دهنده
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// نوع فعالیت مجموعه یا موسسه
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int CityId { get; set; }

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
        /// پست الکترونیکی
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// آدرس سایت
        /// </summary>
        public string SiteUrl { get; set; }
        
        /// <summary>
        /// فعالیت شاخص انجام شده توسط موسسه
        /// </summary>
        public string IndicatorActivity { get; set; }

        /// <summary>
        /// موضوع دقیق سخنرانی
        /// </summary>
        public string ExactSubject { get; set; }

        /// <summary>
        /// تاریخ و زمان درخواست سخنرانی
        /// </summary>
        public string SpeakDate { get; set; }

        /// <summary>
        /// کد همکاری
        /// </summary>
        public string CooperationCode { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblCity TblCity { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblSpeaker TblSpeaker { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblCategory> TblCategories { get; set; }

    }
}
