using System;

namespace BlogSite.Web.Models
{
    public class PagingInfo 
    {
        public int TotalBlogs { get; set; }
        public int BlogsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalBlogs / BlogsPerPage); }
        }
    }
}