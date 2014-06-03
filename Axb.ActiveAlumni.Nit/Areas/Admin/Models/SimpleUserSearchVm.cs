using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class SimpleUserSearchVm
    {
        public List<User> FilteredUsers { get; set; }
        public List<User> PagedUsers { get; set; }
        public int PageIndex { get; set; }
    }
}