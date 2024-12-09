using Emlaksite.Identity;
using Emlaksite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlaksite.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userstore = new UserStore<ApplicationUser>(new DataContext());
            UserManager = new UserManager<ApplicationUser>(userstore);
            var rolestore = new RoleStore<ApplicationRole>(new DataContext());
            RoleManager= new RoleManager<ApplicationRole>(rolestore);

        }
        public ActionResult Profil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            var data = new Profile()
            {
                  ID=user.Id,
                  Name=user.Name,
                  Surname=user.Surname,
                  Username=user.UserName,
                  Email=user.Email
            };
            return View(data);
        }

        [HttpPost]
        public ActionResult Profil(Profile profile)
        {
            var user = UserManager.FindById(profile.ID);
            user.Name = profile.Name;
            user.Surname = profile.Surname;
            user.Email = profile.Email;
            user.UserName = profile.Username;
            UserManager.Update(user);
            return View("Update");

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public ActionResult Register(Register register)
		{

           
                var user = new ApplicationUser();
                user.Email = register.Email;
                user.Name = register.Name;
                user.Surname = register.Surname;
                user.UserName = register.UserName;
                var result = UserManager.Create(user, register.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası..");
                }
            
			return View(register);
		}

        public ActionResult Login()
        {
            return View();

        }
		//Not Aşağıdaki kod alanları için
		//Microsoft.AspNet.Identity.Core nuget paketi indir
		//Microsoft.AspNet.Identity.Owin
		//Microsoft.AspNet.Identity.EntityFramework
		//Systemweb yazarak Microsoft.Owin.Host.SystemWeb
		[HttpPost]
		public ActionResult Login(Login login,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(login.UserName, login.Password);
                if (user != null) 
                { 
                     var authManager=HttpContext.GetOwinContext().Authentication;
                     var identitclaims = UserManager.CreateIdentity(user, "ApplicationCookie"); 
                     var authProperties = new AuthenticationProperties(); 
                     authProperties.IsPersistent = login.RememberMe;
                    authManager.SignIn(authProperties, identitclaims);
                    if (!String.IsNullOrEmpty(ReturnUrl)) 
                    {

                        return Redirect(ReturnUrl);
                    
                    }
                    return RedirectToAction("Index", "Home");                
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok...");
                }

            }
            return View(login);

        }
		
        public ActionResult Logout()
        {
			var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
			return RedirectToAction("Index", "Home");
        }

		public ActionResult Index()
        {
            return View();
        }
    }
}