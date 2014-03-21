using System;
using System.Web.Security;
using System.Linq;

using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;
using BlogSite.Services.Concrete;

namespace BlogSite.Web.Infrastructure.Concrete
{
    public class BlogSiteRoleProvider : RoleProvider
    {
        private SiteService _service = new SiteService(new UserRepository(), new CommentRepository(), new BlogRepository());

        public override bool IsUserInRole(string username, string roleName)
        {
            User user = _service.GetUser(username);
            return user.Roles.Any(role => role.Name == roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            Role[] roles = _service.GetRolesForUser(username);
            string[] roleNames = new string[roles.Count()];
            for (int i = 0; i < roles.Count(); i++)
            {
                roleNames[i] = roles[i].Name;
            }
            return roleNames;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}