using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netzalist.LeadManager.Web
{
    public class LeadManagerAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Check cookie
            if (!base.AuthorizeCore(httpContext)) return false;

            // Now check the session:
            return httpContext.Session["Session"] != null;
        }
    }
}