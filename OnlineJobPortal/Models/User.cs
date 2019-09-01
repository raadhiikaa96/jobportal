using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJobPortal.Models
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        //public int ReviewID { get; set; }
        //public string ApplicationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public int TenthPercentage { get; set; }
        public int TwelfthPercentage { get; set; }
        public string GraduationName { get; set; }
        public int GraduationPercentage { get; set; }
        public string HighestQualificationName { get; set; }
        public int HQPercentage { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
        public string ResetPasswordCode { get; set; }

        public virtual ICollection<UserToApplication> UserToApplications { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}