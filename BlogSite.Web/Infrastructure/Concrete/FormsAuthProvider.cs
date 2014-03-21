using System.Linq;
using System.Web.Security;
using System;
using System.Web;
using BlogSite.Services.Abstract;
using BlogSite.Services.Concrete;
using BlogSite.Web.Infrastructure.Abstract;
using BlogSite.Web.Models;
using BlogSite.Domain.Entities;

namespace BlogSite.Web.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        private readonly IService _service;

        public FormsAuthProvider(IService repo)
        {
            _service = repo;
        }

        public bool Authenticate(string userName, string password)
        {
            bool result = _service.ValidateUser(userName, password);
            if (result)
            {
                User user = _service.GetUser(userName);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), true, user.UserId.ToString());
                string encriptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(encriptedTicket);
                HttpContext.Current.Response.Cookies.Add(cookie);
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            return result;
        }

        public void LogOff()
        {
            FormsAuthentication.SignOut();
        }

        public bool ValidateUserName(string name)
        {
            if (name.All(i => (!char.IsLetter(i)) || (i != ' ') || (i != '-')))
            {
                return (!_service.Users.Any(u=>u.UserName == name));
            }
            return false;
        }

        private User PrepareUser(RegisterViewModel model)
        {
            return new User
                {
                    UserName = model.UserName,
                    PasswordHash = PasswordHasher.CreatePasswordHash(model.Password),
                    Email = model.Email,
                    Birthday = model.Birthday,
                };
        }

        public void SaveUser(RegisterViewModel model)
        {
            _service.SaveUser(PrepareUser(model));
        }

        public User GetUser(string userName)
        {
            return _service.GetUser(userName);
        }
    }
}