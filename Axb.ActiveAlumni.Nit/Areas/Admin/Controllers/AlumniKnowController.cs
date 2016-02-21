using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class AlumniKnowController : BaseController
    {
        AlumniKnowSrv _srv = new AlumniKnowSrv();

        protected override void Dispose(bool disposing)
        {
            try
            {
                _srv.Dispose();
            }
            catch { }
            base.Dispose(disposing);
        }


        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult NonAdmin()
        {
            FillAuthKeys();
            PopulateCourses();
            return PartialView();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult NonAdmin(NonAdminNews model)
        {
            if (ModelState.IsValid)
            {

                model.IPAddress = Request.UserHostAddress;
                if (IsAuth)
                {
                    var curr = CurrentUser;
                    model.UserId = curr.UserId;
                    model.UserName = curr.FullName;
                }
                _db.NonAdminNewsPosts.Add(model);
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Summary()
        {
            FillAuthKeys();
            var list = _srv.GetActiveArticles();
            return PartialView(list.Take(3));
        }


        [AllowAnonymous]
        [HttpGet]
        public JsonResult Tweeks()
        {
            var tweekJson = new List<TweekSummary>();
            foreach (var item in _srv.GetActiveArticles())
	        {
		        tweekJson.Add(new TweekSummary
                {
                    Id = item.AlumniToKnowId,
                    Tweek = item.About,
                    UserId = item.UserId,
                    UserName = item.User.FullName,
                    Time = item.CreatedOn.ToString("yyyy/MM/dd/HH/mm"),
                    Batch = item.Batch,
                    lCnt = item.AgreeCnt,
                    dCnt = item.DisagreeCnt
                });
	        }
            return Json(tweekJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            CurrentPage = PageTypes.AlumniToKnow;
            ListDisplayVm<AlumniToKnow> model = null;
            var list = (IsAuth && CurrentUser.IsAdmin())
                            ? _srv.GetAllArticles()
                            : _srv.GetActiveArticles();
            if (list.Any())
            {
                model = new ListDisplayVm<AlumniToKnow>
                {
                    SelectedId = id ?? list.First().AlumniToKnowId,
                    Items = list.ToList()
                };
            }

            FillAuthKeys();
            return View(model);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            AlumniToKnow model;
            if (id == null)
            {
                model = new AlumniToKnow();
            }
            else
            {
                model = _db.AlumnisToKnow.Find(id);
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(AlumniToKnow model)
        {
            if (ModelState.IsValid)
            {
                _srv.AddOrUpdate(model);
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UpdateAffinity(int id, int status)
        {
            var ip = Request.UserHostAddress;
            TweetAffinity aff;
            User user;
            if (IsAuth)
            {
                user = CurrentUser;
                aff = _db.TweetAffinities.FirstOrDefault(c => c.UserId == user.UserId && c.AlumniToKnowId == id);
            }
            else
            {
                aff = _db.TweetAffinities.FirstOrDefault(c => c.IPAddress == ip && c.UserId == 0 && c.AlumniToKnowId == id);
                user = new User
                {
                    UserId = 0,
                    FirstName = "Uk",
                    LastName = "Uk"
                };
            }
            if (aff == null)
            {
                _db.TweetAffinities.Add(new TweetAffinity
                    {
                        AlumniToKnowId= id,
                        Status = status == 1 ? AffinityStatus.Agree : AffinityStatus.Disagree,
                        TimeStamp= DateTime.UtcNow,
                        UserId = user.UserId,
                        UserName = user.FullName,
                        IPAddress = Request.UserHostAddress
                    });
                _db.SaveChanges();
            }
            else
            {
                aff.Status = status == 1 ? AffinityStatus.Agree : AffinityStatus.Disagree;
                aff.TimeStamp = DateTime.UtcNow;
                _db.Entry(aff).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            var comment = _db.AlumnisToKnow
                            .Include(c => c.Affinities)
                            .Single(d => d.AlumniToKnowId == id);
            var json = new
            {
                l = comment.AgreeCnt,
                d = comment.DisagreeCnt,
            };
            return Json(json);
        }
    }
}
