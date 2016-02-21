using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class NewMessageVm
    {
        public int ReceiverId { get; set; }
        public int ThreadId { get; set; }
        public string ReceiverName { get; set; }

        public List<int> AcSeleUserIds { get; set; }

        [Required(ErrorMessage="Message cannot be empty")]
        [MessageLength]
        public string Message { get; set; }
    }
}