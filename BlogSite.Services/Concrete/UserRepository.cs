using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using BlogSite.Domain;
using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;

namespace BlogSite.Services.Concrete
{
    public class UserRepository : IUserRepository
    {
        private EfDbContext _context = new EfDbContext();

        public IQueryable<User> Users
        {
            get
            {
                return _context.Users.Include(u => u.Blogs).Include(u => u.Comments).Include(u => u.Roles);
            }
        }

        public void SaveUser(User user)
        {
            if (user.UserId == 0)
            {
                user.Roles = new List<Role> { GetRole("Blogger") };
                _context.Users.Add(user);
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public IQueryable<Role> Roles
        {
            get { return _context.Roles.Include(r => r.Users); }
        }

        public bool ValidateUser(string userName, string password)
        {
            foreach (User user in Users)
            {
                if (user.UserName.ToUpper() == userName.ToUpper())
                {
                    if (PasswordHasher.CompareByteArrays(user.PasswordHash,
                                                         PasswordHasher.CreatePasswordHash(password)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ChangeAdminStatus(User user)
        {
            if (user.IsAdmin)
            {
                user.IsAdmin = false;
                user.Roles.Remove(Roles.FirstOrDefault(r => r.Name == "Admin"));
            }
            else
            {
                user.IsAdmin = true;
                user.Roles.Add(Roles.FirstOrDefault(r => r.Name == "Admin"));
            }
            SaveUser(user);
        }

        public User GetUser(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User GetUser(int id)
        {
            return Users.FirstOrDefault(u => u.UserId == id);
        }

        public bool IsAdmin(User user)
        {
            return user.IsAdmin;
        }

        public bool IsAdmin(int id)
        {
            return GetUser(id).IsAdmin;
        }

        public Role GetRole(string roleName)
        {
            return Roles.FirstOrDefault(r => r.Name == roleName);
        }

        public Role[] GetRolesForUser(int userId)
        {
            return GetUser(userId).Roles.ToArray();
        }

        public Role[] GetRolesForUser(string userName)
        {
            return GetUser(userName).Roles.ToArray();
        }

        public Role[] GetRolesForUser(User user)
        {
            return user.Roles.ToArray();
        }
    }
}
