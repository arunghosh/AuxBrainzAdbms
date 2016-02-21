using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class NewDiscussionInfo
    {
        public List<User> Users { get; set; }

        public int DiscussionId { get; set; }

        public IEnumerable<string> VisibilityOptions { get; set; }

        public List<string> Visibilities { get; set; }

        public List<int> AcSeleUserIds { get; set; }
        public List<string> AcCourses { get; set; }

        public List<string> TagOptions { get; set; }
        public List<string> SelectedTags { get; set; }

        public string TagCsv { get; set; }

        [Required(ErrorMessage = "Title cannot be empty")]
        [TitleLengthAttribute]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message cannot be empty")]
        [MessageLength]
        public string Message { get; set; }

        public NewDiscussionInfo()
        {
            VisibilityOptions = Enum.GetNames(typeof(DiscussCrowdType)).Cast<string>();
            Visibilities = new List<string>();
            AcCourses = new List<string>();
            SelectedTags = new List<string>();
            AcSeleUserIds = new List<int>();
            TagOptions = new List<string> { "Job", "Alumni Meet", "Entrepreneur" };
        }
        

        public byte GetVisibilityCode()
        {
            byte code = 0;
            if (Visibilities == null) return code;
            foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
            {
                if (Visibilities.Contains(item.ToString()))
                {
                    code |= (byte)(0x01 << (byte)item);
                }
            }
            return code;
        }
    }
}