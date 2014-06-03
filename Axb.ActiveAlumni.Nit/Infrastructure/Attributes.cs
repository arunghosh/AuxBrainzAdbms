using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Axb.ActiveAlumni.Nit
{
    public class PasswordAttribute : ValidationAttribute
    {
        public string number = "1234567890";
        public string atoz = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string special = @"` ~ ! @ # $ % ^ & * ( ) _ - + = { } [ ] \ | : ; "" ' < > , . ? /";
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Password field is required";
                return false;
            }
            var pswd = value as string;
            if (pswd.Length > 20)
            {
                ErrorMessage = "The lenght of password should be less than 20";
                return false;
            }
            else if (pswd.Length < 7)
            {
                ErrorMessage = "The password should be atlest 8 character long";
                return false;
            }

            if (!number.ToArray().Any(n => pswd.Contains(n)))
            {
                ErrorMessage = "Password should contain atleast one number";
                return false;
            }

            //if (!atoz.ToArray().Any(n => pswd.Contains(n)))
            //{
            //    ErrorMessage = "Password should contain atleast one uppercase letter";
            //    return false;
            //}

            //if (!special.ToArray().Any(n => pswd.Contains(n)))
            //{
            //    ErrorMessage = "Password should contain atleast one special character";
            //    return false;
            //}

            return true;
        }
    }


    public class ConnectionMsgLengthAttribute : StringLengthAttribute
    {
        public ConnectionMsgLengthAttribute()
            : base(512)
        {

        }
    }


    public class MentorMsgLengthAttribute : StringLengthAttribute
    {
        public MentorMsgLengthAttribute()
            : base(1024)
        {

        }
    }

    public class MessageLengthAttribute : StringLengthAttribute
    {
        public MessageLengthAttribute()
            : base(1024)
        {
        }
    }

    public class TitleLengthAttribute : StringLengthAttribute
    {
        public TitleLengthAttribute()
            : base(256)
        {

        }
    }

    public class FullNameLengthAttribute : StringLengthAttribute
    {
        public FullNameLengthAttribute()
            : base(128)
        {

        }
    }
}