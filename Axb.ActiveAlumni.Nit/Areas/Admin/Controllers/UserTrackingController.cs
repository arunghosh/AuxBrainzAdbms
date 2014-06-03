using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class UserTrackingController : AdminControllerBase
    {
        public ViewResult Activity(int timeInDays = 1)
        {
            var ts = DateTime.UtcNow.Add(TimeSpan.FromDays(timeInDays * -1));
            var sessions = _db.UserSessions.Where(s => s.Start > ts).ToList();
            return View(sessions);
        }

        public ViewResult FailedLogins(int timeInDays = 2)
        {
            var ts = DateTime.UtcNow.Add(TimeSpan.FromDays(timeInDays * -1));
            var attempts = _db.FailedLogins.Where(s => s.Time > ts).ToList();
            return View(attempts);
        }

        public ViewResult Logs(int timeInDays = 7)
        {
            var ts = DateTime.UtcNow.Add(TimeSpan.FromDays(timeInDays * -1));
            var logs = _db.Logs.Where(s => s.Date > ts).ToList();
            return View(logs);
        }
    }
}
