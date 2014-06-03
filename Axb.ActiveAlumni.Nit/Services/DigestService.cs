using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class DigestService : ServiceBase
    {

        public void SendDigest(int userId)
        {
            try
            {
                var user = _db.Users.Find(userId);
                if (user.TsMailDigest.AddDays(user.DigestDaysSpan) < DateTime.UtcNow)
                {
                    var services = new List<IDigestService>
                    {
                        new NewsSrv(),
                        new EventSrv(),
                        new MentorSrv(),
                        new DiscussionSrv(),
                        new MessageSrv(),
                    };
                    var userSrv = new UserDigest(userId, services);
                    MailSrv.SendNewsletterAsync(user, userSrv.GetDigest(), "NITCAA Newsletter", null);
                    //user.TsMailDigest = DateTime.UtcNow;
                    //UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                AddLog(userId + "#SendDigest-Failure#"  + ex.Message, LogTypes.Error, "Server");
            }
        }
    }
}