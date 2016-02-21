using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class ConnectService : ServiceBase
    {
        static readonly object _sync = new object();

        List<Connection> _myConnects;
        public List<Connection> MyConnects
        {
            get
            {
                if (_myConnects == null)
                {
                    _myConnects = _db.Connections
                        .Where(c => c.SenderId == UserId || c.ReceiverId == UserId)
                        .ToList();
                }
                return _myConnects;
            }
        }


        int _userId = 0;
        public int UserId
        {
            get
            {
                if (_userId == 0) _userId = CurrUserId;
                return _userId;
            }
        }


        public List<Connection> GetNewConnectReq()
        {
            var connects = MyConnects
                            .Where(c => c.IsRequestSend && c.ReceiverId == UserId)
                            .ToList();
            return connects;
        }

        public List<int> GetConnectReqIds(ConnectStatusType status)
        {
            var connects = MyConnects
                            .Where(c => c.Status == status);
            var ids = connects
                    .SelectMany(c => new List<int> { c.SenderId, c.ReceiverId })
                    .ToList();
            ids.RemoveAll(i => i == UserId);
            return ids;
        }

        public void RemoveConnection(int id)
        {
            var conn = _db.Connections
                            .SingleOrDefault(c => (c.SenderId == id && c.ReceiverId == UserId)
                                            || (c.SenderId == UserId && c.ReceiverId == id));
            if (conn != null)
            {
                _db.Entry(conn).State = System.Data.EntityState.Deleted;
                _db.SaveChanges();
            }
        }

        public void CreateConnectReq(int id)
        {
            lock (_sync)
            {
                var duplicate = _db.Connections
                                    .SingleOrDefault(c => (c.SenderId == id && c.ReceiverId == UserId)
                                                         || (c.SenderId == UserId && c.ReceiverId == id));
                if (duplicate != null)
                {

                    if (duplicate.ReceiverId == UserId)
                    {
                        // Update duplicate
                        duplicate.Status = ConnectStatusType.Accepted;
                        _db.Entry(duplicate).State = System.Data.EntityState.Modified;
                        _db.SaveChanges();
                    }
                    else
                    {
                        throw new SimpleException("Request already send.");
                    }
                }
                else
                {
                    // New request
                    var user = _db.Users.Include(u => u.UserCourses).Single(u => u.UserId == UserId);
                    var connect = new Connection
                    {
                        ReceiverId = id,
                        SenderId = UserId,
                        SendOn = DateTime.UtcNow,
                        SenderName = user.FullName,
                    };
                    if (user.UserCourses.Any())
                    {
                        var course = user.UserCourses.First();
                        connect.Batch = course.Batch;
                        connect.SenderCourse = string.Format("{0}, {1}", course.BranchName, course.CourseName);
                    }
                    _db.Connections.Add(connect);
                    _db.SaveChanges();
                }
            }
        }
    }
}