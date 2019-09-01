using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class ResetPassword
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password required!", AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "Your password doesn't match with the new password")]
        [MinLength(6, ErrorMessage = "Password should be 6 character long")]
        public string ConfirmNewPassword { get; set; }

        [Display(Name ="Reset Code")]
        [Required]
        public string ResetCode { get; set; }
    }
}