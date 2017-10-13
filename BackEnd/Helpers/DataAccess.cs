using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace MyOnlineBooking.Helpers
{
    public class DataAccessHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            //if (UnitTestDetector.IsRunningFromMSTestV2())
            //    return ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString; 
            //else
            //    return ConfigurationManager.ConnectionStrings["ProductConnection"].ConnectionString;
        }
    }
}