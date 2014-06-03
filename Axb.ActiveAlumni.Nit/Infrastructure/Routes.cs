using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Infrastructure
{
    public static class Routes
    {
        public const string RootwithP = "http://www.nitcalumni.com/";
        public const string Root = "www.nitcalumni.com/";

        public static string GetSlug(string str)
        {
            var subStr = str.Length > 30
                        ? str.Substring(0, 30)
                        : str;
            return subStr.Replace(' ', '-').Replace(',', '-').Replace('/', '-').Replace('\\', '-').Replace('&','-');

        }
        public static string NewsImg(int eventId)
        {
            return RootwithP + "Admin/AlumniNews/NewsImage/" + eventId;
        }
        public static string EventsUrl(int eventId)
        {
            return Root + PageTypes.Events.ToString() + "/" + eventId;
        }

        public static string ImageUrl(string name)
        {
            return RootwithP +  "Content/Images/" + name;
        }

        public static string DisucssionUrl(int id)
        {
            return Root + PageTypes.Discussions.ToString() + "/" + id;
        }

        public static string MessageUrl(int id)
        {
            return Root + PageTypes.Messages.ToString() + "/" + id;
        }

        public static string NewsUrl(int id)
        {
            return Root + PageTypes.News.ToString() + "/" + id;
        }

        public static string AlumniSpeakUrl(int id)
        {
            return Root + PageTypes.AlumniSpeaks.ToString() + "/" + id;
        }

        public static string MentorUrl(int id)
        {
            return Root + PageTypes.Mentors.ToString() + "/" + id;
        }

        public static string Layout
        {
            get
            {
                return System.Web.HttpContext.Current.Request.IsAuthenticated
                        ? "~/Views/Shared/_UserLayout.cshtml"
                        : "~/Views/Shared/_Layout.cshtml";
            }
        }


        public static string CityFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\cities.txt");
                return path;
            }
        }

        public static string CompanyFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\companies.txt");
                return path;
            }
        }

        public static string JobPosFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\jobpos.txt");
                return path;
            }
        }

        public static string CourseListFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\courses.txt");
                return path;
            }
        }

        public static string BranchListFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\branches.txt");
                return path;
            }
        }

        public static string WelcomeFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\welcome.txt");
                return path;
            }
        }

        public const string AcSrvCtgrys = "GetSrvCtgrys";
        public const string AcSrvNames = "GetSrvNames";
        public const string AcCourses = "GetCourses";
        public const string AcDegree = "GetDegree";
        public const string AcJobDomains = "GetJobDomains";
        public const string AcJobPositions = "GetJobPositions";
        public const string AcJoinedSkills = "GetJoinedSkills";
        public const string AcJobSkills = "GetJobSkills";
        public const string AcProfileSkills = "GetProfileSkills";
        public const string AcJobPostOrgs = "GetJobPostOrgs";
        public const string AcCompanyNames = "GetCompanyNames";
        public const string AcAutoUserNames = "AutoUserNames";
        public const string AcJobPostLocations = "GetJobLocations";
        public const string AcCities = "GetCities";
        public const string ControllerIndex = "Index";
        public const string EditCircle = "/Circle/Edit/";
        public const string CloseNote = "CloseNote";
        public const string TempDataMsgView = "TempDataMsg";
        public const string PswdRulesView = "PasswordRules";
        public const string ViewDataMsgView = "ViewDataMsg";
        public const string UnAuthView = @"UnAuth";
        public const string UserSelector = @"UserSelector";
        public const string Login = @"~/Authentication/Login";
        public const string RootHome = @"~/";
        const string _profileUrl = @"/Home/ProfileImage/";
        const string _profile = @"/user/";
        const string _searchPicUrl = @"/Home/SearchImage/";
        const string _img35Url = @"/Home/SmallImage/";
        const string _img40Url = @"/Home/Image40/";
        const string _imgTinyUrl = @"/Home/ImageTiny/";

        public static string SearchPic(int id)
        {
            return _searchPicUrl + id;
        }
        public static string ProfilePic(int id)
        {
            return _profileUrl + id;
        }

        public static string Img35Pic(int id)
        {
            return _img35Url + id;
        }

        public static string Img40Pic(int id)
        {
            return _img40Url + id;
        }

        public static string ImgTinyPic(int id)
        {
            return _imgTinyUrl + id;
        }

        public static string Profile(int id)
        {
            return _profile + id;
        }

        //public const string AlumniHome = @"Alumni";

        public static string GetAbsRoute(string route)
        {
            return @"~/" + route;
        }

        public static Dictionary<PageTypes, NavigationItem> NavigationItems { get; set; }

        public static string GetTitile(PageTypes pageType)
        {
            var page = NavigationItems[pageType];
            var title = page.Titile;
            var displayTxt = page.DisplayText;
            return (string.IsNullOrEmpty(title) ? displayTxt : title) + " | NIT Calicut Alumni";
        }

        static Routes()
        {
            NavigationItems = new Dictionary<PageTypes, NavigationItem>();

            NavigationItems.Add(PageTypes.Login, new NavigationItem
            {
                DisplayText = "Login",
                Controller = "Authentication",
                Action = "Login",
                Area = "",
                PageType = PageTypes.Login,
                Titile = "User Login"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Alumni Directory",
                Controller = "Search",
                Action = "Index",
                PageType = PageTypes.Find,
                Titile = "Search"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Professional Search",
                Controller = "Search",
                Action = "Profession",
                PageType = PageTypes.ProfSearch,
                Titile = "Search"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Service Search",
                Controller = "Search",
                Action = "Service",
                PageType = PageTypes.SrvSearch,
                Titile = "Search"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Events",
                Controller = "Event",
                Action = "Index",
                PageType = PageTypes.Events,
                Titile = "Events"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Chapters",
                Controller = "AlumniOrg",
                Action = "Chapters",
                PageType = PageTypes.Chapters,
                Titile = "Alumni Chapters"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Parent Chapter",
                Controller = "AlumniOrg",
                Action = "Parent",
                PageType = PageTypes.ParentChapter,
                Titile = "Chapters"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Alumni Charter",
                Controller = "AlumniOrg",
                Action = "Charter",
                PageType = PageTypes.Charter,
                Titile = "Alumni Charter"
            });

            //AddCommonRoute(new NavigationItem
            //{
            //    DisplayText = "Events",
            //    Controller = "Event",
            //    Action = "Index",
            //    PageType = PageTypes.Events,
            //    Titile = "Events"
            //});

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Circles",
                Controller = "Circle",
                Action = "Index",
                PageType = PageTypes.Circles,
                Titile = "Friends Circle"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Register",
                Controller = "Register",
                Action = "Index",
                PageType = PageTypes.NewUser
            });

            //AddCommonRoute(new NavigationItem
            //{
            //    DisplayText = "Home",
            //    Controller = "Home",
            //    Action = "Home",
            //    IconName = "icon-home",
            //    ImageUrl = "/Content/images/home.png",
            //    PageType = PageTypes.UserHome
            //});

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Home",
                Controller = "Home",
                Action = "Index",
                PageType = PageTypes.GuestHome
            });

            NavigationItems.Add(PageTypes.User, new NavigationItem
            {
                DisplayText = "Profile",
                Controller = "FullProfile",
                Action = "Index",
                Area = "Profile",
                IconName = "icon-user",
                ImageUrl = "/Content/images/profile.png",
                PageType = PageTypes.User

            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Messages",
                Controller = "Message",
                Action = "Index",
                PageType = PageTypes.Messages,
                Titile = "Messages"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Discussion",
                Controller = "Discussion",
                Action = "Index",
                PageType = PageTypes.Discussions
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Post Job",
                Controller = "JobPost",
                Action = "MyPosts",
                Titile= "Job Posts",
                PageType = PageTypes.MyJobPosts
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Search Jobs",
                Controller = "JobPost",
                Action = "Search",
                PageType = PageTypes.JobSearch
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Blood Bank",
                Controller = "BloodSearch",
                Action = "Index",
                PageType = PageTypes.BloodBank
            });

            #region mentoring

            // Mentoring
            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Mentor Requests",
                Controller = "Mentor",
                Action = "PendingRequests",
                PageType = PageTypes.MetoringPending
            });

            // mentoring
            AddCommonRoute(new NavigationItem
            {
                DisplayText = "All Activities",
                Controller = "Mentor",
                Action = "MentorSearch",
                PageType = PageTypes.MentorSearch
            });

            // mentoring
            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Mentoring Activities",
                Controller = "Mentor",
                Action = "Index",
                PageType = PageTypes.Mentors
            });

            // mentoring
            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Mentor a Student",
                Controller = "Mentor",
                Action = "Home",
                PageType = PageTypes.MentorHome
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Back To Campus",
                Controller = "BackCampus",
                Action = "Index",
                Titile = "Back To Campus",
                PageType = PageTypes.BackToCampus
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Admin Search",
                Controller = "Search",
                Action = "AdminSearch",
                PageType = PageTypes.AdminFind
            });

            #endregion

            //AddAlumniRoute(new NavigationItem
            //{
            //    DisplayText = "Connect",
            //    Controller = "AlumniHome",
            //    Action = "Index",
            //    PageType = PageTypes.AlumniConnect
            //});
            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Newsletter",
                Controller = "NewsLetter",
                Action = "Index",
                PageType = PageTypes.NewsLetters
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "User Registration",
                Controller = "UserStatistics",
                Action = "UserRegistration",
                PageType = PageTypes.UserRegStat
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "RECAA 89",
                Controller = "SpecialForm",
                Action = "Index",
                PageType = PageTypes.RECAA89
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "User Statistics",
                Controller = "UserStatistics",
                Action = "Index",
                PageType = PageTypes.UserStat
            });


            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Logs",
                Controller = "UserTracking",
                Action = "Logs",
                PageType = PageTypes.Logs
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "News",
                Controller = "AlumniNews",
                Action = "Index",
                PageType = PageTypes.News
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "AlumniStory",
                Controller = "AlumniNews",
                Action = "BlogIndex",
                PageType = PageTypes.AlumniStory
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Feedbacks",
                Controller = "Feedback",
                Action = "Index",
                PageType = PageTypes.Feedbacks
            });

            //AddAdminRoute(new NavigationItem
            //{
            //    DisplayText = "News",
            //    Controller = "AlumniNews",
            //    Action = "AdminIndex",
            //    PageType = PageTypes.AdminNews
            //});

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Special Offers",
                Controller = "SpecialOffer",
                Action = "Index",
                PageType = PageTypes.SpecialOffers
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Manage User",
                Controller = "ManageUser",
                Action = "Index",
                PageType = PageTypes.ManageUsers
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Alumni Speak",
                Controller = "AlumniSpeak",
                Action = "Index",
                PageType = PageTypes.AlumniSpeaks
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Alumni To Know",
                Controller = "AlumniKnow",
                Action = "Index",
                PageType = PageTypes.AlumniToKnow
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Dashboard",
                Controller = "Dashboard",
                Action = "Index",
                PageType = PageTypes.AdminDashboard
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Join Requests",
                Controller = "JoinRequest",
                Action = "Search",
                PageType = PageTypes.RegisterSearch
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Active Session",
                Controller = "UserTracking",
                Action = "Activity",
                PageType = PageTypes.UserActivity
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Failed Logins",
                Controller = "UserTracking",
                Action = "FailedLogins",
                PageType = PageTypes.FailedLogins
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Support Projects",
                Controller = "CrowdFunding",
                Action = "Index",
                PageType = PageTypes.SupportProject
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Advertise on Site",
                Controller = "Advertise",
                Action = "Index",
                PageType = PageTypes.Advt
            });

            AddAdminRoute(new NavigationItem
            {
                DisplayText = "Support Alumni",
                Controller = "FundAlumni",
                Action = "Index",
                PageType = PageTypes.SupportAlumni
            });

        }

        private static void AddAlumniRoute(NavigationItem navItem)
        {
            navItem.Area = "Alumni";
            NavigationItems.Add(navItem.PageType, navItem);
        }

        private static void AddStudentRoute(NavigationItem navItem)
        {
            navItem.Area = "Student";
            NavigationItems.Add(navItem.PageType, navItem);
        }

        private static void AddAdminRoute(NavigationItem navItem)
        {
            navItem.Area = "Admin";
            NavigationItems.Add(navItem.PageType, navItem);
        }

        private static void AddCommonRoute(NavigationItem navItem)
        {
            navItem.Area = "";
            NavigationItems.Add(navItem.PageType, navItem);
        }
    }
}