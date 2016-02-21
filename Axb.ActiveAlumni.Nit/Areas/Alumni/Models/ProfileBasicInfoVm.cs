//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using Axb.ActiveAlumni.Nit.Entities;

//namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Models
//{
//    public class ProfileBasicInfoVm : IProfileVm
//    {
//        [StringLength(64)]
//        [Required]
//        [Display(Name = "First Name")]
//        public string FirstName { get; set; }

//        [StringLength(64)]
//        [Required]
//        [Display(Name = "Last Name")]
//        public string LastName { get; set; }

//        [Required]
//        public string Batch { get; set; }

//        [Required]
//        public int CourseId { get; set; }

//        [Required]
//        public int BranchId { get; set; }

//        [StringLength(128)]
//        [Display(Name = "Name")]
//        public string FullName
//        {
//            get
//            {
//                return FirstName + " " + LastName;
//            }
//        }

//        [Required]
//        public GenderType Sex { get; set; }

//        [Display(Name = "Maritial Status")]
//        public RelationStatusType MaritialStatus { get; set; }

//        [DataType(DataType.Date)]
//        [Display(Name = "Date of Birth")]
//        public DateTime? DateOfBirth { get; set; }

//        public int BirthDay { get; set; }
//        public int BirthYear { get; set; }
//        public int BirthMonth { get; set; }

//        public ProfileBasicInfoVm()
//        {

//        }

//        public ProfileBasicInfoVm(User user)
//        {
//            FirstName = user.FirstName;
//            LastName = user.LastName;
//            MaritialStatus = user.MaritialStatus;
//            Sex = user.Sex;
//            DateOfBirth = user.DateOfBirth;
//            if (user.DateOfBirth != null)
//            {
//                var date = user.DateOfBirth.Value.Date;
//                BirthDay = date.Day;
//                BirthYear = date.Year;
//                BirthMonth = date.Month;
//            }
//            //BranchId = user.BranchId;
//            Batch = user.Batch;
//        }

//        public void UpdateUser(User user)
//        {
//            user.Sex = Sex;
//            user.MaritialStatus = MaritialStatus;
//            user.LastName = LastName;
//            user.FirstName = FirstName;
//            user.DateOfBirth = new DateTime(BirthYear, BirthMonth, BirthDay);
//            //user.BranchId = BranchId;
//            user.Batch = Batch;
//        }
//    }
//}