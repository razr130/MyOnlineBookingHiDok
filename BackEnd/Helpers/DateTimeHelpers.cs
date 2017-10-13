using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;


namespace BackEnd.Helpers
{
    public static class DateTimeHelpers
    {
        /// <summary>
        ///     Cek validitas format JAM dalam string
        /// </summary>
        /// <param name="jam"></param>
        /// <param name="format">HH:mm, HH:mm:ss</param>
        /// <returns></returns>
        public static bool IsValidJam(string jam, string format)
        {
            DateTime dummyDate;
            return DateTime.TryParseExact(jam, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);
        }

        /// <summary>
        ///     Cek validitas format JAM dalam string
        /// </summary>
        /// <param name="tgl"></param>
        /// <param name="format">dd-MM-yyyy, yyyy-MM-dd</param>
        /// <returns></returns>
        public static bool IsValidTgl(string tgl, string format)
        {
            DateTime dummyDate;
            return DateTime.TryParseExact(tgl, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);
        }
        
        public static DateTime ToDate(string tgl, string format)
        {
            DateTime retVal;
            var isValid =  DateTime.TryParseExact(tgl, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out retVal);
            if (isValid)
                return retVal;
            else
                throw new Exception("Invalid Date String");
        }
    }
}