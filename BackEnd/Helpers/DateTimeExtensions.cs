using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BackEnd.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToStringDMY(this DateTime dateTime)
        {
            // Summary:
            // Convert type DateTime to type string with DMY format
            //
            // Parameters:
            //     dateTime:
            //     DateTime object extended by this method.
            //      
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string ToStringYMD(this DateTime dateTime)
        {
            // Summary:
            // Convert type DateTime to type string with YMD format
            //
            // Parameters:
            //     dateTime:
            //     DateTime object extended by this method.
            //      
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}