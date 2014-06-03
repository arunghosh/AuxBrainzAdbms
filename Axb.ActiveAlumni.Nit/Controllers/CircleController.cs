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

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class CircleController : BaseController
    {
        CircleSrv _srv = new CircleSrv();

        [HttpGet]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.Circles;
            var model = new SearchCircleVm();
            model.ApplyFilters();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SearchCircleVm model)
        {
            CurrentPage = PageTypes.Circles;
            model.ApplyFilters(Request.Form);
            return View(model);
        }

        [HttpPost]
        public PartialViewResult Edit(int? id, List<int> userIds)
        {
            var model = new CircleEdit();
            if (id != null)
            {
                var circle = _db.Circles.Include(c => c.Members).SingleOrDefault(c => c.CircleId == id);
                if (circle.UserId == CurrentUserId)
                {
                    model.Name = circle.Name;
                    model.Users = circle.Members;
                    model.Id = circle.CircleId;
                }
                else
                {
                    LogUnAuth();
                    throw new Exception(Strings.UnAuthAccess);
                }
            }
            else if(userIds != null)
            {
                model.Users = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            }
            return PartialView("Edit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(CircleEdit model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        _srv.AddCircle(model.Name, model.AcSeleUserIds);
                    }
                    else
                    {
                        _srv.UpdateCircle(model.Id, model.Name, model.AcSeleUserIds);
                    }
                }
                catch (SimpleException ex)
                {

                    LogUnAuth();
                    AddModelError(ex);
                }
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult AddUser(int id)
        {
            var currUserId = CurrentUserId;
            var circles = _db.Circles.Where(c => c.UserId == currUserId)
                            .ToList();
            var user = _db.Users.Find(id);
            var model = new AddCircleVm
            {
                Circles = circles,
                UserId = id,
                UserName = user.FullName
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUser(int? circleId, int userId, string newCircle)
        {

            if (circleId == null && !string.IsNullOrWhiteSpace(newCircle))
            {
                var circle = new Circle
                {
                    Name = newCircle,
                    UserId = CurrentUserId,
                };
                _db.Circles.Add(circle);
                var user = _db.Users.Find(userId);
                circle.Members.Add(user);
                _db.SaveChanges();
            }
            else if (circleId != null)
            {
                var circle = _db.Circles.Include(c => c.Members).Single(c => c.CircleId == circleId);
                if ((circle.UserId == CurrentUserId)
                 && !circle.Members.Select(m => m.UserId).Contains(userId))
                {
                    var user = _db.Users.Find(userId);
                    circle.Members.Add(user);
                    _db.SaveChanges();
                }
            }
            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var circle = _db.Circles.Find(id);
            if (circle.UserId == CurrentUserId)
            {
                if (circle != null)
                {
                    _db.Entry(circle).State = System.Data.EntityState.Deleted;
                    _db.SaveChanges();
                }
            }
            else
            {
                LogUnAuth();
            }
            return GetErrorMsgJSON();
        }

    }
}
