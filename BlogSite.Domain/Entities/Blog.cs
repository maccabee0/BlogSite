using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlogSite.Domain.Entities
{
    [Serializable]
    public class Blog
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int BlogId { get; set; }
        
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

        public List<Comment> Comments { get; set; }
        
        public User User { get; set; }

        //public virtual List<Tag> Tags { get; set; } 

        public Blog()
        {
            PostDate = DateTime.Now;
        }
    }
}
