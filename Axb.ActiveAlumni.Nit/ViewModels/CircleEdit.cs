using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class CircleEdit
    {
        public int Id { get; set; }
        public List<int> AcSeleUserIds { get; set; }
        public List<User> Users { get; set; }
        [Required]
        public string Name { get; set; }

        public CircleEdit()
        {
            Users = new List<User>();
            AcSeleUserIds = new List<int>();
        }
    }
}