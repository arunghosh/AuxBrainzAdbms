using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class ProfileOverviewVmBase
    {
        public bool IsReadOnly { get; set; }
        public int? UserId { get; set; }
        public ProfileOverviewVmBase()
        {
            IsReadOnly = false;
        }

    }
}