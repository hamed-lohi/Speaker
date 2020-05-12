using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{
    /// <summary>
    /// اطلاعیه
    /// </summary>
    public class TblNotifications:BaseEntity
    {
        public TblNotifications()
        {          
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر
        /// null: General
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// شناسه آگهی
        /// </summary>
        //public long? PostId { get; set; }

        /// <summary>
        /// عنوان اطلاعیه
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// متن اطلاعیه
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// تاریخ اطلاعیه
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }
        
        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual TblPost TblPost { get; set; }

    }
}
