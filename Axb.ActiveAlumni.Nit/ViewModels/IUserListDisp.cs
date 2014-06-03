using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public interface IUserListDisp
    {
        List<int> AcceptedIds { get; set; }
        List<int> PendingIds { get; set; }
        List<int> RejectedIds { get; set; }
        IEnumerable<User> PagesUsers { get; }
        IEnumerable<User> TotalUsers { get; }
    }
}