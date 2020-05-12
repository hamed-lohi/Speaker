using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using Newtonsoft.Json;

namespace MyModels.Entity
{

    /// <summary>
    /// ثابت ها
    /// </summary>
    public class TblConst: BaseEntity
    {
        public TblConst()
        {
            TblConsts = new List<TblConst>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام ثابت (نوع ثابت)
        /// </summary>
        public string ConstName { get; set; }


        /// <summary>
        /// شناسه نوع ثابت
        /// </summary>
        public int? PId { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// فایل تصویر
        /// </summary>
        public int? ImageFileId { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual TblConst Parent { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual TblFile TblFileImage { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblConst> TblConsts { get; set; }
    }
}
