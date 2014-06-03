using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class CourseController : ProfileControllerBase
    {
        //
        // GET: /Profile/Job/

        [HttpGet]
        public PartialViewResult List(int? id)
        {
            var userId = id ?? CurrentUserId;
            var courses = _db.UserCourses.Where(j => j.UserId == userId);
            var model = courses.ToList();
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == userId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Show(int id)
        {
            var course = _db.UserCourses.Find(id);
            PopulateCourses(course.BranchId);
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == course.UserId);
            return PartialView(course);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var course = (id == null)
                        ? new UserCourse()
                        : _db.UserCourses.Find(id);
            PopulateCourses(course.BranchId);
            return PartialView(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(UserCourse userCourse)
        {
            userCourse.UpdateCourseNames();
            var result = UpdateUserOwnedEntity(userCourse);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(int id)
        {
            var course = _db.UserCourses.Find(id);
            if (_db.UserCourses.Where(u => u.UserId == course.UserId).Count() > 1)
            {
                return DeleteUserOwned(course);
            }
            else
            {
                return Json("Failed to remove course!!! Atlest one course should be there.");
            }
        }
    }
}
