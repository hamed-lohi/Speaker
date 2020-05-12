using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MyModels.Configuration
{
    /// <summary>
    /// اطلاعات پایه
    /// </summary>
    public abstract class BaseEntity
    {

        /// <summary>
        /// تاریخ درج
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public long InsertDate { get; set; }

        /// <summary>
        /// تاریخ آپدیت
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public long UpdateDate { get; set; }

        /// <summary>
        /// شناسه کاربر ایجاد کننده
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public int InsertUserId { get; set; }

        /// <summary>
        /// شناسه کاربر ویرایش کننده
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public int UpdateUserId { get; set; }

        /// <summary>
        /// نوع عملیات
        /// </summary>
        //[XmlIgnore]
        //[JsonIgnore]
        public byte? ActionType { get; set; }

    }
}
