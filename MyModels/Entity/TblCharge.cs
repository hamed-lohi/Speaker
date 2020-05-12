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
    /// شارژ
    /// </summary>
    public class TblCharge : BaseEntity
    {
        public TblCharge()
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
        /// زمان شارژ
        /// </summary>
        public long ChargeTime { get; set; }

        /// <summary>
        /// شناسه گزینه های شارژ
        /// </summary>
        public int ChargeOptionId{ get; set; }

        /// <summary>
        /// مبلغ شارژ
        /// </summary>
        public decimal Amount{ get; set; }

        /// <summary>
        /// کد
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ChargeOption ChargeOption { get; set; }
        
    }
}
