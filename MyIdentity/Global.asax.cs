using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyIdentity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
        }
        //void Application_PostAuthenticateRequest()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var name = User.Identity.Name; // Get current user name.

        //        MyIdentityEntities context = new MyIdentityEntities();
        //        var user = context.AspNetUsers.Where(u => u.UserName == name).FirstOrDefault();
        //        IQueryable<string> roleQuery = from u in context.AspNetUsers
        //                                       from r in u.AspNetRoles
        //                                       where u.UserName == Context.User.Identity.Name
        //                                       select r.Name;
        //        string[] roles = roleQuery.ToArray();

        //        HttpContext.Current.User = Thread.CurrentPrincipal =
        //                                   new GenericPrincipal(User.Identity, roles);
        //    }
        //}

    }
}
