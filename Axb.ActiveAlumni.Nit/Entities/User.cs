using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Axb.ActiveAlumni.Nit.Entities
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }

        #region General Information

        [StringLength(256)]
        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        [StringLength(64)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        [StringLength(128)]
        public string FullName
        {
            get
            {
                return (FirstName + " " + LastName).Trim();
            }
        }

        public GenderType Sex { get; set; }

        public RelationStatusType MaritialStatus { get; set; }

        [StringLength(8)]
        public string BloodGroup { get; set; }

        public bool CanDonateBlood { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public virtual string Skills { get; set; }

        #endregion

        #region Office Purpose

        public bool IsTouchPoint { get; set; }

        public bool HasSeenTerms { get; set; }

        public int DigestDaysSpan { get; set; }

        public UserCreateTypes CreateType { get; set; }

        public virtual List<UserLog> UserLogs { get; set; }
        public virtual List<Session> UserSessions { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }

        [NotMapped]
        public string RoleStr 
        {
            get
            {
                var role = string.Join(" | ", UserRoles.Select(u => u.RoleType.ToString()).Distinct());
                return role;
            }
        }

        [StringLength(256)]
        public string HashedPassword { get; set; }

        [StringLength(64)]
        public string EmailConfirmationToken { get; set; }

        [StringLength(10)]
        public string MobileConfirmationToken { get; set; } 

        [StringLength(64)]
        public string PasswordResetToken { get; set; }

        public DateTime? PasswordExpiryTime { get; set; }

        public UserRegisterStatus AccountStatus { get; set; }

        [StringLength(32)]
        public string RoleString { get; set; }

        #endregion

        public User()
        {
            AccountStatus = UserRegisterStatus.Pending;
            Skills = string.Empty;
            TsEventView = TsMailDigest = TsMentorView = TsDiscussionView = TsMessageView = DateTime.UtcNow;
            JoinedOn = DateTime.UtcNow;
            PasswordChangedOn = DateTime.UtcNow;
            EmailConfirmedOn = null;
            DigestDaysSpan = 7;
            CreateType = UserCreateTypes.User;
        }

        #region Contact Information

        [StringLength(32)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        public byte MobileVisibility { get; set; }
        public byte HomePhoneVisibility { get; set; }
        public byte EmailVisibility { get; set; }


        public bool NotifyViaEmail { get; set; }

        public bool NotifyViaMobile { get; set; }

        [StringLength(32)]
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Secondry Email Address")]
        [MaxLength(64)]
        [EmailAddress]
        public string OptionalEmail { get; set; }

        [MaxLength(128)]
        public string Website { get; set; }
        
        [MaxLength(128)]
        public string Linkdin { get; set; }

        [MaxLength(128)]
        public string Facebook { get; set; }

        [MaxLength(128)]
        public string CurrentCity { get; set; }

        [MaxLength(128)]
        public string CurrentCountry { get; set; }

        // TODO remove address
        public virtual Address PermanentAddress { get; set; }
        public virtual Address CurrentAddress { get; set; }

        #endregion

        #region Interests

        public bool MentoringInteset { get; set; }
        public bool StartupInterest { get; set; }
        public bool LectureInterest { get; set; }
        public bool PlacementInterest { get; set; }
        public bool VolunteerInterest { get; set; }

        #endregion

        #region Alumni Specific Information

        public virtual List<JobOpening> JobOpenings { get; set; }
        public virtual List<Job> Jobs { get; set; }
        public virtual List<Education> Educations { get; set; }
        public virtual List<Relative> Relatives { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<ServiceInfo> ServiceInfos { get; set; }
        public virtual List<Circle> Circles { get; set; }

        #endregion

        #region Time Stamps

        public DateTime TsMailDigest { get; set; }
        public DateTime TsMessageView { get; set; }
        public DateTime TsEventView { get; set; }
        public DateTime TsDiscussionView { get; set; }
        public DateTime TsMentorView { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? EmailConfirmedOn { get; set; }
        public DateTime? MobileConfirmedOn { get; set; }
        public DateTime? PasswordChangedOn { get; set; }

        #endregion

        #region Methods

        public byte GetVisibilityCode()
        {
            byte code = 0;
            foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
            {
                var roles = UserRoles.Select(u => u.RoleType.ToString());
                if (roles.Contains(item.ToString()))
                {
                    code |= (byte)(0x01 << (byte)item);
                }
            }
            return code;
        }

        public List<string> GetSkills()
        {
            return (Skills != null && Skills.Any()) ? Skills.Trim().Split('^', ',').ToList() : new List<string>();
        }

        public void UpdateSkills(IEnumerable<string> skills)
        {
            if (skills != null)
            {
                Skills = string.Join("^", skills);
            }
            else
            {
                Skills = string.Empty;
            }
        }

        public bool IsInRole(UserRoleType role)
        {
            return UserRoles.Any(r => r.RoleType == role);
        }

        public bool IsAlumni()
        {
            return IsInRole(UserRoleType.Alumni);
        }

        public bool IsAdmin()
        {
            return IsInRole(UserRoleType.Admin);
        }

        public bool IsRelative()
        {
            return IsInRole(UserRoleType.Relative);
        }

        public bool IsTest()
        {
            return IsInRole(UserRoleType.Test);
        }

        public bool IsStudent()
        {
            return IsInRole(UserRoleType.Student);
        }

        public bool IsStaff()
        {
            return IsInRole(UserRoleType.Staff);
        }

        #endregion
    }
}
