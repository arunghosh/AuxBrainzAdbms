using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class NewsSrv : ServiceBase, IDigestService
    {
        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.News).OrderByDescending(n => n.Date).ToList().Take(4);
            return news;
        }

        public string GetDigestTitle()
        {
            return "Alumni News";
        }
    }

    public class BlogSrv : ServiceBase, IDigestService
    {
        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.AlumniStory).OrderByDescending(n => n.Date).ToList().Take(3);
            return news;
        }

        public string GetDigestTitle()
        {
            return "Blogs";
        }
    }

    public class JobSrv : ServiceBase, IDigestService
    {
        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var news = _db.JobOpenings.Where(n => n.IsActive).OrderByDescending(n => n.PostedOn).ToList().Take(3);
            return news;
        }

        public string GetDigestTitle()
        {
            return "Job Openings";
        }
    }
}