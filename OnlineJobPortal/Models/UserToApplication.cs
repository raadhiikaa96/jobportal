using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class UserToApplication
    {
        public int UserToApplicationID { get; set; }
        public int UserID { get; set; }
        public int ApplicationID { get; set; }

        public virtual User User { get; set; }
        public virtual Application Application { get; set; }
    }
}