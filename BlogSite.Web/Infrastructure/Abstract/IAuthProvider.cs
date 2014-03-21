using BlogSite.Web.Models;
using BlogSite.Domain.Entities;

namespace BlogSite.Web.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string userName, string password);

        void LogOff();

        bool ValidateUserName(string name);

        void SaveUser(RegisterViewModel model);

        User GetUser(string userName);
    }
}