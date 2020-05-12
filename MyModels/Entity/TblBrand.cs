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
    /// برند و مدل
    /// </summary>
    public class TblBrand : BaseEntity
    {
        public TblBrand()
        {
            TblPosts = new List<TblPost>();
            TblBrands = new List<TblBrand>();
            TblCategories = new List<TblCategory>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه پدر
        /// </summary>
        public int? PId { get; set; } // null: Brand & hasValue: model

        /// <summary>
        /// مدل
        /// </summary>
        public int? SSModel { get; set; }

        /// <summary>
        /// نام برند
        /// </summary>
        public string BrandName { get; set; }


        ///// <summary>
        ///// شناسه دسته بندی
        ///// </summary>
        //public int? CategoryId { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        /// <summary>
        /// مسیر آیکن
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

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
        public virtual TblBrand Parent { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblBrand> TblBrands { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual TblCategory TblCategory { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblCategory> TblCategories { get; set; }

    }
}
