using System.Collections.Generic;
using System.Linq;

using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;

namespace BlogSite.Services.Concrete
{
    public class SiteService : IService
    {
        public IBlogRepository BlogRepository { get; private set; }
        public IQueryable<Blog> Blogs { get { return BlogRepository.Blogs; } }

        public void SaveBlog(Blog blog)
        {
            BlogRepository.SaveBlog(blog);
        }

        public void DeleteBlog(Blog blog)
        {
            BlogRepository.DeleteBlog(blog);
        }

        public Blog GetBlog(int id)
        {
            return BlogRepository.GetBlog(id);
        }

        public ICommentRepository CommentRepository { get; private set; }
        public IQueryable<Comment> Comments { get { return CommentRepository.Comments; } }

        public void SaveComment(Comment comment)
        {
            CommentRepository.SaveComment(comment);
        }

        public void DeleteComment(Comment comment)
        {
            CommentRepository.DeleteComment(comment);
        }

        public Comment GetComment(int id)
        {
            return CommentRepository.GetComment(id);
        }

        public IQueryable<Comment> GetCommentsByBlog(int blogId)
        {
            return CommentRepository.GetCommentsByBlog(blogId);
        }

        public IUserRepository UserRepository { get; private set; }
        public IQueryable<User> Users { get { return UserRepository.Users; } }

        public void SaveUser(User user)
        {
            UserRepository.SaveUser(user);
        }

        public bool ValidateUser(string userName, string password)
        {
            return UserRepository.ValidateUser(userName, password);
        }

        public void ChangeAdminStatus(User user)
        {
            UserRepository.ChangeAdminStatus(user);
        }

        public User GetUser(int id)
        {
            return UserRepository.GetUser(id);
        }

        public User GetUser(string userName)
        {
            return UserRepository.GetUser(userName);
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
            return UserRepository.GetRole(roleName);
        }

        public Role[] GetRolesForUser(int userId)
        {
            return UserRepository.GetRolesForUser(userId);
        }

        public Role[] GetRolesForUser(string userName)
        {
            return UserRepository.GetRolesForUser(userName);
        }

        public Role[] GetRolesForUser(User user)
        {
            return user.Roles.ToArray();
        }

        public SiteService(IUserRepository user, ICommentRepository comment, IBlogRepository blog)
        {
            BlogRepository = blog;
            UserRepository = user;
            CommentRepository = comment;
        }

        public IEnumerable<Blog> GetBlogsByUser(int id)
        {
            return UserRepository.GetUser(id).Blogs;
        }

        public IEnumerable<Blog> GetBlogsByUser(string userName)
        {
            return GetBlogsByUser(UserRepository.GetUser(userName));
        }

        public IEnumerable<Blog> GetBlogsByUser(User user)
        {
            return GetBlogsByUser(user.UserId);
        }

        
    }
}
