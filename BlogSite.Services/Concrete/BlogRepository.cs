using System.Data;
using System.Data.Entity;
using System.Linq;

using BlogSite.Domain;
using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;

namespace BlogSite.Services.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        EfDbContext _context = new EfDbContext();

        public IQueryable<Blog> Blogs
        {
            get
            {
                return _context.Blogs.OrderByDescending(b=>b.PostDate).Include(b => b.Comments.Select(c=>c.User)).Include(b => b.User); 
            }
        }

        public void SaveBlog(Blog blog)
        {
            if (blog.BlogId == 0)
            {
                _context.Blogs.Add(blog);
            }
            else
            {
                _context.Entry(blog).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void DeleteBlog(Blog blog)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        public Blog GetBlog(int id)
        {
            return Blogs.FirstOrDefault(b => b.BlogId == id);
        }

    }
}
