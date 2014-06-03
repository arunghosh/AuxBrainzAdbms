using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class JobOpening : UserOwnedEntity
    {
        [Key]
        public int JobPostId { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        [TitleLength]
        [Required]
        public string Title { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(128)]
        public string Position { get; set; }

        [StringLength(128)]
        public string Organisation { get; set; }

        [Display(Name = "Job Type")]
        public JobTypes JobType { get; set; }

        public int? ExperienceFrom { get; set; }

        public int? ExperienceTo { get; set; }

        [MessageLength]
        [Required]
        public string Description { get; set; }

        public DateTime PostedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [EmailAddress]
        [Display(Name="Send resume to")]
        [Required]
        public string SendYourResumesTo { get; set; }

        [Display(Name="Let admin send consolidated resumes")]
        public bool SendToAdmin { get; set; }

        [MessageLength]
        public string DesiredSkills { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public override int EntityKey
        {
            get { return JobPostId; }
        }

        public JobOpening()
        {
            JobType = JobTypes.FullTime;
            PostedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            IsActive = true;
        }

        public string ExperienceDisplay
        {
            get
            {
                if (ExperienceTo != null)
                {
                    return string.Format("{0} - {1} years", ExperienceFrom, ExperienceTo);
                }
                if (ExperienceFrom != null)
                {
                    return ExperienceFrom + " years";
                }
                return "--";
            }
        }

        public List<string> GetSkills()
        {
            return (DesiredSkills != null && DesiredSkills.Any()) ? DesiredSkills.Split('^').ToList() : new List<string>();
        }

        public string GetSkillsForDisplay()
        {
            return DesiredSkills.Replace("^", ", ");
        }

        public void UpdateSkills(IEnumerable<string> skills)
        {
            if (skills != null)
            {
                DesiredSkills = string.Join("^", skills);
            }
            else
            {
                DesiredSkills = string.Empty;
            }
        }

        public bool UpdateExperience(string minExp, string maxExp)
        {
            var minIndex = GetMinExperienceFill().IndexOf(minExp);
            var maxIndex = GetMaxExperienceFill().IndexOf(maxExp);
            ExperienceFrom = minIndex == 0 ? (int?)null : minIndex - 1;
            ExperienceTo = maxIndex == 0 ? (int?)null : maxIndex;
            return ExperienceFrom <= ExperienceTo || (ExperienceTo == null);
        }


        public List<string> GetMinExperienceFill()
        {
            var exp = new List<string> { "-- Minimum --", "0 year", "1 year"};
            for (int i = 2; i < 30; i++)
            {
                exp.Add(i + " years");
            }
            return exp;
        }

        public List<string> GetMaxExperienceFill()
        {
            var exp = new List<string> {"-- Maximum --", "1 year" };
            for (int i = 2; i < 30; i++)
            {
                exp.Add(i + " years");
            }
            return exp;
        }
    }
}