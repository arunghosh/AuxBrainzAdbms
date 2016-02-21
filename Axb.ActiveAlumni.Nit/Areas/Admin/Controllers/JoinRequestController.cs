using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;


namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class JoinRequestController : AdminControllerBase
    {

        [HttpGet]
        public ActionResult Search()
        {
            CurrentPage = Infrastructure.PageTypes.RegisterSearch;
            var model = new RegisterSearchVm();
            model.FilterMap[UserSearchTypes.RegisterStatus].CheckedItems.Add(UserRegisterStatus.Pending.ToString());
            model.ApplyFilters();
            return View(model);
        }


        [HttpPost]
        public ActionResult Search(RegisterSearchVm model)
        {
            model.ApplyFilters(Request.Form);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(int Id, UserRegisterStatus Status)
        {
            using (var srv = new RegisterService())
            {
                if (CurrentUser.IsAdmin())
                {
                    srv.UpdateRegStatus(Id, Status);
                }
                else
                {
                    LogUnAuth();
                }
            }
            return Json(new { status = "Request " + Status.ToString() });
        }
    }
}
