using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.DAL
{
    public class JobContext : DbContext
    {
        public JobContext() : base("JobContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserToApplication> UserToApplications { get; set; }
        public DbSet<PostJob> PostJobs { get; set; }
        public DbSet<UploadImage> UploadImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<OnlineJobPortal.Models.UserLogin> UserLogins { get; set; }

        public System.Data.Entity.DbSet<OnlineJobPortal.Models.ResetPassword> ResetPasswords { get; set; }

        //public System.Data.Entity.DbSet<OnlineJobPortal.Models.PostJob> PostJobs { get; set; }
    }
}