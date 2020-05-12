using System.Xml.Serialization;
using MyModels.Configuration;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{
    /// <summary>
    ///  گزارش تخلفات آگهی و کاربر
    /// </summary>
    public class TblReportAbuse:BaseEntity
    {
        public TblReportAbuse()
        {          
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر گزارش کننده
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// شناسه کاربر متخلف
        /// </summary>
        public int? UserViolationId { get; set; }

        /// <summary>
        /// شناسه آگهی
        /// </summary>
        //public long? PostId { get; set; }

        /// <summary>
        /// دلیل گزارش
        /// </summary>
        public string SSReason { get; set; } // 3,66,78,...  ثابت

        /// <summary>
        /// متن گزارش
        /// </summary>
        public string Description { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }
        
        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual TblPost TblPost { get; set; }

    }
}
