using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{

    public class RegisterStatusFilter : UserFilter
    {
        public RegisterStatusFilter()
            : base(UserSearchTypes.RegisterStatus)
        {
            AutoComplete = null;
            Name = "Status";
            CkboxName = "_seleJoinStat";
            IsExpanded = true;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var statusList = users.Select(u => u.AccountStatus.ToString());
            ComposeFilterItems(statusList);
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => _checkedItems.Contains(u.AccountStatus.ToString()));
        }
    }


}
