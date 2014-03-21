using System.Linq;

using BlogSite.Domain.Entities;

namespace BlogSite.Services.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        void SaveUser(User user);

        bool ValidateUser(string userName, string password);

        void ChangeAdminStatus(User user);

         User GetUser(int id);

        User GetUser(string userName);

        bool IsAdmin(User user);

        bool IsAdmin(int id);

        Role GetRole(string roleName);

        Role[] GetRolesForUser(int userId);

        Role[] GetRolesForUser(string userName);

        Role[] GetRolesForUser(User user);
    }
}
