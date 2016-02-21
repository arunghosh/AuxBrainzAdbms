using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class EducationController : ProfileControllerBase
    {
        //
        // GET: /Profile/Education/
        [HttpGet]
        public PartialViewResult List(int? id)
        {
            var userId = id == null ? CurrentUserId : id;
            var edus = _db.Educations.Where(j => j.UserId == userId);
            var model = edus.ToList();
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == userId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var edu = (id == null)
                        ? new Education()
                        : _db.Educations.Find(id);
            ViewBag.Batches = new SelectList(Enumerable.Range(1965, DateTime.Now.Year + 5));
            return PartialView(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Education edu)
        {
            var result = UpdateUserOwnedEntity(edu);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var edu = _db.Educations.Find(id);
            return DeleteUserOwned(edu);
        }
    }
}
