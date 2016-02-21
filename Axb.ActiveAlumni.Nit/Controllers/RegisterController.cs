using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Alumni.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    [AllowAnonymous]
    public class RegisterController : BaseController
    {
        RegisterService _regService = new RegisterService();

        public ActionResult Index()
        {
            CurrentPage = PageTypes.NewUser;
            var viewModel = new RegisterUserVm { Role = UserRoleType.Alumni };
            PopulateCourses(viewModel.CourseId, viewModel.BranchId);
            return View(viewModel);
        }

        public ActionResult Confirm(string id)
        {
            bool status = false;
            try
            {
                status = _regService.ValidateEmailToken(id);
            }
            catch
            {

            }
            if (status)
            {
                TempData[Constants.ViewBagMessageKey] = "Thank you for verifying you email. Your email has been verified. Login to continue.";
                return Redirect("/Login");
            }
            return View(status ? null : new SendToEmailVm());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ViewResult ResedEmailToken(SendToEmailVm model)
        {
            try
            {
                _regService.SendEmailValidationToken(model.Email);
                ViewData[Constants.ViewBagMessageKey] = "Instructions send to your email " + model.Email;
                return View("Confirm", null);
            }
            catch (SimpleException ex)
            {
                ViewBag.Message = ex.Message;
                ModelState.AddModelError("", ex.Message);
            }
            return View("Confirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult RegisterUser(RegisterUserVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _regService.RegisterUser(model);
                    _regService.SendEmailValidationToken(user);
                    return PartialView("ThankYou", model);
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            PopulateCourses(model.CourseId, model.BranchId);
            return PartialView("RegisterUser", model);
        }

        [HttpGet]
        public JsonResult GetBranches(int courseId)
        {
            var branches = DbCache.Branches.Where(b => b.CourseId == courseId).ToList();
            // This is done to remove circular reference
            var jsonBranches = branches.Select(b => new
            {
                id = b.BranchId,
                name = b.Name
            });

            return Json(jsonBranches, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Terms()
        {
            return PartialView();
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                _regService.Dispose();
            }
            catch { }
            base.Dispose(disposing);
        }
    }
}
