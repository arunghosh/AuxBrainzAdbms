using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using RestSharp;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class MailSrv
    {
        readonly static bool EN_MAIL_NT = true;
        static Dictionary<MailType, MailContent> MailMap { get; set; }
        const string SIGN = "<br/>Thank you,<br/>NITC Alumni Association<br/>www.nitcalumni.com";
        const string HEADER = "<div style='padding: 10px 5px 7px 10px;font-size: 15px;font-family:Arial;background: #36a;border-bottom:1px solid #048;color: #f9f9f9;font-weight: bold;border-radius: 2px 2px 0 0;'>NITC Alumni Association</div>";
        const string NEWS_HEADER = "<div style='padding: 15px 5px 7px 20px;font-size: 16px;font-family:Arial;background: #443;border-bottom:1px solid #048;color: #ccc;font-weight: bold;border-radius: 5px 5px 0 0;'>NITC Alumni Association<span style='float:right;margin-right:15px;;font-size:15px;color:#909090;'>Newsletter</span></div>";

        static MailSrv()
        {
            InitMailMap();
        }

        private static void InitMailMap()
        {
            MailMap = new Dictionary<MailType, MailContent>();

            AddMailContent(new MailContent
            {
                MailType = MailType.RegAcknowledge,
                Subject = "Registration",
                Message = "Thank you registering."
            });

            AddMailContent(new MailContent
            {
                MailType = MailType.RegApproved,
                Subject = "NITCAA Registration Confirmation",
                Message = "<div>Thank you for registering with NITCAA. Your registration is confirmed.</div> <div>You can now <a href='" + Routes.Root + "'>log on to your NITCAA account</a></div>",
                IsHtml = true,
            });

            AddMailContent(new MailContent
            {
                MailType = MailType.EmailConfirmation,
                Subject = "NITCAA Email Verification",
                IsHtml = true,
                Message = @"<a href='" + Routes.Root + @"Register/Confirm/{0}'>Click here to verify your email address</a>"
            });

            AddMailContent(new MailContent
            {
                MailType = MailType.ForgotPassword,
                Subject = "You request for new NITCAA Password",
                IsHtml = true,
                Message = @"<div>You recently requested to reset your NITCAA password. </div><div><a href='" + Routes.Root + @"Settings/ResetPassword/{0}'>Click this link to set new password</a></div><br/><div>Due to security reason, this link will expire in 2 hours.</div>"
            });
        }

        public static void SendPreDefMailAsync(User user, MailType type, Action<bool> callBack)
        {
            if (EN_MAIL_NT)
            {
                new Thread(new ThreadStart(() =>
                {
                    bool status = true;
                    try
                    {
                        var content = MailMap[type];
                        var msg = GetCompleteMessage(type, user);
                        var fullMsg  = AddHeaderFooter(msg.Replace("\n", "<br/>").Replace("\r", ""), user);
                        SendMail(user, fullMsg, content.Subject);
                    }
                    catch
                    {
                        status = false;
                    }
                    if (callBack != null) callBack(status);
                }))
                .Start();
            }
        }

        public static void SendMailAsync(User user, string message, string subject, Action<bool> callBack)
        {
            new Thread(new ThreadStart(() =>
            {
                bool status = true;
                try
                {
                    SendMail(user, AddHeaderFooter(message.Replace("\n", "<br/>").Replace("\r", ""), user), subject);
                }
                catch
                {
                    status = false;
                }
                if (callBack != null) callBack(status);
            }))
            .Start();
        }

        public static void SendNewsletterAsync(User user, string message, string subject, Action<bool> callBack)
        {
            new Thread(new ThreadStart(() =>
            {
                bool status = true;
                try
                {
                    SendMail(user, AddNewsLetterHeaderFooter(message.Replace("\n", "<br/>").Replace("\r", ""), user), subject);
                }
                catch
                {
                    status = false;
                }
                if (callBack != null) callBack(status);
            }))
            .Start();
        }

        public static bool SendMail(User user, string message, string subject)
        {
            bool status = true;
            try
            {
                RestSharp.RestClient client = new RestClient();
                client.BaseUrl = "https://api.mailgun.net/v2";
                client.Authenticator =
                        new HttpBasicAuthenticator("api",
                                                   "key-3eipp68fja382ul8imvyjoiao1qvggw5");
                RestRequest request = new RestRequest();
                request.AddParameter("domain",
                                     "smtp.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "nitcalumni.com/messages";
                request.AddParameter("from", "NITC Alumni Association<postmaster@nitcalumni.com>");
                request.AddParameter("to", user.Email);
                //request.AddParameter("to", "georgevivek@gmail.com");
                //request.AddParameter("to", "arunghosh@gmail.com");
                request.AddParameter("subject", subject);
                request.AddParameter("html", message);
                request.Method = Method.POST;
                client.Execute(request);
            }
            catch
            {
                status = false;
            }
            return status;
        }

        public static void SendMailToManyAsync(IEnumerable<User> users, string message, string subject, Action<bool> callBack = null)
        {
            if (EN_MAIL_NT)
            {
                new Thread(new ThreadStart(() =>
                {
                    bool status = true;
                    try
                    {
                        foreach (var user in users)
                        {
                            SendMail(user, message, subject);
                        }
                    }
                    catch
                    {
                        status = false;
                    }
                    if (callBack != null) callBack(status);
                }))
                .Start();
            }
        }

        static string AddHeaderFooter(string message, User user)
        {
            message = "Hi " + user.FullName + ",<br/><br/>" +  message;
            message += SIGN;
            message = HEADER + "<div style='background:#f0f2f5;font-family:Arial;padding:20px;color:#444;line-height:22px'>" + message + "</div><br/><hr/><div style='color:#777;'>Note: This is a system generated mail. Please do not reply to this mail.</div>";
            return message;
        }

        static string AddNewsLetterHeaderFooter(string message, User user)
        {
            message += SIGN;
            message = NEWS_HEADER + "<div style='background:#fff;border:1px solid #ddd;border-top:1px solid #000;font-family:Arial;padding:20px;color:#444;line-height:22px'>" + message + "</div><br/><hr/><div style='color:#777;'>Note: This is a system generated mail. Please do not reply to this mail.</div>";
            return message;
        }

        static string GetCompleteMessage(MailType type, User user)
        {
            var msg = MailMap[type].Message;
            switch (type)
            {
                case MailType.RegAcknowledge:
                    break;
                case MailType.RegApproved:
                    break;
                case MailType.ForgotPassword:
                    msg = string.Format(msg, user.PasswordResetToken);
                    break;
                case MailType.Notification:
                    break;
                case MailType.EmailConfirmation:
                    msg = string.Format(msg, user.EmailConfirmationToken);
                    break;
                default:
                    break;
            }
            return msg;
        }

        public void SendMail(List<string> emailId, string message)
        {

        }

        private static void AddMailContent(MailContent content)
        {
            MailMap.Add(content.MailType, content);
        }
    }

    public class MailContent
    {
        public MailType MailType { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        public MailContent()
        {
            IsHtml = false;
        }
    }

    public enum MailType
    {
        RegAcknowledge,
        RegApproved,
        ForgotPassword,
        Notification,
        EmailConfirmation
    }
    // Welcome Message
    // Confirmation Message
    // Password Recovery
}