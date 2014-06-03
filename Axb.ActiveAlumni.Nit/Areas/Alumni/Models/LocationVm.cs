using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Models
{
    public class LocationVm
    {
        public Address PermenantAddress { get; set; }
        public Address CurrentAddres { get; set; }
    }
}