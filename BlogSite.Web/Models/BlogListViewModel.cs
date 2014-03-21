using System.Collections.Generic;

using BlogSite.Domain.Entities;

namespace BlogSite.Web.Models
{
    public class BlogListViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}