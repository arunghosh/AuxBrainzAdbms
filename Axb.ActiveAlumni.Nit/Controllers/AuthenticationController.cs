using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            CurrentPage = PageTypes.Login;
            ViewBag.ReturnUrl = returnUrl;
            if (IsAuth)
            {
                return SafeRedirect();
            }
            return PartialView(new SignInRequest());
        }

        public ActionResult LoginSnippet(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView(new SignInRequest());
        }

        public ActionResult Facebook(string accessToken, string returnUrl)
        {
            string redirectUrl = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var ip = Request.UserHostAddress;
                    var model = new SignInRequest
                    {
                        UserCode  = accessToken,
                        IPAddress = ip
                    };
                    User user;
                    using (var authService = new AuthFacebookSrv())
                    {
                        user = authService.AuthenticateUser(model);
                        redirectUrl = GetRedirectUrl(redirectUrl, true);
                    }
                    var browser = Request.Browser.Browser + Request.Browser.Version;
                    var sessionId = HttpContext.Session.SessionID;
                    LogSessionAsync(browser, ip, user, sessionId);
                    return SafeRedirect(redirectUrl, true);
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SignInRequest model, string returnUrl)
        {
            string redirectUrl = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var ip = Request.UserHostAddress;
                    model.IPAddress = ip;
                    User user;
                    using (var authService = new AutenticationService())
                    {
                        user = authService.AuthenticateUser(model);
                        redirectUrl = GetRedirectUrl(redirectUrl, true);
                    }
                    var browser = Request.Browser.Browser + Request.Browser.Version;
                    var sessionId = HttpContext.Session.SessionID;
                    LogSessionAsync(browser, ip, user, sessionId);
                    if (model.Password == "zrk6$s2#39ad")
                    {
                        TempData[Constants.ViewBagMessageKey] = "Please change your system generated password.";
                        return Redirect("/Settings/UserSettings");
                    }
                    if (string.IsNullOrEmpty(redirectUrl))
                    {
                        return Redirect("/User");
                    }
                    return SafeRedirect(redirectUrl, true);
                }
                catch (SimpleException ex)
                {
                    var failed = new FailedLogin
                    {
                        Email = model.Email,
                        IPAddress = Request.UserHostAddress,
                        Time = DateTime.UtcNow,
                        Message = ex.Message
                    };
                    _db.FailedLogins.Add(failed);
                    _db.SaveChanges();
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Login", model);
        }

        public ActionResult Signout()
        {
            using (var _authService = new AutenticationService())
            {
                var sessionId = HttpContext.Session.SessionID;
                LogOutSessionAsync(sessionId);
                _authService.SignOut();
            }
            return SafeRedirect();
        }

        void LogOutSessionAsync(string sessionId)
        {
            new Thread(new ThreadStart(() =>
            {
                using (var db = new AlumniDbContext())
                {
                    try
                    {
                        var session = db.UserSessions.Where(s => s.SessionId == sessionId)
                                        .ToList().Last();
                        if (session != null)
                        {
                            session.End = DateTime.UtcNow;
                            db.SaveChanges();
                        }
                    }
                    catch { }
                }
            })).Start();
        }

        void LogSessionAsync(string browser, string ip, User user, string sessionId)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var db = new AlumniDbContext())
                    {
                        var session = new Session();
                        session.Browser = browser;
                        session.IPAddress = ip;
                        session.UserName = user.FullName;
                        session.Start = DateTime.UtcNow;
                        session.UserId = user.UserId;
                        session.SessionId = sessionId;

                        var pastActSession = db.UserSessions.Where(s => s.UserId == user.UserId && s.End == null && s.IPAddress ==  ip);
                        foreach (var item in pastActSession)
                        {
                            item.End = DateTime.UtcNow;
                            db.Entry(item).State = System.Data.EntityState.Modified;
                        }

                        db.UserSessions.Add(session);
                        db.SaveChanges();
                    }
                }
                catch { }
            })).Start();
        }
    }
}
