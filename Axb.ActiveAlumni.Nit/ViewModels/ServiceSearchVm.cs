using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class ServiceSearchVm: UserSearchBase
    {
        public ServiceSearchVm()
        {
            FillConnections();
        }

        public override void AddFilters()
        {
            AddFilter(new SrvNameFilter());
            AddFilter(new SrvCtgryFilter());
            AddFilter(new ConnectFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
        }

        public override void FillMasterList()
        {
            MasterList = _db.Users
                        .Include(u => u.ServiceInfos)
                        .Include(u => u.UserCourses)
                        .Where(u => u.ServiceInfos.Any())
                        .ToList();
        }
    }
}