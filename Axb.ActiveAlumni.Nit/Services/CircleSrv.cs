using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class CircleSrv : ServiceBase
    {
        public List<Circle> MyCircles
        {
            get
            {
                return _db.Circles.Where(c => c.UserId == CurrUserId).ToList();
            }
        }

        public void UpdateCircle(int circleId, string name, List<int> userIds)
        {
            var circle = _db.Circles.Include(c => c.Members).Single(c => c.CircleId == circleId);
            if (circle.UserId != CurrUserId)
            {
                throw new Exception(Strings.UnAuthAccess);
            }
            if (circle.Name != name)
            {
                ValidateCircleName(name);
            }
            circle.Members = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            circle.Name = name;
            _db.Entry(circle).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        public void AddCircle(string name, List<int> userIds)
        {
            ValidateCircleName(name);
            var circle = new Circle
            {
                Members = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList(),
                Name = name,
                UserId = CurrUserId
            };
            _db.Circles.Add(circle);
            _db.SaveChanges();
        }

        private void ValidateCircleName(string name)
        {
            if (MyCircles.Any(c => c.Name == name))
            {
                throw new SimpleException(Strings.CircleDuplicate);
            }
        }
    }
}