using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobPortal.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "(0:DD-MM-YYYY)")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "10th Percentage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "10th Percentage is Required")]
        public int TenthPercentage { get; set; }

        [Display(Name = "12th Percentage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "12th Percentage is Required")]
        public int TwelfthPercentage { get; set; }

        [Display(Name = "Graduation Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Graduation Name is Required")]
        public string GraduationName { get; set; }

        [Display(Name = "Graduation Percentage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Graduation Percentage is Required")]
        public int GraduationPercentage { get; set; }

        [Display(Name = "Highest Qualification Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Highest Qualification Name is Required")]
        public string HighestQualificationName { get; set; }

        [Display(Name = "Highest Qualification Percentage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Highest Qualification Percentage is Required")]
        public int HQPercentage { get; set; }

        [Display(Name = "Contact Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Number is Required")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits contact number")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits contact number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Display(Name = "Role")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required")]
        public string Role { get; set; }
    }
}