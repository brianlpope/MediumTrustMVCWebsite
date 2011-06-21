using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeRoads.MVC.Infrastructure
{
    public class Helper
    {
        public static DateTime ConvertDateFromUnix(long date)
        {
            DateTime unixDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixDate.AddSeconds(date);
        }
    }
}