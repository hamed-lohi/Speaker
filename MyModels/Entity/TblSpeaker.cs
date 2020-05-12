using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using Newtonsoft.Json;
using MyModels.Models;
using System.ComponentModel;

namespace MyModels.Entity
{

    /// <summary>
    /// سخنران
    /// </summary>
    public class TblSpeaker : BaseEntity
    {
        public TblSpeaker()
        {
            TblSpeechFields = new List<TblSpeechField>();
            TblSpeakerRequests = new List<TblSpeakerRequest>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// زمینه فعالیت (ثابت)
        /// </summary>
        public int SSActivityId { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// شماره همراه
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// عنوان گروه
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// دانشگاه
        /// </summary>
        public string University { get; set; }
        
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string Major { get; set; }
        
        /// <summary>
        /// مقطع
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// تایید شده
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// منتشر شده
        /// </summary>
        public bool IsPublished { get; set; }
        
        /// <summary>
        /// تاریخ و زمان تایید 
        /// </summary>
        public long ApprovedDate { get; set; }

        /// <summary>
        /// تاریخ و زمان انتشار 
        /// </summary>
        public long PublishDate { get; set; }

        /// <summary>
        /// شناسه کاربر ثبت کننده
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        /// <summary>
        /// فایل تصویر
        /// </summary>
        public int? ImageFileId { get; set; }

        /// <summary>
        /// نمونه صوت
        /// </summary>
        public int? ResumeFileId { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// سوابق فعالیت
        /// </summary>
        public string ActivityDescription { get; set; }

        /// <summary>
        /// سوابق تحصیلی رسمی و غیر رسمی
        /// </summary>
        public string EducationDescription { get; set; }

        /// <summary>
        /// سوابق و موضوعات تدریس
        /// </summary>
        public string TeachingDescription { get; set; }

        /// <summary>
        /// سوابق اجرایی
        /// </summary>
        public string RecordsDescription { get; set; }

        /// <summary>
        /// سوابق پژوهش و تالیف
        /// </summary>
        public string ResearchDescription { get; set; }

        /// <summary>
        /// دیدگاه استاد
        /// </summary>
        public string MasterDescription { get; set; }

        /// <summary>
        /// مونث میباشد؟
        /// </summary>
        public bool IsFemale { get; set; }

        /// <summary>
        /// شناسه های ثابت
        /// زمینه سخنرانی
        /// [NotMapped]
        /// </summary>
        [NotMapped]
        public int[] TblSpeechFieldIds { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblCity TblCity { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblFile TblFileImage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblFile TblFileResume { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeechField> TblSpeechFields { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeakerRequest> TblSpeakerRequests { get; set; }

    }
}
