using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Alumni.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Infrastructure;
using System.Text;
using System.IO;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class SearchController : BaseController
    {

        public PartialViewResult NameSearch()
        {
            return PartialView();
        }

        [HttpGet]
        public ViewResult Index()
        {
            CurrentPage = PageTypes.Find;
            var model = new SearchUserVm();
            model.ApplyFilters();
            model.FilteredItems = new List<User>();
            return View(model);
        }


        [HttpPost]
        public ActionResult Index(SearchUserVm model, string output)
        {
            CurrentPage = PageTypes.Find;
            model.ApplyFilters(Request.Form);
            if (output == "excel")
            {
                StringBuilder csvData = new StringBuilder();
                csvData.AppendLine(("Name,Batch,Course,Degree,Mobile,Email"));
                foreach (var item in model.FilteredItems)
                {
                    var uc = item.UserCourses.Any() ? item.UserCourses[0] : new UserCourse {Batch="--", BranchName="--", CourseName="--" };
                    csvData.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.FullName, uc.Batch, uc.BranchName, uc.CourseName, item.MobileNumber, item.Email));
                }
                var byteArray = Encoding.ASCII.GetBytes(csvData.ToString());
                var stream = new MemoryStream(byteArray);
                return File(stream, "text/plain", "NITCAA_UserDetails_" + DateTime.Now.ToString("MMM_dd") + ".csv");
            }
            return View(model);
        }

        [HttpGet]
        public ViewResult Profession()
        {
            CurrentPage = PageTypes.ProfSearch;
            var model = new ProfessionalSearchVm();
            model.ApplyFilters();
            model.FilteredItems = new List<User>();
            return View(Routes.ControllerIndex, model);
        }

        [HttpPost]
        public ActionResult Profession(ProfessionalSearchVm model, string output)
        {
            CurrentPage = PageTypes.ProfSearch;
            model.ApplyFilters(Request.Form);
            if (output == "excel")
            {
                StringBuilder csvData = new StringBuilder();
                csvData.AppendLine(("Name,Batch,Course,Degree,Mobile,Email"));
                foreach (var item in model.FilteredItems)
                {
                    var uc = item.UserCourses.Any() ? item.UserCourses[0] : new UserCourse { Batch = "--", BranchName = "--", CourseName = "--" };
                    csvData.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.FullName, uc.Batch, uc.BranchName, uc.CourseName, item.MobileNumber, item.Email));
                }
                var byteArray = Encoding.ASCII.GetBytes(csvData.ToString());
                var stream = new MemoryStream(byteArray);
                return File(stream, "text/plain", "NITCAA_ProfDetails_" + DateTime.Now.ToString("MMM_dd") + ".csv");
            }

            return View(Routes.ControllerIndex, model);
        }

        [HttpGet]
        public ActionResult AdminSearch()
        {
            CurrentPage = PageTypes.AdminFind;
            var model = new AdminSearchVm();
            model.ApplyFilters();
            return View(Routes.ControllerIndex, model);
        }


        [HttpPost]
        public ActionResult AdminSearch(AdminSearchVm model, string output)
        {
            model.ApplyFilters(Request.Form);
            if (output == "excel")
            {
                StringBuilder csvData = new StringBuilder();
                csvData.AppendLine(("Name,Batch,Course,Degree,Mobile,Email"));
                foreach (var item in model.FilteredItems)
                {
                    var uc = item.UserCourses.Any() ? item.UserCourses[0] : new UserCourse { Batch = "--", BranchName = "--", CourseName = "--" };
                    csvData.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.FullName, uc.Batch, uc.BranchName, uc.CourseName, item.MobileNumber, item.Email));
                }
                var byteArray = Encoding.ASCII.GetBytes(csvData.ToString());
                var stream = new MemoryStream(byteArray);
                return File(stream, "text/plain", "NITCAA_ProfDetails_" + DateTime.Now.ToString("MMM_dd") + ".csv");
            }
            return View(Routes.ControllerIndex, model);
        }
        
        [HttpGet]
        public ViewResult Service()
        {
            CurrentPage = PageTypes.ProfSearch;
            var model = new ServiceSearchVm();
            model.ApplyFilters();
            model.FilteredItems = new List<User>();
            return View(Routes.ControllerIndex, model);
        }

        [HttpPost]
        public ViewResult Service(ServiceSearchVm model)
        {
            CurrentPage = PageTypes.ProfSearch;
            model.ApplyFilters(Request.Form);
            return View(Routes.ControllerIndex, model);
        }

        [HttpGet]
        public PartialViewResult SearchResult(SearchUserVm model)
        {
            return PartialView(model);
        }
    }
}
