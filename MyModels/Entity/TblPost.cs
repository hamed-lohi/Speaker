using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{


    /// <summary>
    /// اخبار
    /// </summary>
    public class TblPost : BaseEntity
    {
        public TblPost()
        {
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نوع خبر
        /// </summary>
        public int SSPostType { get; set; } // ثابت

        /// <summary>
        /// فایل تصویر
        /// </summary>
        public int? ImageFileId { get; set; }

        /// <summary>
        /// عنوان اخبار
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// خلاصه اخبار
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// متن خبر
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// زمان تایید
        /// </summary>
        public long? ApprovedDate { get; set; } // TimeSpan

        /// <summary>
        /// زمان انتشار
        /// </summary>
        public long? PublishDate { get; set; } // TimeSpan

        /// <summary>
        /// انتشار
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// کاربر ثبت کننده
        /// </summary>
        public int UserId { get; set; }

    /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        /// <summary>
        ///  وضعیت
        /// 5:Remove permanently , 4:Deleted , 3:Rejected , 2:InQueue , 1:Accepted
        /// </summary>
        public byte State { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblFile TblFileImage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

    }
}
