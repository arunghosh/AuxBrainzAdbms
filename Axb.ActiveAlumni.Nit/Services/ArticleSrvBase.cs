using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class ArticleSrvBase: ServiceBase
    {
        public void UpdateAlumniInfo(AlumniArticleBase article, int alumniId)
        {
            var alumni = _db.Users.Include(u => u.UserCourses).Single(u => u.UserId == alumniId);
            article.UserId = CurrUserId;
            article.AlumniId = alumni.UserId;
            if (alumni.UserCourses.Any())
            {
                var uc = alumni.UserCourses[0];
                article.Course = uc.BranchName + ", " + uc.CourseName;
                article.Batch = uc.Batch;
            }
            article.AlumniName = alumni.FullName;
        }
    }
}