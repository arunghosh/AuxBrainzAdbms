using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Infrastructure
{
    public static class RolePagesMap
    {
        private static List<NavigationItem> _commonPages = new List<NavigationItem>();
        public static List<NavigationItem> CommonPages
        {
            get { return _commonPages; }
            set { _commonPages = value; }
        }


        static Dictionary<UserRoleType, List<NavigationItem>> _pages = new Dictionary<UserRoleType, List<NavigationItem>>();
        public static Dictionary<UserRoleType, List<NavigationItem>> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        static RolePagesMap()
        {
            List<NavigationItem> adminPages = new List<NavigationItem>();
            List<NavigationItem> alumiPages = new List<NavigationItem>();
            List<NavigationItem> studentPages = new List<NavigationItem>();
            List<NavigationItem> guestPages = new List<NavigationItem>();
            List<NavigationItem> topPages = new List<NavigationItem>();


            var routes = Routes.NavigationItems;
            guestPages.Add(routes[PageTypes.NewUser]);
            guestPages.Add(routes[PageTypes.Login]);


            //************** Community ***************//
            var alumniCommity = routes[PageTypes.GuestHome].Clone();
            alumniCommity.DisplayText = "Alumni";
            alumniCommity.NavivationItems.Add(routes[PageTypes.GuestHome]);
            alumniCommity.NavivationItems.Add(routes[PageTypes.Find]);
            alumniCommity.NavivationItems.Add(routes[PageTypes.News]);
            alumniCommity.NavivationItems.Add(routes[PageTypes.AlumniSpeaks]);
            //alumniCommity.NavivationItems.Add(routes[PageTypes.AlumniToKnow]);
            _commonPages.Add(alumniCommity);


            //************** Community ***************//
            var asRoot = routes[PageTypes.ParentChapter];
            var association = new NavigationItem
            {
                DisplayText = "Association",
                Area = asRoot.Area,
                Action = asRoot.Action,
                Controller = asRoot.Controller,
                PageType = PageTypes.ParentChapter,
            };
            association.NavivationItems.Add(routes[PageTypes.Events]);
            association.NavivationItems.Add(routes[PageTypes.ParentChapter]);
            association.NavivationItems.Add(routes[PageTypes.Chapters]);
            //association.NavivationItems.Add(routes[PageTypes.Charter]);
            _commonPages.Add(association);


            //************** Connect ***************//
            var connectR = routes[PageTypes.MentorHome].Clone();
            connectR.DisplayText = "Alumni Connect";
            connectR.NavivationItems.Add(routes[PageTypes.MentorHome]);
            connectR.NavivationItems.Add(routes[PageTypes.JobSearch]);
            connectR.NavivationItems.Add(routes[PageTypes.MyJobPosts]);
            //connectR.NavivationItems.Add(routes[PageTypes.BackToCampus]);
            _commonPages.Add(connectR);


            //************** Association ***************//
            //_commonPages.Add(routes[PageTypes.User]);
            //_commonPages.Add(routes[PageTypes.Find]);
            //_commonPages.Add(routes[PageTypes.UserHome]);


            //*************** Network *****************//
            var ntwrk = routes[PageTypes.Circles].Clone();
            ntwrk.DisplayText = "Network";
            ntwrk.NavivationItems.Add(routes[PageTypes.Circles]);
            ntwrk.NavivationItems.Add(routes[PageTypes.Discussions]);
            ntwrk.NavivationItems.Add(routes[PageTypes.Messages]);
            _commonPages.Add(ntwrk);

            //*************** Services *****************//
            var service = routes[PageTypes.SpecialOffers].Clone();
            service.DisplayText = "Services";
            service.NavivationItems.Add(routes[PageTypes.SpecialOffers]);
            service.NavivationItems.Add(routes[PageTypes.BloodBank]);
            service.NavivationItems.Add(routes[PageTypes.ProfSearch]);
            service.NavivationItems.Add(routes[PageTypes.Advt]);
            //service.NavivationItems.Add(routes[PageTypes.SrvSearch]);
            _commonPages.Add(service);

            //*************** Contribute *****************//
            var contri = routes[PageTypes.SupportProject].Clone();
            contri.DisplayText = "Contribute";
            contri.NavivationItems.Add(routes[PageTypes.SupportProject]);
            contri.NavivationItems.Add(routes[PageTypes.Advt]);
            contri.NavivationItems.Add(routes[PageTypes.SupportAlumni]);
            //_commonPages.Add(contri);


            //*************** Job *****************//
            //var job = routes[PageTypes.MyJobPosts].Clone();
            //job.DisplayText = "Job";
            //job.NavivationItems.Add(routes[PageTypes.MyJobPosts]);
            //job.NavivationItems.Add(routes[PageTypes.JobSearch]);
            //_commonPages.Add(job);


            //****************** Admin Pages ***************************
            var userRoot = routes[PageTypes.AdminDashboard];
            var user = new NavigationItem
            {
                DisplayText = "Users-Adm",
                Area = userRoot.Area,
                Action = userRoot.Action,
                Controller = userRoot.Controller,
                PageType = PageTypes.AdminDashboard,
            };
            user.NavivationItems.Add(routes[PageTypes.AdminDashboard]);
            user.NavivationItems.Add(routes[PageTypes.ManageUsers]);
            user.NavivationItems.Add(routes[PageTypes.UserStat]);
            user.NavivationItems.Add(routes[PageTypes.MetoringPending]);
            user.NavivationItems.Add(routes[PageTypes.UserActivity]);
            user.NavivationItems.Add(routes[PageTypes.NewsLetters]);
            user.NavivationItems.Add(routes[PageTypes.AdminFind]);
            //user.NavivationItems.Add(routes[PageTypes.Feedbacks]);
            //user.NavivationItems.Add(routes[PageTypes.Logs]);
            adminPages.Add(user);


            var userHomeMgmt = routes[PageTypes.Events];
            var hmeMgmt = new NavigationItem
            {
                DisplayText = "Home Contents-Adm",
                Area = userHomeMgmt.Area,
                Action = userHomeMgmt.Action,
                Controller = userHomeMgmt.Controller,
                PageType = PageTypes.RegisterSearch,
            };
            hmeMgmt.NavivationItems.Add(routes[PageTypes.Events]);
            hmeMgmt.NavivationItems.Add(routes[PageTypes.AlumniSpeaks]);
            //hmeMgmt.NavivationItems.Add(routes[PageTypes.News]);
            hmeMgmt.NavivationItems.Add(routes[PageTypes.SpecialOffers]);
            hmeMgmt.NavivationItems.Add(routes[PageTypes.AlumniToKnow]);
            //adminPages.Add(hmeMgmt);

            //var mentorRoot = routes[PageTypes.MentorSearch];
            //var mentor = new NavigationItem
            //{
            //    DisplayText = "Mentoring",
            //    Area = mentorRoot.Area,
            //    Action = mentorRoot.Action,
            //    Controller = mentorRoot.Controller,
            //    PageType = PageTypes.MentorSearch,
            //};
            //mentor.NavivationItems.Add(routes[PageTypes.MentorSearch]);
            //mentor.NavivationItems.Add(routes[PageTypes.MetoringPending]);
            
            //adminPages.Add(mentor);

            // Student Pages

            // Staff Pages




            _pages.Add(UserRoleType.Admin, adminPages);
            _pages.Add(UserRoleType.Student, studentPages);
            _pages.Add(UserRoleType.Alumni, alumiPages);
            _pages.Add(UserRoleType.Guest, guestPages);
        }
    }
}