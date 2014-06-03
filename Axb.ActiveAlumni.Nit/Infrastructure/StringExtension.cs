using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Infrastructure
{
    public static class StringExtension
    {

        public static string LetterLimited(this string obj, int count)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "--";
            }
            return obj.Length > count ? obj.Substring(0, count) + " ..." : obj;
        }

        public static string FormattedDisplay(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return "--";
            }
            return obj;
        }
    }
}