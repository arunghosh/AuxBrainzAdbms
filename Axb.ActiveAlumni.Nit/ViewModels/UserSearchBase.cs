using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public abstract class UserSearchBase : SearchVmBase<User, UserFilter, UserSearchTypes>, IUserListDisp
    {
        public UserSearchBase()
        {
        }

        public override bool HasFilters
        {
            get
            {
                return !string.IsNullOrEmpty(UserName) || Filters.Any(f => f.FilterItems.Any(i => i.IsChecked));
            }
        }

        public string UserName
        {
            get
            {
                if (FilterMap.ContainsKey(UserSearchTypes.UserName))
                {
                    var items = FilterMap[UserSearchTypes.UserName].CheckedItems;
                    return items.Any() ? items[0] : string.Empty;
                }
                return string.Empty;
            }
            set
            {
                if (FilterMap.ContainsKey(UserSearchTypes.UserName))
                {
                    FilterMap[UserSearchTypes.UserName].CheckedItems = new List<string> { };
                    FilterMap[UserSearchTypes.UserName].CheckedItems.Add(value ?? string.Empty);
                }
            }
        }

        public override void AddFilters()
        {
            AddFilter(new LocationFilter());
            AddFilter(new CompanyNameFilter());
            AddFilter(new ConnectFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
            AddFilter(new SkillFilter());
            AddFilter(new InterestFilter());
            AddFilter(new NameFilter());
            AddFilter(new CourseFilter());
            AddFilter(new RoleFilter());
        }

        protected void FillConnections()
        {
            var connect = new ConnectService();
            PendingIds = connect.GetConnectReqIds(ConnectStatusType.RequestSend);
            AcceptedIds = connect.GetConnectReqIds(ConnectStatusType.Accepted);
            RejectedIds = connect.GetConnectReqIds(ConnectStatusType.Rejected);
            PendingIds = RejectedIds.Concat(PendingIds).ToList();
        }

        protected void AddRelatives()
        {
            MasterList = MasterList.Concat(DbCache.Relatives);
        }

        public IEnumerable<User> PagesUsers
        {
            get { return PagedItems; }
        }

        public IEnumerable<User> TotalUsers
        {
            get { return FilteredItems; }
        }

        public List<int> AcceptedIds { get; set; }
        public List<int> PendingIds { get; set; }
        public List<int> RejectedIds { get; set; }

    }
}