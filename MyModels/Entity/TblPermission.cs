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
    /// مجوز های کاربر و نقش
    /// </summary>
    public class TblPermission : BaseEntity
    {
        public TblPermission()
        {
            //TblSpeechFields = new List<TblSpeechField>();
            //TblSpeakerRequests = new List<TblSpeakerRequest>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// نقش - ثابت
        /// </summary>
        public int? SSRoleId { get; set; }

        /// <summary>
        /// فرم - ثابت
        /// </summary>
        public int SSFormId { get; set; }

        /// <summary>
        /// مشاهده
        /// </summary>
        public bool View { get; set; }

        /// <summary>
        /// درج
        /// </summary>
        public bool Insert { get; set; }
        
        /// <summary>
        /// ویرایش
        /// </summary>
        public bool Update { get; set; }

        /// <summary>
        /// حذف
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblSpeechField> TblSpeechFields { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblSpeakerRequest> TblSpeakerRequests { get; set; }

    }
}
