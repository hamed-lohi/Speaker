using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyModels.Configuration;
using Newtonsoft.Json;

namespace MyModels.Entity
{

    
    //[DataContract]
    /// <summary>
    /// دسته بندی
    /// </summary>
    public class TblCategory: BaseEntity
    {
        public TblCategory()
        {
            TblCategories = new List<TblCategory>();
            //TblBrands = new List<TblBrand>();
            TblPosts = new List<TblPost>();
        }

        //[DataMember]
        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        //[DataMember]
        /// <summary>
        /// نام دسته بندی
        /// </summary>
        public string CategoryName { get; set; }

        
        //[DataMember]
        /// <summary>
        /// شناسه پدر
        /// </summary>
        public int? PId { get; set; }

        //[DataMember]
        /// <summary>
        /// اولویت
        /// </summary>
        public short Priority { get; set; }

        //[DataMember]
        /// <summary>
        /// مسیر آیکن
        /// </summary>
        public string IconUrl { get; set; }

        //[DataMember]
        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

        //[DataMember]
        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// شناسه برند
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// نوع معامله
        /// </summary>
        public int? SSTradeType { get; set; } // ثابت

        /// <summary>
        /// کار کرده
        /// </summary>
        public int? SSStock { get; set; }

        /// <summary>
        /// خرید یا فروش
        /// </summary>
        public int? SSSaleOrBuy { get; set; }

        /// <summary> وسایط نقلیه - املاک - خانگی - شخصی - فراغت - موجودات زنده
        /// نوع/مدل
        /// </summary>
        public int? SSType { get; set; } // ثابت

        /// <summary> فراغت
        /// رشته
        /// </summary>
        public int? SSType2 { get; set; } // ثابت

        /// <summary> فراغت
        /// رشته
        /// </summary>
        public int? SSType3 { get; set; } // ثابت

        /// <summary> شخصی
        /// جنس
        /// </summary>
        public int? SSGender { get; set; } // ثابت

        /// <summary>
        /// متن راهنما
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual TblCategory Parent { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual TblBrand TblBrand { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblCategory> TblCategories { get; set; }
        
        //[XmlIgnore]
        //[JsonIgnore]
        //public virtual ICollection<TblBrand> TblBrands { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }
    }
}
