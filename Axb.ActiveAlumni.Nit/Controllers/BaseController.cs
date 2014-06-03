using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Diagnostics;
using System.Threading;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    [AxbAuthorize]
    public abstract class BaseController : Controller
    {
        public static PageTypes CurrentPage { get; protected set; }

        protected bool IsAuth
        {
            get
            {
                try
                {
                    var stat = System.Web.HttpContext.Current.Request.IsAuthenticated;
                    return stat;
                }
                catch
                {
                    return false;
                }

            }
        }

        protected string HostAdress
        {
            get
            {
                return Request.UserHostAddress;
            }
        }

        protected UserService _userService = new UserService();
        protected AlumniDbContext _db = new AlumniDbContext();

        protected User CurrentUser
        {
            get
            {
                var user = _db.Users.Find(CurrentUserId);
                return user;
            }
        }

        protected int CurrentUserId
        {
            get
            {
                var userId = UserSession.CurrentUserId;
                return userId;
            }
        }

        protected User GetUserByIdOrCurrent(int? id)
        {
            return id == null ? GetCurrentUser() : _db.Users.Find(id);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                _db.Dispose();
                _userService.Dispose();
            }
            catch { }
            base.Dispose(disposing);
        }

        protected void FillAuthKeys()
        {
            ViewData[Constants.IsAuthKey] = IsAuth;
            ViewData[Constants.IsAdminKey] = IsAuth ? CurrentUser.IsAdmin() : false;
        }

        protected void LogUnAuth()
        {
            _userService.LogUnAuth(HostAdress);
            ModelState.AddModelError("", Strings.UnAuthAccess);
        }

        protected void AddModelError(SimpleException ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        [NonAction]
        protected void UpdateEntity(User user)
        {
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        protected User GetCurrentUser()
        {
            var user = _db.Users.Find(CurrentUserId);
            return user;
        }

        [NonAction]
        public virtual ActionResult SafeRedirect(string url = null, bool isAuth = false)
        {
            return Redirect(GetRedirectUrl(url, isAuth));
        }

        public virtual string GetRedirectUrl(string url = null, bool isAuth = false)
        {
            if (!String.IsNullOrWhiteSpace(url)
                && Url.IsLocalUrl(url)
                && url.Length > 1
                && url.StartsWith("/", StringComparison.Ordinal)
                && !url.StartsWith("//", StringComparison.Ordinal)
                && !url.StartsWith("/\\", StringComparison.Ordinal))
            {
                return url;
            }

            if (isAuth)
            {
                var request = System.Web.HttpContext.Current.Request;
                var context = System.Web.HttpContext.Current;
                HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var roles = authTicket.UserData.Split('|');
                    if (roles.Contains(Constants.RoleAlumni))
                    {
                        url = Routes.NavigationItems[PageTypes.User].TinyUrl;
                    }
                    else if (roles.Contains(Constants.RoleStudent))
                    {
                        url = Routes.NavigationItems[PageTypes.User].TinyUrl;
                    }
                    else if (roles.Contains(Constants.RoleAdmin))
                    {
                        url = Routes.NavigationItems[PageTypes.AdminDashboard].TinyUrl;
                    }
                    else
                    {
                        url = Routes.NavigationItems[PageTypes.User].TinyUrl;
                    }
                }
            }
            url = String.IsNullOrWhiteSpace(url) ? Routes.RootHome : url;
            return url;
        }


        [NonAction]
        protected List<string> GetModelStateErrorMsgs()
        {
            return ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
        }


        [NonAction]
        protected JsonResult GetErrorMsgJSON()
        {
            var errMsgs = GetModelStateErrorMsgs();
            var jsonResult = new
            {
                errMsg = errMsgs.Any() ? errMsgs[0] : null,
            };
            return Json(jsonResult);
        }

        [NonAction]
        protected void PopulateCourses(int branchId = 1)
        {
            var branch = _db.Branches.Find(branchId);
            PopulateCourses(branch.CourseId, branchId);
        }

        [NonAction]
        protected void PopulateCourses(int courseId, int branchId)
        {
            var courses = DbCache.Courses;
            var branches = DbCache.Branches.Where(b => b.CourseId == courseId).ToList();
            ViewBag.Course = courses.SingleOrDefault(c => c.CourseId == courseId).Name;
            ViewBag.Branch = branches.SingleOrDefault(b => branchId == b.BranchId).Name;
            ViewBag.CourseId = new SelectList(courses, "CourseId", "Name", courseId);
            ViewBag.BranchId = new SelectList(branches, "BranchId", "Name", branchId);
            ViewBag.Batches = new SelectList(DbCache.Batches);
            ViewBag.Branches = new SelectList(DbCache.Branches.Select(c => c.Name));
        }

        #region Autocomplete

        [HttpGet]
        public JsonResult GetUserNames(string term)
        {
            var data = AutoHelper.UserNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutoUserNames(string term)
        {
            var data = AutoHelper.AutoNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserByName(string term)
        {
            term = term.ToLower();
            var jsonData = new List<AutoComUserInfo>();

            int intBatch;
            var batchTerm = term.Length > 4 ? term.Substring(0, 4) : term;
            if (int.TryParse(batchTerm, out intBatch))
            {
                var filtered = DbCache.BatcheCourse.Where(u => u.ToLower().Contains(term)).Take(6);
                foreach (var item in filtered)
                {
                    var userInfo = new AutoComUserInfo
                    {
                        Name = item,
                        Id = 0,
                    };
                    jsonData.Add(userInfo);
                }
            }
            else
            {
                var users = DbCache.UserSearch
                                .Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(term))
                                .Take(4)
                                .ToList();

                foreach (var item in users)
                {
                    string course = "--";
                    string batch = "--";

                    if (item.UserCourses.Any())
                    {
                        var uc = item.UserCourses.First();
                        course = string.Format("{0}, {1}", uc.BranchName, uc.CourseName);
                        batch = uc.Batch;
                    }
                    var userInfo = new AutoComUserInfo
                    {
                        Name = item.FullName,
                        Id = item.UserId,
                        Course = course,
                        Batch = batch
                    };
                    jsonData.Add(userInfo);
                }
            }
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCompanyNames(string term)
        {
            var data = AutoHelper.CompanyNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLocations(string term)
        {
            var data = AutoHelper.CityNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCities(string term)
        {
            var data = AutoHelper.CityNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBatches(string term)
        {
            var data = AutoHelper.Batches(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCourses(string term)
        {
            var data = AutoHelper.CouseNames(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDegree(string term)
        {
            var data = AutoHelper.Degrees(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobPostOrgs(string term)
        {
            var data = AutoHelper.JobPostOrgs(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobPositions(string term)
        {
            var data = AutoHelper.JobPostions(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobDomains(string term)
        {
            var data = AutoHelper.JobDomains(term);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 600, VaryByParam = "id")]
        public ImageResult ProfileImage(int id)
        {
            return _userService.GetImage(id, 50, 50);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 5000, VaryByParam = "id")]
        public ImageResult SearchImage(int id)
        {
            return _userService.GetImageCrop(id, 60, 60);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 5000, VaryByParam = "id")]
        public ImageResult SmallImage(int id)
        {
            return _userService.GetImage(id, 35, 35);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 5000, VaryByParam = "id")]
        public ImageResult Image40(int id)
        {
            return _userService.GetImage(id, 40, 40);
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 5000, VaryByParam = "id")]
        public ImageResult ImageTiny(int id)
        {
            return _userService.GetImage(id, 30, 30);
        }

        [HttpGet]
        public JsonResult GetProfileSkills(string term)
        {
            var skills = AutoHelper.ProfileSkills(term);
            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJoinedSkills(string term)
        {
            var skills = AutoHelper.JoinedSkills(term);
            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobSkills(string term)
        {
            var skills = AutoHelper.JobSkills(term);
            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobLocations(string term)
        {
            var skills = AutoHelper.JobPostLocations(term);
            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSrvNames(string term)
        {
            var rslt = AutoHelper.GetSrvNames(term);
            return Json(rslt, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSrvCtgrys(string term)
        {
            var rslt = AutoHelper.GetSrvCtgrys(term);
            return Json(rslt, JsonRequestBehavior.AllowGet);
        }


        #endregion

        //TODO : Dispose

        //#region Dispose Methods

        //bool disposed = false;

        //protected override void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            //dispose managed ressources
        //        }
        //    }

        //    //dispose unmanaged ressources
        //    if (_db != null)
        //    {
        //        _db.Dispose();
        //    }
        //    disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //#endregion
    }
}
