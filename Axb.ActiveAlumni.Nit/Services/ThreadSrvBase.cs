using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public abstract class ThreadSrvBase: ServiceBase
    {

        public NotifyPopupData GetNotifyModel()
        {
            var time = LastViewd;
            var items = ComposeDispItems(MyItems);
            var model = new NotifyPopupData(ControllerName)
            {
                UnreadItems = items.Where(i => i.Date >= time && !i.IsReply),
                ReadItems = items.Where(i => i.Date < time || i.IsReply).Take(6),
                IsCreate = NotifyCreateNew
            };
            return model;
        }

        public virtual List<NotifyDispItem> ComposeDispItems(IEnumerable<ThreadBase> msgThreads)
        {
            var userId = UserSession.CurrentUserId;
            var dispItems = new List<NotifyDispItem>();
            foreach (var item in msgThreads)
            {
                var lastMsg = item.ThreadItems.Last();
                var dispItem = new NotifyDispItem
                {
                    ItemId = item.EntityKey,
                    Title = item.ThreadTitle,
                    Date = lastMsg.Date,
                    Comment = lastMsg.Text,
                    UserId = lastMsg.SenderId,
                    UserName = lastMsg.SenderName,
                };
                if (lastMsg.SenderId == userId && item.EntityUserMaps.Any(mp => mp.UserId != userId))
                {
                    var map = item.EntityUserMaps.First(mp => mp.UserId != userId);
                    dispItem.UserId = map.UserId;
                    dispItem.UserName = map.UserName;
                    dispItem.IsReply = true;
                }
                dispItems.Add(dispItem);
            }
            return dispItems;
        }

        public abstract IEnumerable<ThreadBase> MyItems
        {
            get;
        }

        public abstract string ControllerName
        {
            get;
        }


        public abstract bool NotifyCreateNew
        {
            get;
        }

        public abstract DateTime  LastViewd
        {
            get;
        }

        public int NewItemsCount
        {
            get
            {

                return GetNewItems(MyItems).Count;
            }
        }

        protected virtual List<ThreadBase> GetNewItems(IEnumerable<ThreadBase> threads)
        {
            var userId = UserSession.CurrentUserId;
            var newMsgs = new List<ThreadBase>();
            foreach (var thread in threads)
            {
                var lastMsg = thread.ThreadItems.Last();
                if (lastMsg.Date >= LastViewd && lastMsg.SenderId != userId)
                {
                    newMsgs.Add(thread);
                }
            }
            return newMsgs;
        }

    }
}