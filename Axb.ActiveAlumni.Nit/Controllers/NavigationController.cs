using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class NavigationController : BaseController
    {

        [AllowAnonymous]
        public PartialViewResult Top()
        {
            IEnumerable<NavigationItem> navItems = new List<NavigationItem>();
            var pages = RolePagesMap.Pages;
            navItems = navItems.Concat(pages[UserRoleType.Guest]);

            var txtItems = navItems.Where(n => string.IsNullOrEmpty(n.ImageUrl));
            var imgItems = navItems.Where(n => !string.IsNullOrEmpty(n.ImageUrl));

            var vm = new NavigationVm
            {
                TextItems = txtItems,
                ImageItems = imgItems,
                SelectePage = CurrentPage,
            };
            return PartialView("Menu", vm);
        }

        [AllowAnonymous]
        public PartialViewResult Guest()
        {
            IEnumerable<NavigationItem> navItems = new List<NavigationItem>();
            var pages = RolePagesMap.CommonPages;
            navItems = navItems.Concat(pages);
            
            var txtItems = navItems.Where(n => string.IsNullOrEmpty(n.ImageUrl));
            var imgItems = navItems.Where(n => !string.IsNullOrEmpty(n.ImageUrl));

            var vm = new NavigationVm
            {
                TextItems = txtItems,
                ImageItems = imgItems,
                SelectePage = CurrentPage,
            };
            return PartialView("Menu", vm);
        }


        [AllowAnonymous]
        public PartialViewResult Mobile()
        {
            IEnumerable<NavigationItem> navItems = new List<NavigationItem>();
            var pages = RolePagesMap.CommonPages;
            navItems = navItems.Concat(pages);

            var txtItems = navItems.Where(n => string.IsNullOrEmpty(n.ImageUrl));
            var imgItems = navItems.Where(n => !string.IsNullOrEmpty(n.ImageUrl));

            var vm = new NavigationVm
            {
                TextItems = txtItems,
                ImageItems = imgItems,
                SelectePage = CurrentPage,
            };
            return PartialView(vm);
        }

        public PartialViewResult Menu()
        {
            var user = CurrentUser;
            IEnumerable<NavigationItem> navItems = new List<NavigationItem>();
            var pages = RolePagesMap.Pages;
            navItems = navItems.Concat(RolePagesMap.CommonPages).Distinct();
            if (user.IsAlumni())
            {
                navItems = navItems.Concat(pages[UserRoleType.Alumni]);
            }
            if (user.IsStudent())
            {
                navItems = navItems.Concat(pages[UserRoleType.Student]);
            }
            if (user.IsAdmin())
            {
                navItems = navItems.Concat(pages[UserRoleType.Admin]);
            }

            var txtItems = navItems.Where(n => string.IsNullOrEmpty(n.ImageUrl));
            var imgItems = navItems.Where(n => !string.IsNullOrEmpty(n.ImageUrl));

            var vm = new NavigationVm
            {
                TextItems = txtItems,
                ImageItems = imgItems,
                SelectePage = CurrentPage,
            };
            return PartialView(vm);
        }

        public PartialViewResult SubMenu()
        {
            return PartialView();
        }

    }
}
