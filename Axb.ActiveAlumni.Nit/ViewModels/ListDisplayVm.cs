using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class ListDisplayVm<T>
    {
        public List<T> Items { get; set; }
        public int SelectedId { get; set; }
    }
}