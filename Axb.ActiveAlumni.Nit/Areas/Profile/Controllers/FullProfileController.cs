using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class FullProfileController : ProfileControllerBase
    {
        //
        // GET: /Profile/FullProfile/

        public ActionResult Index(int? id)
        {
            CurrentPage = PageTypes.User;

            var currUser = GetCurrentUser();
            var reqUser = id == null ? currUser : _db.Users.Include(x => x.UserRoles).Single(u => u.UserId ==id);
            var isAdmin = CurrentUser.IsAdmin();
            var model = new ProfileIdVm
            {
                CurrUserId = CurrentUserId,
                ReqUserId = reqUser.UserId,
                CanReqMentor = (reqUser != currUser) 
                                && currUser.IsStudent() 
                                && reqUser.IsAlumni()
                                && reqUser.MentoringInteset,
                ReqUser = reqUser,
                IsCurrAdmin = isAdmin
            };
            ViewData[Constants.ProfileEditKey] = (reqUser == currUser);
            ViewBag.Title = reqUser.FullName + "-  Profile";
            //if (reqUser.IsAlumni() || reqUser.IsTest()) return View("Alumni", model);
            //if (reqUser.IsStudent()) return View("Student", model);
            return View("Alumni", model);
        }


        [HttpGet]
        public ActionResult PictureEdit()
        {
            CurrentPage = PageTypes.User;
            var user = GetCurrentUser();
            ViewBag.Title = "Edit Profile Picture";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureEdit(string fileTitle)
        {
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                byte[] imageBytes = new byte[file.ContentLength];
                if (file.ContentLength > 700 * 1024)
                {
                    ModelState.AddModelError("", "The maximum allowed profile picture size is 700KB");
                    return View();
                }
                else
                {
                    file.InputStream.Read(imageBytes, 0, (int)file.ContentLength);
                    var user = GetCurrentUser();
                    user.ImageType = file.FileName.Split('\\').Last();
                    user.ImageData = imageBytes;
                    UpdateEntity(user);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to update profile picture");
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowPicture(int? id)
        {
            var result = _userService.GetImage(id ?? CurrentUserId, 120, 120);
            return result;
        }


    }
}
