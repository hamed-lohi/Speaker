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
    /// خدمات
    /// </summary>
    public class TblService:BaseEntity
    {
        public TblService()
        {    
            TblPosts = new List<TblPost>();
            TblShoppings = new  List<TblShopping>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان خدمت
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        public decimal Cost { get; set; }
        
        /// <summary>
        /// مسیر آیکن
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مسیر تصویر راهنما
        /// </summary>
        public string HelpImageUrl { get; set; }

        /// <summary>
        /// مدت زمان
        /// </summary>
        public int? Time { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblShopping> TblShoppings { get; set; }

    }
}
