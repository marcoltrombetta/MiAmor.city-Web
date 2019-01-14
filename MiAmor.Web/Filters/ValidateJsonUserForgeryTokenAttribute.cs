using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Helpers;
using MiAmor.Web.Helpers;
using MiAmor.Services;
using System.Web.Routing;


namespace MiAmor.Web.Filters
{    
   
    public sealed class ValidateJsonUserForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
          

            var token = httpContext.Request.Headers["Authorization"]; // Json request has the token in Headers.
            if (token == null)
            {
                token = httpContext.Request["Authorization"]; // Fallback.
            }
     
            string strToken = token.ToString();
            Authentication Authentication = new Authentication();
            int i=Authentication.IsTokenAuthenticated(strToken);
            if (i!=0)
            {
                //filterContext.Controller.ViewBag.userInfo = token;
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }
    }

}