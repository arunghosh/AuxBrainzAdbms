using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{

    public enum MentorSearchTypes
    {
        Status,
        Year
    }

    public abstract class MentorFilter : SearchFilterBase<MentorShip, MentorSearchTypes>
    {
        public MentorFilter(MentorSearchTypes type)
            :base(type)
        {
        }
    }

    public class MentorStatusFilter : MentorFilter
    {
        public static string GetStatusMsg(string type)
        {
            switch ((MentorStatusType)Enum.Parse(typeof(MentorStatusType), type))
            {
                case MentorStatusType.RequestSend:
                    return "Pending Admin Approval";
                case MentorStatusType.AdminApproved:
                    return "Approved by Admin";
                case MentorStatusType.AdminRejected:
                    return "Rejected by Admin";
                case MentorStatusType.AlumniApproved:
                    return "Alumni Approved";
                case MentorStatusType.AlumniRejected:
                    return "Rejected by Alumni";
                case MentorStatusType.StudentInfo:
                    return "Peding Info from Student";
                case MentorStatusType.AlumniInfo:
                    return "Peding Info from Alumnu";
                case MentorStatusType.AdminInfo:
                    return "Peding Info from Admin";
                case MentorStatusType.SuccessfullyCompleted:
                    return "Successfully Completed";
                case MentorStatusType.Terminated:
                default:
                    return type.ToString();
            }
        }

        public override void ComposeFilters(IEnumerable<MentorShip> items)
        {
            var mentor = items.Select(i => i.Status.ToString());
            ComposeFilterItems(mentor);
            foreach (var item in FilterItems)
            {
                item.DisplayText = GetStatusMsg(item.ValueText);
            }
        }

        public override IEnumerable<MentorShip> Execute(IEnumerable<MentorShip> items)
        {
            if (!_checkedItems.Any()) return items;
            var fItems = items.Where(i => _checkedItems.Contains(i.Status.ToString()));
            return fItems;
        }

        public MentorStatusFilter()
            : base(MentorSearchTypes.Status)
        {
            AutoComplete = null;
            Name = "Status";
            CkboxName = "SelectedStatus";
            IsExpanded = true;
            ShowAll = true;
        }
    }


}