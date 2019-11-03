using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJobPortal.Models
{
    public class UploadImage
    {
        public int UploadImageID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile {get; set;}
    }
}