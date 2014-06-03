using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class SearchCircleVm : SearchVmBase<Circle, CircleFilter, CircleSearchTypes>, IUserListDisp
    {
        public SearchCircleVm()
        {
        }

        public override void FillMasterList()
        {

            var connect = new ConnectService();
            PendingIds = connect.GetConnectReqIds(ConnectStatusType.RequestSend);
            AcceptedIds = connect.GetConnectReqIds(ConnectStatusType.Accepted);
            RejectedIds = connect.GetConnectReqIds(ConnectStatusType.Rejected);
            PendingIds = RejectedIds.Concat(PendingIds).ToList();

            var temp = _db.Circles.Where(c => UserSession.CurrentUserId == c.UserId)
                        .ToList();
            var myConn = _db.Users.Where(u => AcceptedIds.Contains(u.UserId)).ToList();
            temp.Insert(0, new Circle
            {
                Name = "My Connections",
                Members = myConn
            });
            MasterList = temp;
        }

        public IEnumerable<User> PagesUsers
        {
            get
            {
                int itemPerPage = 10;
                int itemsCnt = TotalUsers.Count();
                TotalPages = (itemsCnt / itemPerPage) + ((itemsCnt % itemPerPage == 0) ? 0 : 1);
                return TotalUsers.Skip((PageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        public IEnumerable<User> TotalUsers
        {
            get { return FilteredItems.SelectMany(p => p.Members).Distinct().ToList(); }
        }

        public List<int> AcceptedIds { get; set; }
        public List<int> PendingIds { get; set; }
        public List<int> RejectedIds { get; set; }

        public override void AddFilters()
        {
            AddFilter(new CircleNameFilter());
        }
    }
}