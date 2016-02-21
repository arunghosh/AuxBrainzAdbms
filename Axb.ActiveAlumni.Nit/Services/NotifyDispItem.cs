using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class NotifyDispItem
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool IsReply { get; set; }

        public NotifyDispItem()
        {
            IsReply = false;
        }
    }
}