using System.Web.Mvc;

using BlogSite.Domain.Entities;
using BlogSite.Web.Infrastructure.Abstract;
using BlogSite.Web.Models;

namespace BlogSite.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider prov)
        {
            _authProvider = prov;
        }

        public ActionResult LogOff()
        {
            _authProvider.LogOff();
            return RedirectToAction("AllBlogs", "Blog");
        }

        [HttpGet]
        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                ModelState.AddModelError("", "Incorrect username or password.");
                return View();
            }
            return View();
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!_authProvider.ValidateUserName(model.UserName))
                {
                    TempData["message"] = string.Format("User {0} is not been saved", model.UserName);
                    ModelState.AddModelError("", "User Name invalid or already taken.");
                }
                else
                {
                    _authProvider.SaveUser(model);
                    _authProvider.Authenticate(model.UserName, model.Password);
                    TempData["message"] = string.Format("{0} has been saved", model.UserName);
                }
            }
            return View();
        }

        public ViewResult UserProfile(string userName)
        {
            User user = _authProvider.GetUser(userName);
            return View(user);
        }
    }
}
