using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{

    public abstract class ProfileControllerBase : BaseController
    {


        [NonAction]
        protected JsonResult DeleteUserOwned(UserOwnedEntity entity)
        {
            if (entity != null && entity.UserId == CurrentUserId)
            {
                _db.Entry(entity).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            else
            {
                return Json("Failed to delete");
            }
            return Json("Deleted");
        }

        [NonAction]

        protected JsonResult UpdateUserOwnedEntity(UserOwnedEntity entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserId;
                    entity.UserId = userId;
                    _db.Entry(entity).State = entity.EntityKey == 0 ? EntityState.Added : EntityState.Modified;
                    _db.SaveChanges();
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Strings.UpdateFailed);
                }
            }
            var errMsgs = GetModelStateErrorMsgs();
            var jsonResult = new
            {
                errMsg = errMsgs.Any() ? errMsgs[0] : null,
            };
            return Json(jsonResult);
        }


        protected JsonResult UpdateUserSubProfile(IUserSubProfile model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = GetCurrentUser();
                    model.UpdateUser(user);
                    UpdateEntity(user);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ModelState.AddModelError("", Strings.ValidDate);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Strings.UpdateFailed);
                }
            }
            var errMsgs = GetModelStateErrorMsgs();
            var jsonResult = new
            {
                errMsg = errMsgs.Any() ? errMsgs[0] : null,
            };
            return Json(jsonResult);
        }


    }
}
