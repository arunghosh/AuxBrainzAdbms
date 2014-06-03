using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class NavigationVm
    {
        public IEnumerable<NavigationItem> ImageItems { get; set; }
        public IEnumerable<NavigationItem> TextItems { get; set; }
        public PageTypes SelectePage { get; set; }
    }

}