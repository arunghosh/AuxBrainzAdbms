using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Connection: EntityBase
    {
        [Key]
        public int ConnectionId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        [FullNameLength]
        public string SenderName { get; set; }

        [StringLength(256)]
        public string SenderCourse { get; set; }

        [StringLength(10)]
        public string Batch { get; set; }

        [ConnectionMsgLength]
        public string Message { get; set; }

        [DataType(DataType.Date)]
        public DateTime SendOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RespondedOn { get; set; }

        public ConnectStatusType Status { get; set; }

        public override int EntityKey
        {
            get { return ConnectionId; }
        }

        public Connection()
        {
            Status = ConnectStatusType.RequestSend;
            RespondedOn = null;
        }

        [NotMapped]
        public bool IsRejected
        {
            get { return Status == ConnectStatusType.Rejected; }
        }

        [NotMapped]
        public bool IsAccepted
        {
            get { return Status == ConnectStatusType.Accepted; }
        }

        [NotMapped]
        public bool IsRequestSend
        {
            get { return Status == ConnectStatusType.RequestSend; }
        }

        public bool HasUser(int id)
        {
            return id == SenderId || id == ReceiverId;
        }

        public bool IsConnected(int id)
        {
            return IsAccepted && HasUser(id);
        }
    }



}