using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string UserID { get; set; }
        //public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string One{ get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }
        public string Seven { get; set; }
        public string Overall { get; set; }

        public virtual User User { get; set; }
    }
}