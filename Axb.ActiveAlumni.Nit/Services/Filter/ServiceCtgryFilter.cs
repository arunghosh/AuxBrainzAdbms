using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class SrvCtgryFilter: UserFilter
    {
        public SrvCtgryFilter()
            : base(UserSearchTypes.SrvCtgry)
        {
            AutoComplete = Routes.AcSrvCtgrys;
            Name = "Category";
            CkboxName = "_seleSrvCtgry";
            IsExpanded = true;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.ServiceInfos.Any(s => _checkedItems.Contains(s.Category)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var items = users
                .SelectMany(u => u.ServiceInfos)
                .Select(u => u.Category);
            ComposeFilterItems(items);
        }
    }


    public class SrvNameFilter : UserFilter
    {
        public SrvNameFilter()
            : base(UserSearchTypes.SrvName)
        {
            AutoComplete = Routes.AcSrvNames;
            Name = "Service";
            CkboxName = "_seleSrvName";
            IsExpanded = true;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.ServiceInfos.Any(s => _checkedItems.Contains(s.Title)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var items = users
                .SelectMany(u => u.ServiceInfos)
                .Select(u => u.Title);
            ComposeFilterItems(items);
        }
    }
}