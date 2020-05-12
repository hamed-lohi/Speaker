using System.Xml.Serialization;
using MyModels.Models;
using Newtonsoft.Json;

namespace MyModels.Entity
{
    /// <summary>
    /// پیام کوتاه
    /// </summary>
    public class TblSMS
    {
        public TblSMS()
        {
            //TblCities = new List<TblSMS>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// کد
        /// </summary>
        public int Code { get; set; } // 6 digit

        /// <summary>
        /// کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// زمان ارسال
        /// </summary>
        public long SendTime { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; } // 0: wate  &  1: pass


        [XmlIgnore]
        [JsonIgnore]
        public virtual ApplicationUser TblUser { get; set; }

    }
}
