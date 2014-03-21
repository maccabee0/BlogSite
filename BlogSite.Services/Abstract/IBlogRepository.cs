using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlogSite.Domain.Entities;

namespace BlogSite.Services.Abstract
{
    public interface IBlogRepository
    {
        IQueryable<Blog> Blogs { get; }

        void SaveBlog(Blog blog);

        void DeleteBlog(Blog blog);

        Blog GetBlog(int id);
    }
}
