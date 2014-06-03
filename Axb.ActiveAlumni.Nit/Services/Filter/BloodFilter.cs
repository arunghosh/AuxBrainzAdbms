using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class BloodFilter : UserFilter
    {
        public BloodFilter()
            : base(UserSearchTypes.Blood)
        {
            AutoComplete = null;
            Name = "Group";
            CkboxName = "_seleBlood";
            IsExpanded = true;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var bloodGrps = users.Select(u => u.BloodGroup).ToList();
            //bloodGrps.RemoveAll(m => string.IsNullOrEmpty(m));
            ComposeFilterItems(bloodGrps);
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => _checkedItems.Contains(u.BloodGroup));
        }

    }
}