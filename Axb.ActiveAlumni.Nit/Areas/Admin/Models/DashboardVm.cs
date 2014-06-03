using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class DashNotifyItem
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class DashboardVm
    {
        public int JoinReqCnt { get; set; }
        public int MentorReqCnt { get; set; }
        public int UserSessionCnt { get; set; }
        public int FeedBackCnt { get; set; }
        public int LogCount { get; set; }
        public int FailedLoginCnt { get; set; }
        public int GuestPostCnt { get; set; }

        public List<DashNotifyItem> DashItems { get; set; }

        public DashboardVm()
        {
            DashItems = new List<DashNotifyItem>();
        }

        //public void AddReadItem(DbSet<IReadEntity> items, string name, PageTypes pageType)
        //{
        //    var notify = new DashNotifyItem
        //    {
        //        Count = items.Count(i => !i.IsRead),
        //        Name = name,
        //        Url = Routes.NavigationItems[pageType].TinyUrl
        //    };
        //    DashItems.Add(notify);
        //}
    }
}