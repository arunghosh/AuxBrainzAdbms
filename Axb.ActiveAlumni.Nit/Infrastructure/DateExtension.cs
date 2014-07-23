using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Infrastructure
{
    public static class DateExtension
    {

        public static string Tommddyyhm(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture);
        }

        //public static string _DisplayMedium(this DateTime obj)
        //{
        //    return Display(obj).Replace(" ago", "");
        //}

        public static DateTime ToIst(this DateTime obj)
        {
            return obj.Add(TimeSpan.FromMinutes(330));
        }
        public static string _Display(this DateTime obj)
        {

            var now = DateTime.UtcNow;
            if (obj > now.Add(TimeSpan.FromMinutes(-60)))
            {
                var mins = (now - obj).Minutes;
                return mins + (mins > 1 ? " mins ago" : " min ago");
            }
            else if (obj > now.Add(TimeSpan.FromHours(-24)))
            {
                var hrs = (now - obj).Hours;
                return hrs + (hrs > 1 ? " hours ago" : " hour ago");
            }
            else if (obj > now.Add(TimeSpan.FromDays(-10)))
            {
                var days = (now - obj).Days;
                return days + (days > 1 ? " days ago" : " day ago");
            }
            else return obj.ToString("dd MMM");
        }

        public static string _DisplayShort(this DateTime obj)
        {
            var now = DateTime.UtcNow;
            if (obj > now.Add(TimeSpan.FromMinutes(-60)))
            {
                var mins = (now - obj).Minutes;
                return mins + "m";
            }
            else if (obj > now.Add(TimeSpan.FromHours(-24)))
            {
                var hrs = (now - obj).Hours;
                return hrs + "h";
            }
            else if (obj > now.Add(TimeSpan.FromDays(-10)))
            {
                var days = (now - obj).Days;
                return days + "d";
            }
            else return obj.ToString("dd MMM");
        }
    }
}