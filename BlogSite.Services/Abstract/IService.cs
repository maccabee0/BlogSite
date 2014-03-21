using System.Collections.Generic;
using System.Linq;

using BlogSite.Domain.Entities;

namespace BlogSite.Services.Abstract
{
    public interface IService 
    {
        IBlogRepository BlogRepository { get; }

        IQueryable<Blog> Blogs { get; }

        void SaveBlog(Blog blog);

        void DeleteBlog(Blog blog);

        Blog GetBlog(int id);

        ICommentRepository CommentRepository { get; }
        
        IQueryable<Comment> Comments { get; }

        void SaveComment(Comment comment);

        void DeleteComment(Comment comment);

        Comment GetComment(int id);

        IQueryable<Comment> GetCommentsByBlog(int blogId);

        IUserRepository UserRepository { get; }

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

        IEnumerable<Blog> GetBlogsByUser(int id);

        IEnumerable<Blog> GetBlogsByUser(string userName);

        IEnumerable<Blog> GetBlogsByUser(User user);
    }
}
