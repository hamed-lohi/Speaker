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
    /// خریدها
    /// </summary>
    public class TblShopping : BaseEntity
    {
        public TblShopping()
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
        //public long? PostId { get; set; }
        
        /// <summary>
        /// شناسه خدمت
        /// </summary>
        public int ServiceId { get; set; }
        
        /// <summary>
        /// زمان انقضاء
        /// </summary>
        public long? ExpirationTime { get; set; }

        /// <summary>
        /// زمان خرید
        /// </summary>
        public long ShoppingTime { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

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

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblService TblService { get; set; }

    }
}
