using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJobPortal.Models
{
    public class PostJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string JobProfile { get; set; }
        public string PostedBy { get; set; }
        public DateTime WhenCreated { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}