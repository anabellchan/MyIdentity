using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MyIdentity.ViewModels;

namespace MyIdentity.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {
            // UserStore and UserManager manages data retreival.
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            IdentityUser identityUser = manager.Find(login.UserName, login.Password);

            if (ModelState.IsValid)
            {
                if (identityUser != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    var identity = new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.Name, login.UserName),
                                        },
                                        DefaultAuthenticationTypes.ApplicationCookie,
                                        ClaimTypes.Name, ClaimTypes.Role);
                    // SignIn() accepts ClaimsIdentity and issues logged in cookie. 
                    authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                    return RedirectToAction("Welcome");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUser newUser)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var identityUser = new IdentityUser()
            {
                UserName = newUser.UserName,
                Email = newUser.myEmail,
            };
            IdentityResult result = manager.Create(identityUser, newUser.Password);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Request.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(identityUser, DefaultAuthenticationTypes.ApplicationCookie);
                // todo connect to db and add newUser
                RedirectToAction("Index");
                //authenticationManager.SignIn(new AuthenticationProperties() { },
                //                             userIdentity);


            }
            return View();
        }
        [Authorize]
        public ActionResult Welcome()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }


        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(AspNetRole role)
        {
            MyIdentityEntities context = new MyIdentityEntities();
            context.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult AddUserToRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserToRole(string userName, string roleName)
        {
            MyIdentityEntities context = new MyIdentityEntities();
            AspNetUser user = context.AspNetUsers
                             .Where(u => u.UserName == userName).FirstOrDefault();
            AspNetRole role = context.AspNetRoles
                             .Where(r => r.Name == roleName).FirstOrDefault();

            user.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Admin")]
        // To allow more than one role access use syntax like the following:
        // [Authorize(Roles="Admin, Staff")]
        public ActionResult AdminOnly()
        {
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}

