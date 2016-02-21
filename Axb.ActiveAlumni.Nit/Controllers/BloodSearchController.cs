using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class BloodSearchController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.Find;
            var model = new BloodSearchVm();
            model.ApplyFilters();
            return View(model);
        }


        [HttpPost]
        public ViewResult Index(BloodSearchVm model)
        {
            CurrentPage = PageTypes.Find;
            model.ApplyFilters(Request.Form);
            return View(model);
        }

        [HttpGet]
        public PartialViewResult SearchResult(BloodSearchVm model)
        {
            return PartialView(model);
        }

    }
}
