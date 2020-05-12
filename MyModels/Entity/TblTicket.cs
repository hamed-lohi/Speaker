using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    /// تیکت
    /// </summary>
    public class TblTicket : BaseEntity
    {
        public TblTicket()
        {
            TblTicketMessages = new List<TblTicketMessage>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// واحد مربوطه
        /// </summary>
        //public int SSDepartment { get; set; }
        
        /// <summary>
        /// موضوع تیکت
        /// </summary>
        public int SSSubject { get; set; }

        /// <summary>
        /// عنوان تیکت
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// زمان ثبت
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// وضعیت
        ///  1:Unanswered  2:Open  3:Close
        /// </summary>
        public byte State { get; set; }

        [NotMapped]
        public  TblTicketMessage TicketMessage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblTicketMessage> TblTicketMessages { get; set; }
    }
}
