using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class ConnectFilter : UserFilter
    {
        const string CONN = "Connected";
        const string N_CONN = "Not Connected";
        const string COM_CONN = "Not Connected";
        public ConnectFilter()
            :base(UserSearchTypes.Connect)
        {
            AutoComplete = null;
            Name = "Connection";
            CkboxName = "_seledConn";
            IsExpanded = true;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            MasterFilters = FilterItems;
            FilterItems = new List<FilterItem>();
            var connectSrv = new ConnectService();
            var connIds = connectSrv.GetConnectReqIds(ConnectStatusType.Accepted);
            var connUsers = users.Where(u => connIds.Contains(u.UserId))
                            .ToList();
            var cfItem = new FilterItem(_checkedItems.Contains(CONN))
            {
                Count = connUsers.Count,
                ValueText = CONN
            };
            FilterItems.Add(cfItem);

            var ncfItem = new FilterItem(_checkedItems.Contains(N_CONN))
            {
                Count = users.Count() - connUsers.Count,
                //Count = users.Count(u => !u.IsRelative()) - connUsers.Count,
                ValueText = N_CONN
            };
            FilterItems.Add(ncfItem);
            MasterFilters = FilterItems;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            var connectSrv = new ConnectService();
            var connIds = connectSrv.GetConnectReqIds(ConnectStatusType.Accepted);
            var filterUsers = users;
            if (_checkedItems.Contains(CONN) && !_checkedItems.Contains(N_CONN))
            {
                filterUsers = users.Where(u => connIds.Contains(u.UserId));
            }
            if (_checkedItems.Contains(N_CONN) && !_checkedItems.Contains(CONN))
            {
                filterUsers = users.Where(u => !connIds.Contains(u.UserId));
            }
            return filterUsers;
        }
    }
}