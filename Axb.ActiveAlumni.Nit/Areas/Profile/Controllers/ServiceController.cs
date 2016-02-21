using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class ServiceController : ProfileControllerBase
    {
        //
        // GET: /Profile/ServiceInfo/

        [HttpGet]
        public PartialViewResult List(int? id)
        {
            var currId = CurrentUserId;
            var userId = id == null ? currId : id;
            var infos = _db.ServiceInfos.Where(j => j.UserId == userId).ToList();
            ViewData[Constants.ProfileEditKey] = (currId == userId);
            return PartialView(infos);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var info = (id == null)
                        ? new ServiceInfo()
                        : _db.ServiceInfos.Find(id);
            return PartialView(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(ServiceInfo info)
        {
            var result = UpdateUserOwnedEntity(info);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var info = _db.ServiceInfos.Find(id);
            return DeleteUserOwned(info);
        }

    }
}
