using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Chapter : EntityBase
    {
        [Key]
        public int ChapterId { get; set; }

        [FullNameLength]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Alias { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<ChapterHead> CommitteMembers { get; set; }

        [NotMapped]
        private List<Event> _events;

        [StringLength(32)]
        public string Latitude { get; set; }

        [StringLength(32)]
        public string Longitute { get; set; }


        [StringLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [StringLength(32)]
        public string Phone { get; set; }

        [NotMapped]
        public List<Event> Events
        {
            get
            {
                if (_events == null)
                {
                    var term = Name.ToLower();
                    _events = new EventSrv().MyEvents.Where(n => n.EventName.ToLower().Contains(term) || n.Location.ToLower().Contains(term)).ToList();
                }
                return _events;
            }
        }

        [NotMapped]
        private List<User> _users = null;
        [NotMapped]
        public List<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = DbCache.UserSearch.Where(u => u.CurrentCity != null && !string.IsNullOrEmpty(u.CurrentCity) && Alias.Contains(u.CurrentCity.ToLower())).ToList();
                }
                return _users;
            }
        }

        [NotMapped]
        public List<ChapterHead> ActiveHeads
        {
            get
            {
                var active =  CommitteMembers.Where(m => !m.IsDeleted).ToList();
                return active;
            }
        }

        public Chapter()
        {
            CommitteMembers = new List<ChapterHead>();
        }


        public override int EntityKey
        {
            get { return ChapterId; }
        }
    }
}