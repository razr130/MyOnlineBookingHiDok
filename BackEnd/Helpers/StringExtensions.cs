using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BackEnd.Helpers
{
    public static class StringExtensions
    {
        public static string ToTglDMY(this string stringTgl)
        {
            DateTime dummyDate;
            //  coba parsing sebagai DMY
            bool isValid = DateTime.TryParseExact(stringTgl, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);

            //  jika tidak berhasil, parsing sebagai YMD
            if (!isValid)
            {
                isValid = DateTime.TryParseExact(stringTgl, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dummyDate);
            }

            if (isValid)
            {
                return dummyDate.ToString("dd-MM-yyyy");
            }
            else
            {
                throw new InvalidOperationException("Invalid string date");
            }
        }

        public static string ToTglYMD(this string stringTgl)
        {
            DateTime dummyDate;
            //  coba parsing sebagai DMY
            bool isValid = DateTime.TryParseExact(stringTgl, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);

            //  jika tidak berhasil, parsing sebagai YMD
            if (!isValid)
            {
                isValid = DateTime.TryParseExact(stringTgl, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dummyDate);
            }

            if (isValid)
            {
                return dummyDate.ToString("yyyy-MM-dd");
            }
            else
            {
                throw new InvalidOperationException("Invalid string date");
            }
        }
    }

}