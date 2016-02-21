using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class RoleFilter:UserFilter
    {
        public RoleFilter()
            : base(UserSearchTypes.Role)
        {
            AutoComplete = null;
            Name = "UserType";
            DisplayName = "User Type";
            CkboxName = "_sele_uType";
            IsList = true;
            IsExpanded = false;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var roles = users.SelectMany(u => u.UserRoles.Select(r => r.RoleType).Distinct());
            var roleNames = roles.Select(r => r.ToString()).ToList();
            roleNames.RemoveAll(r => r == UserRoleType.Admin.ToString());
            ComposeFilterItems(roleNames);
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (_checkedItems == null || !_checkedItems.Any()) return users;
            return users.Where(u => u.UserRoles.Any(r => _checkedItems.Contains(r.RoleType.ToString())));
        }
    }
}