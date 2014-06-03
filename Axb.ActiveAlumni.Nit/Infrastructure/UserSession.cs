using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit
{
    public class SessionUserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class UserSession
    {
        public static void ClearSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session.Abandon();
                UpdateCurrentUser(null, null);
            }
            FormsAuthentication.SignOut();
        }

        public static string CurrentUserName
        {
            get
            {
                var info = GetCurrentUserInfo();
                if (info == null)
                {
                    ClearSession();
                    throw new Exception(Strings.SessionExpired);
                }
                else
                {
                    return info.Name;
                }
            }
        }

        public static int CurrentUserId
        {
            get
            {
                var info = GetCurrentUserInfo();
                if(info == null)
                {
                    ClearSession();
                    throw new Exception(Strings.SessionExpired);
                }
                else
                {
                    return info.Id;
                }
            }
        }

        private static SessionUserInfo GetCurrentUserInfo()
        {
            var httpContext = System.Web.HttpContext.Current;
            var userId = httpContext.Session[SessionKeys.CurrentUserId];
            if (userId == null)
            {
                var context = System.Web.HttpContext.Current;
                var request = System.Web.HttpContext.Current.Request;
                HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var roles = authTicket.UserData.Split('|');
                    using (var db = new AlumniDbContext())
                    {
                        var user = db.Users.Find(int.Parse(authTicket.Name));
                        if (user == null)
                        {
                            return null;
                        }
                        userId = user.UserId;
                        UserSession.UpdateCurrentUser(user.FullName, authTicket.Name);
                    }
                }
            }
            return new SessionUserInfo
            {
                Id = int.Parse(userId.ToString()),
                Name = httpContext.Session[SessionKeys.CurrentUserName] as string
            };
        }

        public static void UpdateCurrentUser(string userFullName, string userId)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[SessionKeys.CurrentUserId] = userId;
                session[SessionKeys.CurrentUserName] = userFullName;
            }
        }

    }
}