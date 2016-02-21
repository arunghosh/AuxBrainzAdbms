using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class AlumniKnowSrv: ArticleSrvBase
    {
        public List<AlumniToKnow> GetActiveArticles()
        {
            var list = _db.AlumnisToKnow.Where(a => !a.IsDeleted).ToList();
            list.Reverse();
            return list;
        }

        public List<AlumniToKnow> GetAllArticles()
        {
            var list = _db.AlumnisToKnow.Include(x => x.Affinities).ToList();
            list.Reverse();
            return list;
        }

        public void AddOrUpdate(AlumniToKnow model)
        {
            //var alumniId = model.AlumniId == 0 ? userIds[0] : model.AlumniId;
            UpdateAlumniInfo(model, CurrUserId);
            if (model.EntityKey == 0)
            {
                _db.AlumnisToKnow.Add(model);
            }
            else
            {
                _db.Entry(model).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
        }
    }
}