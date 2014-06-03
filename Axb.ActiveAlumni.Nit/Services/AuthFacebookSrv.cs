using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Facebook;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class AuthFacebookSrv : AuthenticationSrvBase
    {
        protected override void VerifyCredentials()
        {
            var fbClient = new FacebookClient();
            fbClient.AccessToken = _request.UserCode; // HttpContext.Session["fb_at"] as string;
            dynamic userInfo = fbClient.Get("me?fields=id,email");
            string email = userInfo.email;
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                _user = user;
            }
            else
            {
                throw new SimpleException("There is no user account for email address - " + email);
            }
        }
    }
}