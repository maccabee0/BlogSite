using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Web.Models
{
    public class BlogViewModel
    {
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime PostDate { get; set; }
    }
}