using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class ManageUserController : AdminControllerBase
    {
        const string ResultView = "SearchResult";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateTouchPoint(int id)
        {
            var user = _db.Users.Find(id);
            user.IsTouchPoint = !user.IsTouchPoint; 
            var log = new UserLog(CurrentUser, id);
            log.Comment = "Touch Point Status: " + user.IsTouchPoint;
            _db.UserLogs.Add(log);
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Block(int id, UserRegisterStatus status, string reason)
        {
            if (status == UserRegisterStatus.Approved)
            {
                if (string.IsNullOrEmpty(reason))
                {

                    ModelState.AddModelError("", "Reason to block is required");
                }
                else if (reason.Length > 1000)
                {
                    ModelState.AddModelError("", "Length of reason to block cannot be more than 1000");
                }
            }
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(id);
                var log = new UserLog(CurrentUser, id);
                _db.UserLogs.Add(log);
                if (status == UserRegisterStatus.Suspended)
                {
                    user.AccountStatus = UserRegisterStatus.Approved;
                    log.Comment = Strings.UnblockedMsg;
                }
                else
                {
                    user.AccountStatus = UserRegisterStatus.Suspended;
                    log.Comment = "User Blocked. Reason: " + reason;
                }
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUserRole(int id, UserRoleType role)
        {
            var user = _db.Users
                .Include(u => u.UserRoles)
                .Single(u => u.UserId == id);

            var uRole = user.UserRoles.First();
            uRole.RoleType = role;
            var log = new UserLog(CurrentUser, id)
                {
                    Comment = "Made " + uRole.RoleType.ToString()
                };
            _db.UserLogs.Add(log);
            _db.Entry(uRole).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateAdminRole(int id)
        {
            var user = _db.Users
                .Include(u => u.UserRoles)
                .Single(u => u.UserId == id);

            var adminRole = user.UserRoles.SingleOrDefault(u => u.RoleType == UserRoleType.Admin);
            var log = new UserLog(CurrentUser, id);
            _db.UserLogs.Add(log);
            if (adminRole != null)
            {
                if (user.UserRoles.Count == 1)
                {
                    var role = new UserRole
                    {
                        RoleType = UserRoleType.Guest,
                        UserId = id,
                    };
                    _db.UserRoles.Add(role);
                }
                _db.Entry(adminRole).State = System.Data.EntityState.Deleted;
                log.Comment = Strings.RemovedAdmin;
            }
            else
            {
                var role = new UserRole
                {
                    RoleType = UserRoleType.Admin,
                    UserId = id,
                };
                _db.UserRoles.Add(role);
                log.Comment = Strings.AddedAdmin;
            }
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult UserPanel(int id)
        {
            var user = _db.Users.Include(u => u.UserSessions)
                                .Include(u => u.UserRoles)
                                .Include(u => u.UserCourses)
                                .Include(u => u.UserLogs)
                                .Single(u => u.UserId == id);
            return PartialView(user);
        }

        [HttpGet]
        public PartialViewResult Manual(int pageIndex = 0)
        {
            var users = _db.Users.Where(u => u.CreateType == UserCreateTypes.Admin).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();

            return PartialView(ResultView, new SimpleUserSearchVm
                {
                    PageIndex = pageIndex,
                    FilteredUsers = users,
                    PagedUsers = users
                });
        }

        [HttpGet]
        public PartialViewResult Auto(int pageIndex = 0)
        {
            var users = _db.Users.Where(u => u.CreateType == UserCreateTypes.Auto).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();

            return PartialView(ResultView, new SimpleUserSearchVm
            {
                PageIndex = pageIndex,
                FilteredUsers = users,
                PagedUsers = users
            });
        }

        [HttpGet]
        public PartialViewResult TouchPoints(int pageIndex = 0)
        {
            var users = _db.Users.Where(u => u.IsTouchPoint).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();

            return PartialView(ResultView, new SimpleUserSearchVm
            {
                PageIndex = pageIndex,
                FilteredUsers = users,
                PagedUsers = users
            });
        }

        [HttpGet]
        public PartialViewResult Admins(int pageIndex = 0)
        {
            var users = _db.Users.Where(u => u.UserRoles.Any(r => r.RoleType == UserRoleType.Admin)).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();
            return PartialView(ResultView, new SimpleUserSearchVm
            {
                PageIndex = pageIndex,
                FilteredUsers = users,
                PagedUsers = users
            });
        }

        [HttpGet]
        public PartialViewResult TestUsers(int pageIndex = 0)
        {
            var users = _db.Users.Where(u => u.UserRoles.Any(r => r.RoleType == UserRoleType.Test)).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();
            return PartialView(ResultView, new SimpleUserSearchVm
            {
                PageIndex = pageIndex,
                FilteredUsers = users,
                PagedUsers = users
            });
        }

        [HttpGet]
        public PartialViewResult Active(int pageIndex = 0)
        {
            var time = DateTime.UtcNow.AddDays(-1);
            var users = _db.UserSessions.Where(u => u.End == null && u.Start > time).ToList().Select(u => new User
            {
                UserId = u.UserId,
                FirstName = u.UserName
            }).ToList();
            //var users = _db.Users.Where(u => u.UserRoles.Any(r => r.RoleType == UserRoleType.Admin)).ToList();
            var PagesUsers = users.Skip(pageIndex * 10).Take(10).ToList();
            return PartialView(ResultView, new SimpleUserSearchVm
            {
                PageIndex = pageIndex,
                FilteredUsers = users,
                PagedUsers = users
            });
        }

        [HttpGet]
        public PartialViewResult AddUser()
        {
            PopulateCourses();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUser(ManualUserRegVm model)
        {
            if (ModelState.IsValid)
            {
                using (var srv = new RegisterService())
                {
                    srv.RegisterManualAlumni(model);
                }
            }
            return GetErrorMsgJSON();
        }



    }
}
