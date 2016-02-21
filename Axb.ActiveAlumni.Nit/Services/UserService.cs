using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private RNGCryptoService _hashService = new RNGCryptoService();

        public void SendPasswordResetToken(string email)
        {
            var user = FindUserByEmail(email);
            if (user == null)
            {
                throw new SimpleException(Strings.EmailNotReg);
            }
            user.PasswordResetToken = _hashService.CreateTokenWithUserId(user.UserId, Constants.PasswordResetTokenLen);
            user.PasswordExpiryTime = DateTime.UtcNow.Add(TimeSpan.FromHours(Constants.PasswordExpTimeHrs));
            UpdateUser(user);
            MailSrv.SendPreDefMailAsync(user, MailType.ForgotPassword, null);
        }

        public User ValidatePasswordToken(string token)
        {
            var userId = _hashService.GetUserIdFromToken(token);
            var user = _db.Users.Find(userId);

            if (DateTime.UtcNow > user.PasswordExpiryTime)
            {
                throw new SimpleException(Strings.TokenExpired);
            }

            if (token.Length < Constants.PasswordResetTokenLen || user.PasswordResetToken != token)
            {
                throw new SimpleException(Strings.InvalidToken);
            }

            return user;
        }

        public User FindUserByEmail(string email)
        {
            email = email.Trim();
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            return user;
        }

        public void ValidateEmailUnique(string email)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {
                throw new SimpleException(Strings.EmailAlreadyInUse, email);
            }
        }

        public void ChangePassword(string newPassword, string oldPassword)
        {
            var user = _db.Users.Find(UserSession.CurrentUserId);
            if (!_hashService.IsHashSame(oldPassword, user.HashedPassword))
            {
                throw new SimpleException("Current password entered is wrong");
            }
            user.HashedPassword = _hashService.CreateHash(newPassword);
            user.PasswordChangedOn = DateTime.UtcNow;
            UpdateUser(user);
        }

        public void ResetPassword(string token, string newPassword)
        {
            var user = ValidatePasswordToken(token);
            user.PasswordResetToken = string.Empty;
            user.PasswordExpiryTime = null;
            user.HashedPassword = _hashService.CreateHash(newPassword);
            user.PasswordChangedOn = DateTime.UtcNow;
            UpdateUser(user);
        }

        public ImageResult GetImageCrop(int id, int height, int width)
        {
            User user = (id == 0) ? null : _db.Users.Find(id);
            ImageResult result;
            if (user == null || user.ImageData == null || user.ImageData.Length == 0)
            {
                var path = HttpContext.Current.Server.MapPath(@"~\Content\images\no_profile.jpg");
                result = ImageSrv.ThumbnailCrop(Image.FromFile(path), height, width);
            }
            else
            {
                try
                {
                    result = ImageSrv.ThumbnailCrop(Image.FromStream(new MemoryStream(user.ImageData)), height, width);
                }
                catch
                {
                    var path = HttpContext.Current.Server.MapPath(@"~\Content\images\no_profile.jpg");
                    result = ImageSrv.ThumbnailCrop(Image.FromFile(path), height, width);
                }
            }
            return result;
        }

        public ImageResult GetImage(int id, int height, int width)
        {
            User user = (id == 0) ? null : _db.Users.Find(id);
            ImageResult result;
            if (user == null || user.ImageData == null || user.ImageData.Length == 0)
            {
                var path = HttpContext.Current.Server.MapPath(@"~\Content\images\no_profile.jpg");
                result = ImageSrv.Thumbnail(Image.FromFile(path), height, width);
            }
            else
            {
                try
                {
                    result = ImageSrv.Thumbnail(Image.FromStream(new MemoryStream(user.ImageData)), height, width);
                }
                catch
                {
                    var path = HttpContext.Current.Server.MapPath(@"~\Content\images\no_profile.jpg");
                    result = ImageSrv.Thumbnail(Image.FromFile(path), height, width);
                }
            }
            return result;
        }
    }
}