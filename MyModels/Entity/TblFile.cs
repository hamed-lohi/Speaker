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

namespace MyModels.Entity
{

    /// <summary>
    /// فایل
    /// </summary>
    public class TblFile : BaseEntity
    {
        public TblFile()
        {
            TblImageSpeakers = new List<TblSpeaker>();
            TblResumeSpeakers = new List<TblSpeaker>();
            TblConsts = new List<TblConst>();
            TblPosts = new List<TblPost>();
            TblUserInfos = new List<TblUserInfo>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام فایل
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// پسوند فایل
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// نوع فایل
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// نوع رکورد
        /// مربوط به کدام فرم است
        /// </summary>
        public int SSTypeId { get; set; }
        
        /// <summary>
        /// آدرس فایل Url
        /// </summary>
        public string FileUrl { get; set; }
        
        /// <summary>
        /// مسیر فایل
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// حجم فایل
        /// </summary>
        public int FileLength { get; set; }
        
        /// <summary>
        /// شناسه رکورد
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// تایید شده
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// تاریخ و زمان تایید 
        /// </summary>
        public long ApprovedDate { get; set; }

        /// <summary>
        /// شناسه کاربر ثبت کننده
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

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
        public virtual ApplicationUser TblUser { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeaker> TblImageSpeakers { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeaker> TblResumeSpeakers { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblConst> TblConsts { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblUserInfo> TblUserInfos { get; set; }

    }
}
