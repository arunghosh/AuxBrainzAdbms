using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class DashboardController : AdminControllerBase
    {
        public ActionResult Index(string subMenu)
        {
            CurrentPage = PageTypes.AdminDashboard;
            var users = _db.Users.ToList();
            var model = new DashboardVm
            {
                RegisteredUsers = users.Count(u => u.UserSessions.Any()),
                AutoUsers = users.Count(u => u.CreateType == UserCreateTypes.Auto),
                AutoRegUsers = users.Count(u => u.UserSessions.Any() && u.CreateType == UserCreateTypes.Auto),
                TotalUsers = users.Count()
            };

            model.DashItems.Add(new DashNotifyItem
            {
                Name = "User Join Requests",
                Count = _db.Users.Count(i => i.AccountStatus == UserRegisterStatus.Pending),
                Url = Routes.NavigationItems[PageTypes.RegisterSearch].TinyUrl
            });

            model.DashItems.Add(new DashNotifyItem
            {
                Name = "Failed Logins",
                Count = _db.FailedLogins.Count(i => !i.IsRead),
                Url = Routes.NavigationItems[PageTypes.FailedLogins].TinyUrl
            });

            model.DashItems.Add(new DashNotifyItem
            {
                Name = "Feedbacks",
                Count = _db.Feedbacks.Count(i => !i.IsRead),
                Url = Routes.NavigationItems[PageTypes.Feedbacks].TinyUrl
            });

            model.DashItems.Add(new DashNotifyItem
            {
                Name = "Non-Admin News Posts",
                Count = _db.NonAdminNewsPosts.Count(i => !i.IsRead),
                Url = ""
            });

            model.DashItems.Add(new DashNotifyItem
            {
                Name = "Auto Logs",
                Count = _db.Logs.Count(i => !i.IsRead),
                Url = Routes.NavigationItems[PageTypes.Logs].TinyUrl
            });


            return View(model);
        }
    }
}
