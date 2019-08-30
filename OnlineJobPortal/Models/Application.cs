using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class Application
    {
        public string ApplicationID { get; set; }
        //public string UserID { get; set; }
        //public string UserName { get; set; }
        public string AppliedJob { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserToApplication> UserToApplications { get; set; }
    }
}