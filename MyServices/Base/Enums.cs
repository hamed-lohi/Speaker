using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServices.Base
{

    /// <summary>
    /// دسته بندی ثابت ها
    /// </summary>
    public enum ConstEnum
    {
        /// <summary>
        /// زمینه سخنرانی
        /// </summary>
        SpeechField = 1,

        /// <summary>
        /// انواع فایل
        /// </summary>
        FileType = 99,

        /// <summary>
        /// نوع پست
        /// </summary>
        PostType = 200,

        /// <summary>
        /// زمینه فعالیت
        /// </summary>
        ActivityType = 300,

    }

    /// <summary>
    /// نوع فایل در فرم ها
    /// </summary>
    public enum FileFormEnum
    {
        SpeakerImage= 100,
        SpeakerResume= 101,
        ConstImage = 105,
        PostImage = 106,
        UserImage = 110,

    }

    /// <summary>
    /// زمینه فعالیت
    /// </summary>
    public enum ActivityTypeEnum
    {

        /// <summary>
        /// استاد و سخنران
        /// </summary>
        Speaker = 301,

        /// <summary>
        /// نیروی متخصص
        /// </summary>
        Expert = 302,

        /// <summary>
        /// راوی دفاع مقدس
        /// </summary>
        Narrator = 303,

        /// <summary>
        /// قاری قرآن
        /// </summary>
        Reader = 304,

        /// <summary>
        /// مداح و ستایشگر
        /// </summary>
        Eulogist = 305,

        /// <summary>
        /// گروه سرود و تواشیح
        /// </summary>
        VocalGroup = 306,

    }
}
