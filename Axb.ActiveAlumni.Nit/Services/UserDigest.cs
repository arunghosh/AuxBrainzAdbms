using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class UserDigest : ServiceBase
    {
        int _userId;
        List<IDigestService> _services;
        public UserDigest(int userId, List<IDigestService> services)
        {
            _userId = userId;
            _services = services;
        }

        public string GetDigest()
        {
            var htmlCompser = new HtmlComposer();
            foreach (var srv in _services)
            {
                var digest = ComposerDigest(srv);
                htmlCompser.AppendDiv(digest);
            }
            var msg = htmlCompser.Text;
            return msg;
        }

        //private string EventSummay
        //{
        //    get
        //    {
        //        var htmlCompser = new HtmlComposer();
        //        using (var evtSrv = new EventSrv())
        //        {
        //            var events = evtSrv.GetDigestEvents(_userId);
        //            if (events.Any())
        //            {
        //                htmlCompser.AppendHead("Upcoming Events");
        //                foreach (var evt in events)
        //                {
        //                    htmlCompser
        //                        .AppendRaw(evt.GetDisgest())
        //                        .AppendHrDotted();
        //                }
        //            }
        //        }
        //        var msg = htmlCompser.Text.ToString();
        //        return msg;
        //    }
        //}

        //private string DiscussionSummay
        //{
        //    get
        //    {
        //        var htmlCompser = new HtmlComposer();
        //        using (var srv = new DiscussionSrv())
        //        {
        //            var dicussions = srv.GetDigest(_userId);
        //            if (dicussions.Any())
        //            {
        //                htmlCompser.AppendHead("Active Discussions");
        //                foreach (var evt in dicussions)
        //                {
        //                    htmlCompser
        //                        .AppendRaw(evt.GetDisgest())
        //                        .AppendHrDotted();
        //                }
        //            }
        //        }
        //        var msg = htmlCompser.Text.ToString();
        //        return msg;
        //    }
        //}


        string ComposerDigest(IDigestService service)
        {
            var htmlCompser = new HtmlComposer();
            var items = service.GetDigest(_userId);
            if (items.Any())
            {
                htmlCompser.AppendHead(service.GetDigestTitle());
                var last = items.Last();
                foreach (var item in items)
                {
                    htmlCompser
                        .AppendRaw(item.GetDisgest());
                    if(item != last)
                    {
                        htmlCompser.AppendHrDotted();
                    }
                }
                htmlCompser.AppendHr();
                htmlCompser.AppendBr();
            }
            return htmlCompser.Text;
        }

    }
}