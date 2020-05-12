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
    /// زمینه سخنرانی
    /// </summary>
    public class TblSpeechField : BaseEntity
    {
        public TblSpeechField()
        {
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه سخنران
        /// </summary>
        public int SpeakerId { get; set; }

        /// <summary>
        /// زمینه سخنرانی (ثابت)
        /// </summary>
        public int SSSpeechFieldId { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte State { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public virtual TblSpeaker TblSpeaker { get; set; }
    }
}
