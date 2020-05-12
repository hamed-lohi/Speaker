using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Configuration;
using System.Xml.Serialization;
using Newtonsoft.Json;
using MyModels.Models;

namespace MyModels.Entity
{
    /// <summary>
    /// شهر و استان
    /// </summary>
    public class TblCity:BaseEntity
    {
        public TblCity()
        {
            TblCities = new List<TblCity>();
            TblPosts = new List<TblPost>();
            TblSpeakers = new List<TblSpeaker>();
            TblSpeakerRequests = new List<TblSpeakerRequest>();
            TblUserInfos = new List<TblUserInfo>();
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام شهر (استان)
        /// </summary>
        public string CityName { get; set; }


        /// <summary>
        /// شناسه استان
        /// </summary>
        public int? PId { get; set; }

        /// <summary>
        /// آخرین تغییر
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public short? Priority { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual TblCity Parent { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblCity> TblCities { get; set; }
        
        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblPost> TblPosts { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeaker> TblSpeakers { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblSpeakerRequest> TblSpeakerRequests { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual ICollection<TblUserInfo> TblUserInfos { get; set; }
    }
}
