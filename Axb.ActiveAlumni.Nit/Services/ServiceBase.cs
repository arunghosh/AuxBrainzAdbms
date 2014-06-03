﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class ServiceBase : IDisposable
    {
        protected AlumniDbContext _db = new AlumniDbContext();
        private int? _currUserId = null;
        protected int CurrUserId
        {
          get {
              if (_currUserId == null)
              {
                  try
                  {
                      _currUserId = UserSession.CurrentUserId;
                  }
                  catch { }
              }
              return _currUserId ?? 0;
          }
        }
        public ServiceBase()
        {

        }

        #region Dispose Methods

        bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed ressources
                }
            }

            //dispose unmanaged ressources
            if (_db != null)
            {
                _db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected void UpdateUser(User user)
        {
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        public void LogUnAuth(string ip)
        {
            AddLog(Strings.UnAuthAccess, LogTypes.UnAuth, ip);
        }

        public void AddLog(string msg, LogTypes type, string ip)
        {
            var log = new Log
            {
                Date = DateTime.UtcNow,
                LogMessage = msg, 
                UserId = UserSession.CurrentUserId,
                LogType = type,
                IPAddress = ip
            };
            _db.Logs.Add(log);
            _db.SaveChanges();
        }

        public void AddUserLog(int userId, string message)
        {
            var curr = _db.Users.Find(CurrUserId == 0 ? userId : CurrUserId);
            var log = new UserLog
            {
                Comment = message,
                ByUserId = CurrUserId,
                ByUserName = curr.FullName,
                UserId = userId
            };
            _db.UserLogs.Add(log);
            _db.SaveChanges();
        }
    }
}