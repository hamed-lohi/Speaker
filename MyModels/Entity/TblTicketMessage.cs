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
    /// پیامهای تیکت
    /// </summary>
    public class TblTicketMessage : BaseEntity
    {
        public TblTicketMessage()
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
        /// شناسه تیکت
        /// </summary>
        public int TicketId { get; set; }
        
        /// <summary>
        /// پیام
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// نوع پیام
        /// False:Reply   True:Question
        /// </summary>
        public bool IsQuestion { get; set; }

        /// <summary>
        /// زمان ثبت
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual TblTicket TblTicket { get; set; }

    }
}
