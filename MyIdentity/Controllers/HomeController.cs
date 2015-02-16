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
using MyIdentity.Models;
using MyIdentity.ViewModels;
using MyIdentity.Models.Admin;


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
                UserName = newUser.UserName.ToLower(),
                Email = newUser.Email,
            };
            IdentityResult result = manager.Create(identityUser, newUser.Password);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Request.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(identityUser, DefaultAuthenticationTypes.ApplicationCookie);

                // connect to db and add newUser
                UserRepo user = new UserRepo();
                user.Add(newUser);

                // go back to login page
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Authorize]
        public ActionResult Welcome()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            
            return View();
        }

        [Authorize(Roles = "admin, staff")]
        public ActionResult ViewUserRoles()
        {
            UserRepo ur = new UserRepo();
            return View(ur.AllUsers());
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ModifyUserRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyUserRole(string userName, string roleName, String submit)
        {
            switch (submit)
            {
                case "Create":
                    MyIdentityEntities context = new MyIdentityEntities();
                    AspNetUser user = context.AspNetUsers
                                     .Where(u => u.UserName == userName.ToLower()).FirstOrDefault();
                    AspNetRole role = context.AspNetRoles
                                     .Where(r => r.Name == roleName.ToLower()).FirstOrDefault();

                    user.AspNetRoles.Add(role);
                    context.SaveChanges();
                    break;
                case "Delete":
                    RoleRepo roleRepo = new RoleRepo();
                    UserRole userRole = new UserRole();
                    userRole.RoleName = roleName;
                    userRole.UserName = userName;
                    roleRepo.DeleteUser(userRole);
                    break;
                default:
                    throw new Exception();
            }
            return RedirectToAction("ViewUserRoles");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(AspNetRole role)
        {
            MyIdentityEntities context = new MyIdentityEntities();
            role.Name = role.Name.ToLower();
            context.AspNetRoles.Add(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}

