using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class JobController : ProfileControllerBase
    {
        //
        // GET: /Profile/Job/

        [HttpGet]
        public PartialViewResult List(int? id)
        {
            var currId = CurrentUserId;
            var userId = id == null ? currId : id;
            var jobs = _db.Jobs.Where(j => j.UserId == userId).ToList();
            var model = jobs.OrderByDescending(c => c.IsCurrentEmployer).ToList();
            ViewData[Constants.ProfileEditKey] = (currId == userId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var job = (id == null)
                        ? new Job()
                        : _db.Jobs.Find(id);
            return PartialView(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Job job)
        {
            var result = UpdateUserOwnedEntity(job);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var job = _db.Jobs.Find(id);
            return DeleteUserOwned(job);
        }

    }
}
