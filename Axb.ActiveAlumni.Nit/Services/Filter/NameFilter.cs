using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class NameFilter : UserFilter
    {
        public NameFilter()
            : base(UserSearchTypes.UserName)
        {
            //AutoComplete = "GetUserNames";
            //Name = "Name";
            CkboxName = "UserName";
            IsList = false;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            var term = CheckedItems[0].ToLower();
            if (string.IsNullOrWhiteSpace(term)) return users;
            return users.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(term));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
        }
    }
}