using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Areas.Alumni.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class RegisterService : ServiceBase
    {
        UserService _userService = new UserService();
        public static readonly object _regSync = new object();
        ICryptographyService _hashService = new RNGCryptoService();

        public User RegisterManualAlumni(ManualUserRegVm userDetails)
        {
            lock (_regSync)
            {
                if (string.IsNullOrEmpty(userDetails.Email))
                {
                    var count = _db.Users.ToList().Last().UserId + 1;
                    userDetails.Email = "unknown_" + count + "@nitcaa.com";
                }

                _userService.ValidateEmailUnique(userDetails.Email);
                var user = userDetails.GetUserEntity();
                user.HashedPassword = _hashService.CreateHash(Constants.DefaultPassword);
                user.AccountStatus = UserRegisterStatus.Approved;
                user.EmailConfirmedOn = DateTime.UtcNow; // For Dummy Logging in
                user.CreateType = UserCreateTypes.Admin;
                user.UserRoles.Add(new UserRole
                {
                    RoleType = UserRoleType.Alumni
                });
                UpdateCourseDetails(userDetails.Batch, userDetails.BranchId, user);

                _db.Users.Add(user);
                _db.SaveChanges();
                AddUserLog(user.UserId, "User Created Manually");
                return user;
            }
        }

        public User RegisterUser(RegisterUserVm userDetails)
        {
            lock (_regSync)
            {
                var user = _db.Users.ToList()
                    .FirstOrDefault(u => u.FullName == userDetails.FirstName
                                         && u.UserCourses.Any(c => c.Batch == userDetails.Batch 
                                         && userDetails.BranchId == c.BranchId) 
                                         && (u.CreateType == UserCreateTypes.Auto || u.CreateType == UserCreateTypes.Admin));
                if (user == null)
                {
                    return RegisterNewUser(userDetails);
                }
                else
                {
                    return UpgradeAutoToManual(userDetails, user);
                }
            }
        }

        private User UpgradeAutoToManual(RegisterUserVm userDetails, User user)
        {
            if (userDetails.Email.ToLower() != user.Email.ToLower())
            {
                _userService.ValidateEmailUnique(userDetails.Email);
            }

            user.Email = userDetails.Email;
            user.MobileNumber = string.IsNullOrEmpty(userDetails.MobileNumber) ? user.MobileNumber : userDetails.MobileNumber;

            user.CreateType = UserCreateTypes.AutoManual;
            user.JoinedOn = DateTime.UtcNow;

            user.AccountStatus = UserRegisterStatus.Approved;
            user.EmailConfirmedOn = null;
            user.MobileNumber = userDetails.MobileNumber;
            user.HashedPassword = _hashService.CreateHash(userDetails.Password);

            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            AddUserLog(user.UserId, "User converted from auto to manual");
            return user;
        }

        private User RegisterNewUser(RegisterUserVm userDetails)
        {
            var user = ValidateAndUpdateCommonInfo(userDetails);
            if (userDetails.Role == UserRoleType.Alumni || userDetails.Role == UserRoleType.Student)
            {
                UpdateCourseDetails(userDetails.Batch, userDetails.BranchId, user);
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            AddUserLog(user.UserId, "User Register");
            return user;
        }

        private UserCourse UpdateCourseDetails(string batch, int branchId, User user)
        {
            var course = new UserCourse
            {
                Batch = batch,
                BranchId = branchId,
            };
            course.UpdateCourseNames();
            user.UserCourses = new List<UserCourse> { course };
            return course;
        }

        private User ValidateAndUpdateCommonInfo(RegisterUserVm userDetails)
        {
            _userService.ValidateEmailUnique(userDetails.Email);
            var user = userDetails.GetUserEntity();
            var password = userDetails.Password;
            user.AccountStatus = UserRegisterStatus.Approved;
            user.MobileNumber = userDetails.MobileNumber;
            user.HashedPassword = _hashService.CreateHash(password);
            //TODO
            //var role = _db.Roles.Single(r => r.RoleId == (byte)userDetails.Role);
            user.UserRoles.Add(new UserRole
                {
                    RoleType = userDetails.Role
                });
            return user;
        }

        public void SendEmailValidationToken(string email)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new SimpleException(string.Format("The email '{0}' is not registered", email));
            }
            SendEmailValidationToken(user);
        }

        public void SendEmailValidationToken(User user)
        {
            if (user.EmailConfirmedOn != null)
            {
                throw new SimpleException(string.Format("The email '{0}' is already confirmed", user.Email));
            }
            var token = _hashService.CreateTokenWithUserId(user.UserId, Constants.EmailVerificationTokenLen);
            user.EmailConfirmationToken = token;
            UpdateUser(user);
            MailSrv.SendPreDefMailAsync(user, MailType.EmailConfirmation, null);
            AddUserLog(user.UserId, "Email validation token send");
        }

        public bool ValidateEmailToken(string token)
        {
            var userId = _hashService.GetUserIdFromToken(token);
            var user = _db.Users.Find(userId);
            if (user.EmailConfirmationToken == token)
            {
                user.EmailConfirmedOn = DateTime.UtcNow;
            }
            else
            {
                user.EmailConfirmedOn = null;
            }
            UpdateUser(user);
            var status = user.EmailConfirmationToken == token;
            AddUserLog(user.UserId, "Email Validation : " + status.ToString());
            return status;
        }



        public User UpdateRegStatus(int userId, UserRegisterStatus status)
        {
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                new SimpleException(Strings.UserNotFoundById, userId);
            }
            user.AccountStatus = status;
            user.ApprovedOn = DateTime.UtcNow;
            _db.SaveChanges();
            if (status == UserRegisterStatus.Approved)
            {
                MailSrv.SendPreDefMailAsync(user, MailType.RegApproved, null);
            }
            AddUserLog(user.UserId, "Registration status updated : " + status.ToString());
            return user;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
            }
            catch { }
            base.Dispose(disposing);
        }
    }
}