using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class AddCircleVm
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Circle> Circles  { get; set; }
    }
}