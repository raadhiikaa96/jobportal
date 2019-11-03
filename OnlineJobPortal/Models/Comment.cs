using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string Comments { get; set; }
        public int UserID { get; set; }
        public int UploadImageID  { get; set; }
        public DateTime PostedAt { get; set; }
    }
}