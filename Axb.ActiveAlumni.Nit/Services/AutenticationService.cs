using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class AutenticationService : AuthenticationSrvBase
    {
        ICryptographyService _hashService = new RNGCryptoService();

        protected override void VerifyCredentials()
        {
            var password = _request.Password;
            var email = _request.Email.Trim();
            email = email.Trim();
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && _hashService.IsHashSame(password, user.HashedPassword))
            {
                _db.Entry(user).Collection(u => u.UserRoles).Load();
                _user = user;
            }
            else
            {
                throw new SimpleException("Invalid user name or password");
            }
        }
    }
}
