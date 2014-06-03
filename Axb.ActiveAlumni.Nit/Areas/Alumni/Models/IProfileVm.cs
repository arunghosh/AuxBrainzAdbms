using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Models
{
    public interface IProfileVm
    {
        void UpdateUser(User user);
    }
}