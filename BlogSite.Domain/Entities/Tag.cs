using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlogSite.Domain.Entities
{
    [Serializable]
    public class Tag
    {
        [HiddenInput(DisplayValue = false)]
        public int TagId { get; set; }

        public string TagName { get; set; }

        public virtual List<Blog> Blogs { get; set; }
    }
}
