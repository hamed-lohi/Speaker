using MyModels.Configuration;

namespace MyModels.Entity
{
    /// <summary>
    /// آمار
    /// </summary>
    public class TblStatistic : BaseEntity
    {
        public TblStatistic()
        {
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// شناسه گوشی
        /// </summary>
        public long IMEI { get; set; }

        /// <summary>
        /// برند گوشی
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// مدل گوشی
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// نسخه اندروید
        /// </summary>
        public float? AndroidVersion { get; set; }
        
        /// <summary>
        /// نسخه API
        /// </summary>
        public int? APILevel { get; set; }


        /// <summary>
        /// زمان دانلود
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte? State { get; set; }

    }
}
