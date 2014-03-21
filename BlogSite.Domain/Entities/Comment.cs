using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlogSite.Domain.Entities
{
    [Serializable]
    public class Comment
    {
        [HiddenInput(DisplayValue = false)]
        public int CommentId { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int BlogId { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

        public string Text { get; set; }

        public /*virtual*/ Blog Blog { set; get; }

        public /*virtual*/ User User { get; set; }

        public Comment()
        {
            PostDate = DateTime.Now;
        }
    }
}
