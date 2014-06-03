using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Areas.Admin.Controllers;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using System.Globalization;

namespace Axb.ActiveAlumni.Nit.Areas.Admin
{
    public class UserStatisticsController : BaseController
    {
        //
        // GET: /Admin/UserStatistics/

        public ActionResult Index()
        {
            CurrentPage = PageTypes.Find;
            var model = new UserStatVm();
            model.ApplyFilters();
            model.FilteredItems = new List<User>();
            return View("Table", model);
        }

        public ViewResult UserRegistration()
        {
            ViewBag.From = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            ViewBag.To = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            return View();
        }

        [HttpPost]
        public ViewResult UserRegistration(DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var users = _db.Users.ToList().Where(u => u.JoinedOn.Date >= from && u.JoinedOn.Date <= to && u.UserCourses.Any()).ToList();
            ViewBag.From = from.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            ViewBag.To = to.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            return View(users);
        }

        public ViewResult UserAutoReg()
        {
            ViewBag.Date = DateTime.Now.ToString("MM/dd/yyyy");
            return View();
        }

        [HttpPost]
        public ViewResult UserAutoReg(DateTime date)
        {
            date = date.Date;
            var users = _db.Users.Include(u => u.UserSessions).ToList()
                            .Where(u => (u.CreateType == UserCreateTypes.Auto || u.CreateType == UserCreateTypes.AutoManual) && u.UserSessions.Any() && u.UserSessions[0].Start.Date == date).ToList();
            ViewBag.Date = date.ToString("MM/dd/yyyy");
            return View(users);
        }

        public FileStreamResult Batch()
        {
            StringBuilder csvData = new StringBuilder();

            CurrentPage = PageTypes.Find;
            var model = new UserStatVm();
            model.ApplyFilters();
            model.FilteredItems = new List<User>();
            foreach (var item in model.Filters[0].FilterItems.OrderBy(o => o.ValueText))
            {
                csvData.AppendLine(string.Format("{0},{1}", item.ValueText, item.Count));
            }
            var byteArray = Encoding.ASCII.GetBytes(csvData.ToString());
            var stream = new MemoryStream(byteArray);
            return File(stream, "text/plain", "NITCAA_BatchStat_"+ DateTime.Now.ToString("MMM_dd")+".csv");
        }

    }
}
