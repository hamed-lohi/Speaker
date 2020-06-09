using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace utility.Date
{
    public class GetDateTime
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Jan1st1970 زمان فعلی به صورت فاصله زمانی از 
        /// </summary>
        public static long CurrentTimeSeconds()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalSeconds;
            //return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// زمان چند ثانیه اختلاف -+
        /// </summary>
        /// <param name="seconds">تعداد ثانیه ها</param>
        public static long DifferenceSecond(int seconds)
        {
            return CurrentTimeSeconds()+seconds;
        }
        
        /// <summary>
        /// زمان چند دقیقه اختلاف -+
        /// </summary>
        /// <param name="minutes">تعداد دقایق</param>
        public static long DifferenceMinute(int minutes)
        {
            return DifferenceSecond(60*minutes);
        }
        
        /// <summary>
        /// زمان چند ساعت اختلاف -+
        /// </summary>
        /// <param name="minutes">تعداد ساعت ها</param>
        public static long DifferenceHour(int hours)
        {
            return DifferenceMinute(60* hours);
        }
        
        /// <summary>
        /// زمان چند روز اختلاف -+
        /// </summary>
        /// <param name="days">تعداد روز ها</param>
        public static long DifferenceDay(int days)
        {
            return DifferenceHour(24* days);
        }
        
        /// <summary>
        /// زمان چند ماه اختلاف -+
        /// </summary>
        /// <param name="months">تعداد ماه ها</param>
        public static long DifferenceMonth(int months)
        {
            return DifferenceDay(30 * months);
        }
        
        /// <summary>
        /// زمان چند سال اختلاف -+
        /// </summary>
        /// <param name="years">تعداد سال ها</param>
        public static long DifferenceYear(int years)
        {
            return DifferenceMonth(12 * years);
        }
        
        /// <summary>
        /// Jan1st1970 تبدیل فرمت تاریخ میلادی به صورت فاصله زمانی از 
        /// </summary>
        public static long ConvertDateTimeToTimeSeconds(DateTime dt)
        {
            return (long)(dt - Jan1st1970).TotalSeconds;
        }

        /// <summary>
        /// تبدیل فاصله زمانی به تاریخ 
        /// </summary>
        /// <param name="seconds"></param>
        public static DateTime GetDateTimeWithSeconds(long seconds)
        {
            var dt = Jan1st1970.AddSeconds(seconds);
            return dt;
        }

        /// <summary>
        /// گرفتن بازه زمانی ماه قبل شمسی
        /// </summary>
        public static KeyValuePair<long, long> GetLastMonthPeriod()
        {
            var currentTimeSecond = CurrentTimeSeconds();
            var currentMiladiDate = GetDateTimeWithSeconds(currentTimeSecond);

            PersianCalendar persianCal = new PersianCalendar();

            var year = persianCal.GetYear(currentMiladiDate);
            var month = persianCal.GetMonth(currentMiladiDate);
            //var days = persianCal

            var monthStart = persianCal.ToDateTime(year, month, 1, 0, 0, 0, 0).AddMonths(-1);
            //var monthStart = persianCal.AddMonths(currentMiladiDate , -1);
            var monthStartSeconds = ConvertDateTimeToTimeSeconds(monthStart);

            var monthEnd = persianCal.ToDateTime(year, month, 1, 0, 0, 0, 0);
            //var monthEnd = persianCal.AddMonths(currentMiladiDate , 0);          
            var monthEndSeconds = ConvertDateTimeToTimeSeconds(monthEnd)-60;

            var period = new KeyValuePair<long, long>(monthStartSeconds, monthEndSeconds);

            return period;

            //DateTime date_time = DateTime.Now;
            //PersianCalendar persianCal = new PersianCalendar();
            //var month = persianCal.GetMonth(date_time).ToString();
            //var days = persianCal.GetDayOfMonth(date_time).ToString();
            //if (Convert.ToInt32(month) < 10)
            //    month = "0" + month;
            //if (Convert.ToInt32(days) < 10)
            //    days = "0" + days;
            //string new_date = persianCal.GetYear(date_time).ToString() + "/" + month + "/" + days;

        }



    }

}
