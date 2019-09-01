using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class UserLogin
    {
        [Key]
        public int id { get; set; }

        [Display(Name ="Email ID")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="E-Mail ID is required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Please enter 6 character long message")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}