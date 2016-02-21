using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class EmailVerifyFilter : UserFilter
    {
        const string CONN = "Verified";
        const string N_CONN = "Not Verified";

        public EmailVerifyFilter()
            : base(UserSearchTypes.Role)
        {
            AutoComplete = null;
            Name = "EmailVerify";
            //DisplayName = "Email Verification";
            CkboxName = "_sele_emailV";
            IsList = true;
            IsExpanded = true;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            MasterFilters = FilterItems;
            FilterItems = new List<FilterItem>();
            var verfiedUsers = users.Where(u => u.EmailConfirmedOn != null)
                            .ToList();

            var cfItem = new FilterItem(_checkedItems.Contains(CONN))
            {
                Count = verfiedUsers.Count,
                ValueText = CONN
            };
            FilterItems.Add(cfItem);

            var ncfItem = new FilterItem(_checkedItems.Contains(N_CONN))
            {
                Count = users.Count() - verfiedUsers.Count,
                ValueText = N_CONN
            };
            FilterItems.Add(ncfItem);
            MasterFilters = FilterItems;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            var filterUsers = users;
            if (_checkedItems.Contains(CONN) && !_checkedItems.Contains(N_CONN))
            {
                filterUsers = users.Where(u => u.EmailConfirmedOn != null);
            }
            if (_checkedItems.Contains(N_CONN) && !_checkedItems.Contains(CONN))
            {
                filterUsers = users.Where(u => u.EmailConfirmedOn == null);
            }
            return filterUsers;
        }
    }
}