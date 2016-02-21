using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class RelativeController : ProfileControllerBase
    {
        //
        // GET: /Profile/Relative/

        //
        // GET: /Profile/Education/
        [HttpGet]
        public PartialViewResult List(int? id)
        {
            var userId = id == null ? CurrentUserId : id;
            var relatives = _db.Relatives.Where(j => j.UserId == userId);
            var model = relatives.ToList();
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == userId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var relative = (id == null)
                        ? new Relative()
                        : _db.Relatives.Find(id);
            ViewBag.RelationShip = relative.RelationShip.ToSelectList();
            return PartialView(relative);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Relative model)
        {
            var result = UpdateUserOwnedEntity(model);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var relative = _db.Relatives.Find(id);
            return DeleteUserOwned(relative);
        }
    }
}
