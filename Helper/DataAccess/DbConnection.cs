using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOnlineBooking.Helper.

namespace MyOnlineBooking.Helper.DataAccess
{
    public static class DbConnection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ProductConnection"].ConnectionString;
        }
        public static string GetTestConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString;
            
        }
    }
}
