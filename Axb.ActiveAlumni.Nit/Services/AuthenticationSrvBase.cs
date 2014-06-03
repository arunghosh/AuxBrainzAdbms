using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public abstract class AuthenticationSrvBase: ServiceBase, IAutenticationService
    {
        protected User _user = null;
        protected SignInRequest _request;

        public User AuthenticateUser(SignInRequest request)
        {
            _request = request;
            ValidateIPFailure();
            VerifyCredentials();
            ValidateEmailVerification();
            ValidateUserRegisterStatus();
            SetAuthCookie();
            return _user;
        }

        protected void ValidateIPFailure()
        {
            var time5m = DateTime.UtcNow.AddMinutes(-6);
            var failures = _db.FailedLogins.Where(p => p.IPAddress == _request.IPAddress && p.Time > time5m);
            if (failures.Count() > 4)
            {
                throw new SimpleException("Login has been blocked for 5 mins due to 5 failed login attempts.");
            }
        }

        protected abstract void VerifyCredentials();

        protected void ValidateEmailVerification()
        {
            if (_user.EmailConfirmedOn == null)
            {
                throw new SimpleException("Email verification pending");
            }
        }

        protected void ValidateUserRegisterStatus()
        {
            string msg = string.Empty;

            switch (_user.AccountStatus)
            {
                case UserRegisterStatus.Pending:
                    msg = "User approval is pending";
                    break;
                case UserRegisterStatus.Rejected:
                    msg = "User registration request was rejected";
                    break;
                default:
                    return;
            }
            throw new SimpleException(msg);
        }

        protected void SetAuthCookie()
        {
            string formattedRoles = String.Empty;
            formattedRoles = String.Join("|", _user.UserRoles.Select(r => r.RoleType.ToString()));

            HttpContext context = HttpContext.Current;

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: _user.UserId.ToString(),
                issueDate: DateTime.UtcNow,
                expiration: DateTime.UtcNow.AddDays(30),
                isPersistent: true,
                userData: formattedRoles
                );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var formsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                //Secure = _configuration.RequireSSL
            };
            context.Response.Cookies.Add(formsCookie);

            //if (_configuration.RequireSSL)
            //{
            //    // Drop a second cookie indicating that the user is logged in via SSL (no secret data, just tells us to redirect them to SSL)
            //    context.Response.Cookies.Add(new HttpCookie(ForceSSLCookieName, "true"));
            //}
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            UserSession.ClearSession();
        }
    }
}