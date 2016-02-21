using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.Controllers;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class AlumniSpeakController : BaseController
    {
        AlumniSpeakSrv _srv = new AlumniSpeakSrv();

        protected override void Dispose(bool disposing)
        {
            try
            {
                _srv.Dispose();
            }
            catch { }
            base.Dispose(disposing);
        }

        //
        // GET: /Admin/AlumniToKnow/
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            CurrentPage = PageTypes.AlumniSpeaks;
            ListDisplayVm<AlumniSpeak> model = null;
            var list  = (IsAuth && CurrentUser.IsAdmin()) 
                            ? _srv.GetAllArticles()
                            : _srv.GetActiveArticles();
            if (list.Any())
            {
                model = new ListDisplayVm<AlumniSpeak>
                {
                    SelectedId = id ?? list.First().AlumniToKnowId,
                    Items = list.ToList()
                };
            }
            FillAuthKeys();

            var sele = list.Find(l => l.EntityKey == model.SelectedId);
            if (sele != null)
            {
                ViewBag.Meta = sele.AlumniName;
                ViewBag.MetaD = sele.About.LetterLimited(80);
                ViewBag.OgImg = Routes.ImageUrl("logo_fb.jpg");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Summary()
        {
            var list = _srv.GetActiveArticles();
            return PartialView(list.Take(3));
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Show(int id)
        {
            var model = _db.AlumnisSpeak.Find(id);
            FillAuthKeys();
            return PartialView(model);
        }

        [HttpGet]
        [AxbAuthorize(Roles = Constants.RoleAdmin)]
        public PartialViewResult Edit(int? id)
        {
            AlumniSpeak model;
            if (id == null)
            {
                model = new AlumniSpeak();
            }
            else
            {
                model = _db.AlumnisSpeak.Find(id);
            }
            return PartialView(model);
        }

        [HttpPost]
        [AxbAuthorize(Roles = Constants.RoleAdmin)]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Edit(AlumniSpeak model, List<int> AcSeleUserIds)
        {
            if ((AcSeleUserIds == null || !AcSeleUserIds.Any()) && model.AlumniId == 0)
            {
                ModelState.AddModelError("", "Select Alumni");
            }
            if (ModelState.IsValid)
            {
                _srv.AddOrUpdate(model, AcSeleUserIds);
            }
            return GetErrorMsgJSON();
        }

    }
}
