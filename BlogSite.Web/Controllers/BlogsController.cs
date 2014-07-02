using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;
using BlogSite.Web.Models;

namespace BlogSite.Web.Controllers
{
    public class BlogsController : ApiController
    {
        //private SiteService _service = new SiteService(new UserRepository(), new CommentRepository(), new BlogRepository());
        private IService _service;

        public BlogsController(IService service)
        {
            _service = service;
        }
        
        public IEnumerable<BlogViewModel> GetAllBlogs()
        {
            return _service.Blogs.Select(blog => new BlogViewModel
                {
                    ID = blog.BlogId, 
                    Subject = blog.Subject, 
                    Description = blog.Description, 
                    Author = blog.User.UserName, 
                    PostDate = blog.PostDate
                }).ToList();
        }

        public Blog GetBlog(int id)
        {
            return _service.GetBlog(id);
        }
    }
}
