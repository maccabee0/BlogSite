using System;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {1} charaters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The passwords you entered do not match")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}