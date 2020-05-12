using MyModels.Configuration;

namespace MyModels.Entity
{
    /// <summary>
    /// نسخه های آپدیت
    /// </summary>
    public class TblUpdate : BaseEntity
    {
        public TblUpdate()
        {
        }

        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان نسخه
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// کد نسخه
        /// </summary>
        public int VersionCode { get; set; }
        

        /// <summary>
        /// تغییرات
        /// </summary>
        public string ChangeLog { get; set; }

        /// <summary>
        /// بحرانی
        /// </summary>
        public bool IsCritical { get; set; }

        /// <summary>
        /// زمان ثبت
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public byte? State { get; set; }

    }
}
