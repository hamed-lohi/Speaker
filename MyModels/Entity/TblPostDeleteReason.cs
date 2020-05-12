using System.Xml.Serialization;
using MyModels.Configuration;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{
    /// <summary>
    /// دلیل حذف آگهی
    /// </summary>
    public class TblPostDeleteReason:BaseEntity
    {
        public TblPostDeleteReason()
        {          
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// شناسه آگهی
        /// </summary>
        //public long PostId { get; set; }

        /// <summary>
        /// دلیل حذف
        /// </summary>
        public int SSReason { get; set; }

        /// <summary>
        /// متن دلیل حذف
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
