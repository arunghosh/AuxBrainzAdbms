using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.WebPages;

namespace Axb.ActiveAlumni.Nit
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IRequiresSessionState 
    {
        protected void Application_Start()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("Mobile")
            {
                ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf
                    ("Mobile", StringComparison.OrdinalIgnoreCase) >= 0)
            });
            AreaRegistration.RegisterAllAreas();


            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);

            //ControllerBuilder.Current.SetControllerFactory(new ActiveAlumniControlFactory());
            //if (Request.IsLocal)
            //{
            //    MiniProfiler.Start();
            //} 
        }

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        var context = System.Web.HttpContext.Current;
                        var request = System.Web.HttpContext.Current.Request;
                        HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                        if (authCookie != null)
                        {
                            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                            var roles = authTicket.UserData.Split('|');
                            var user = new GenericPrincipal(new GenericIdentity(authTicket.Name, "Forms"), roles);
                            e.User = context.User = Thread.CurrentPrincipal = user;
                            UserSession.UpdateCurrentUser("", authTicket.Name);
                        }

                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }
}