using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlogSite.Domain.Entities
{
    [Serializable]
    public class User
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public byte[] PasswordHash { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        public bool IsAdmin { get; set; }

        public /*virtual*/ List<Blog> Blogs { get; set; }

        public /*virtual*/ List<Comment> Comments { get; set; }

        public /*virtual*/ List<Role> Roles { get; set; } 

        public User()
        {
            JoinDate = DateTime.Now;
        }
    }
}
