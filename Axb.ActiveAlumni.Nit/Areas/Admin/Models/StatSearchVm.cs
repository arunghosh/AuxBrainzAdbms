using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class StatSearchVm
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}