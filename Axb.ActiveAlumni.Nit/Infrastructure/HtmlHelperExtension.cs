using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Axb.ActiveAlumni.Nit.Infrastructure
{
    public enum DateTypes
    {
        Normal,
        Short,
        Medium,
        DateOnly,
        DayOnly,
        TimeOnly,
        WithTime,
        WithDay,
        DateOfMonth,
        MonthOnly
    }

    //public static class HtmlHelperExtension
    //{
    //    public static HtmlString DateTime(this HtmlHelper helper, DateTime time, DateTypes type)
    //    {
    //        var minutes = time.ToString("yyyy/MM/dd/hh/mm");
    //        HtmlString s = new HtmlString(string.Format("<div data-format='{1}' class='time' data-utc='{0}'></div>", minutes, type.ToString()));
    //        return s;
    //    }
    //}
}