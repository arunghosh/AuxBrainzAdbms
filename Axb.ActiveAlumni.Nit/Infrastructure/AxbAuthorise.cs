using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Axb.ActiveAlumni.Nit
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AxbAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            AuthorizeCore(filterContext.HttpContext);

            var isAuthorized = filterContext.HttpContext.Items[IS_AUTHORIZED] != null
                ? Convert.ToBoolean(filterContext.HttpContext.Items[IS_AUTHORIZED])
                : false;

            if (!isAuthorized && filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
                return;
            }
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private const string IS_AUTHORIZED = "isAuthorized";

        public string RedirectUrl = "~/error/unauthorized";

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);
            if (!httpContext.Items.Contains(IS_AUTHORIZED))
            {
                httpContext.Items.Add(IS_AUTHORIZED, isAuthorized);
            }
            httpContext.Items[IS_AUTHORIZED] = isAuthorized;
            return isAuthorized;
        }
    }
}