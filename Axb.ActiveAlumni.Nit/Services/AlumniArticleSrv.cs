using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class AlumniSpeakSrv: ArticleSrvBase
    {
        public List<AlumniSpeak> GetActiveArticles()
        {
            var list = _db.AlumnisSpeak.Where(a => !a.IsDeleted).ToList();
            list.Reverse();
            return list;
        }

        public List<AlumniSpeak> GetAllArticles()
        {
            var list = _db.AlumnisSpeak.ToList();
            list.Reverse();
            return list;
        }

        public void AddOrUpdate(AlumniSpeak model, List<int> userIds)
        {
            var alumniId = model.AlumniId == 0 ? userIds[0] : model.AlumniId;
            UpdateAlumniInfo(model, alumniId);
            if (model.EntityKey == 0)
            {
                _db.AlumnisSpeak.Add(model);
            }
            else
            {
                _db.Entry(model).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
        }
    }
}