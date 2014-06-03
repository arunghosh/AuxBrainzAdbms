using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class NewsletterVm
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public NewsletterVm()
        {
            StartIndex = 0;
            EndIndex = 1000;
        }
    }
}