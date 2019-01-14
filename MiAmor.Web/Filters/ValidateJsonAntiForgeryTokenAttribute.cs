using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Helpers;

namespace MiAmor.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];

            var token = httpContext.Request.Headers["__RequestVerificationToken"]; // Json request has the token in Headers.
            if (token == null)
            {
                token = httpContext.Request["__RequestVerificationToken"]; // Fallback.
            }
            string strCookie = cookie.Value;
            string strToken = token.ToString();
            //AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]); // original
            AntiForgery.Validate(cookie.Value, token);
        }
    }

}