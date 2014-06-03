using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class NotifyPopupData
    {
        public string Name { get; set; }
        public IEnumerable<NotifyDispItem> UnreadItems { get; set; }
        public IEnumerable<NotifyDispItem> ReadItems { get; set; }
        public bool IsCreate { get; set; }
        public NotifyPopupData(string name)
        {
            Name = name;
            IsCreate = false;
        }
    }
}