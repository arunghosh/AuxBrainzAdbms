using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit
{
    public static class Constants
    {
        public static List<string> BloodGroups 
        {
            get{
                return new List<string>
                {
                    "O+",
                    "O-",
                    "A+",
                    "A-",
                    "B+",
                    "B-",
                    "AB+",
                    "AB-",
                };
            }
        }

        public const string DefaultPassword = "nitcaa123";
        public const string UnknownEmail = "unknown@nitcaa.com";
        public const string RoleKey = "USR_RLE";
        public const string IsAuthKey = "IS_AUTH";
        public const string IsAdminKey = "IS_ADMIN";
        public const string HomeImgKey = "HME_IMG";
        public const string HomeTitleKey = "HME_TITLE";
        public const string ProfileEditKey = "PR_IS_EDIT";
        public const string ViewBagMessageKey = "VB_MSG";
        //public const string EmailRegEx = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public const string PasswordRegEx = "^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$";
        public const string PasswordErrMsg = "Minimum length of password should be 8, with atleast a number and a special character !#$%&?";
        //public const string EmailErrMsg = "Enter a valid email address";
        public const string RoleAlumni = "Alumni";
        public const string RoleAdmin = "Admin";
        public const string RoleStudent = "Student";
        public const int EmailVerificationTokenLen = 30;
        public const int PasswordResetTokenLen = 55;
        public const int PasswordExpTimeHrs = 2;
    }
}