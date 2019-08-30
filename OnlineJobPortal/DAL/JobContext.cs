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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}