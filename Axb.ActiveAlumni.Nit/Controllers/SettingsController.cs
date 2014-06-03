using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class SettingsController : BaseController
    {
        [HttpGet]
        public PartialViewResult HeaderMenu()
        {
            return PartialView();
        }

        [HttpGet]
        public ViewResult UserSettings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult ChangePassword(ChangePasswordVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.ChangePassword(model.NewPassword, model.OldPassword);
                    ViewData[Constants.ViewBagMessageKey] = "You password has been changed successfully.";
                    return PartialView(Routes.ViewDataMsgView);
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string id)
        {
            try
            {
                _userService.ValidatePasswordToken(id);
                return View();
            }
            catch (SimpleException ex)
            {
                TempData[Constants.ViewBagMessageKey] = ex.Message;
                return RedirectToAction("ForgotPassword");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ChangePasswordVm model, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.ResetPassword(id, model.NewPassword);
                    TempData[Constants.ViewBagMessageKey] = "Your password has been changed successfully. Kindly login.";
                    return SafeRedirect(Routes.Login);
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult ForgotPassword()
        {
            return View(new SendToEmailVm());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ViewResult ForgotPassword(SendToEmailVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.SendPasswordResetToken(model.Email);
                    TempData[Constants.ViewBagMessageKey] = "We've sent you an email that will allow you to reset your password. Please check your email.\nIf you don't receive email within a few minutes, check your email's spam and junk filters.";
                    model = null;
                }
                catch (SimpleException ex)
                {
                    TempData[Constants.ViewBagMessageKey] = string.Empty;
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
            }
            catch { }
            base.Dispose(disposing);
        }
    }
}
