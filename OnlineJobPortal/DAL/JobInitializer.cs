using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.DAL
{
    public class JobInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<JobContext>
    {
        //protected override void Seed(JobContext context)
        //{
        //    ////base.Seed(context);
        //    //var users = new List<User>
        //    //{
        //    //    new User { UserID = "U001", FirstName = "Radhika", LastName = "Gupta", Gender = "Female", TenthPercentage = 62, TwelfthPercentage = 79, GraduationName = "BCA", GraduationPercentage = 65, HighestQualificationName = "MCA", HQPercentage = 69, Address = "40, 2nd Floor, Jagriti Enclave, Delhi", ContactNumber = "9873390708", Email = "radhika.gupta70@gmail.com", Password = "RG1101@1996" },
        //    //    new User { UserID = "U002", FirstName = "Nikhil", LastName = "Chauhan", Gender = "Male", TenthPercentage = 70, TwelfthPercentage = 80, GraduationName = "BCA", GraduationPercentage = 65, HighestQualificationName = "BCA", HQPercentage = 65, Address = "Sonipat", ContactNumber = "9876543210", Email = "nikhil.chauhan020@gmail.com", Password = "NRC1504@1999" }
        //    //    //new User { UserID = "U001", ApplicationID = "A001", ReviewID = 01, FirstName = "Radhika", LastName = "Gupta", Gender = "Female", TenthPercentage = 62, TwelfthPercentage = 79, GraduationName = "BCA", GraduationPercentage = 65, HighestQualificationName = "MCA", HQPercentage = 69, Address = "40, 2nd Floor, Jagriti Enclave, Delhi", ContactNumber = 9873390708, Email = "radhika.gupta70@gmail.com", Password = "RG1101@1996" }
        //    //};
        //    //users.ForEach(j => context.Users.Add(j));
        //    //context.SaveChanges();

        //    var applications = new List<Application>
        //    {
        //        new Application { ApplicationID = "A001", AppliedJob = "Comapny1", Status = "Pending" },
        //        new Application { ApplicationID = "A002", AppliedJob = "Comapny2", Status = "Pending" },
        //        new Application { ApplicationID = "A003", AppliedJob = "Comapny3", Status = "Pending" },
        //    };
        //    applications.ForEach(j => context.Applications.Add(j));
        //    context.SaveChanges();

        //    var reviews = new List<Review>
        //    {
        //        new Review { ReviewID = 101, UserID = "U001", CompanyName ="XYZ", Location = "Delhi", One = "one", Two = "two", Three = "three", Four = "four", Five = "five", Six = "Six", Seven = "seven", Overall = "Overall"},
        //        new Review { ReviewID = 102, UserID = "U002", CompanyName ="XYZ", Location = "Delhi", One = "one", Two = "two", Three = "three", Four = "four", Five = "five", Six = "Six", Seven = "seven", Overall = "Overall"},
        //        new Review { ReviewID = 103, UserID = "U001", CompanyName ="XYZ", Location = "Delhi", One = "one", Two = "two", Three = "three", Four = "four", Five = "five", Six = "Six", Seven = "seven", Overall = "Overall"}
        //    };
        //    reviews.ForEach(j => context.Reviews.Add(j));
        //    context.SaveChanges();

        //    var usertoapplication = new List<UserToApplication>
        //    {
        //        new UserToApplication { UserToApplicationID = 1, UserID = "U001", ApplicationID = "A001"},
        //        new UserToApplication { UserToApplicationID = 2, UserID = "U001", ApplicationID = "A002"},
        //        new UserToApplication { UserToApplicationID = 3, UserID = "U001", ApplicationID = "A003"},
        //        new UserToApplication { UserToApplicationID = 4, UserID = "U002", ApplicationID = "A003"},
        //    };
        //    usertoapplication.ForEach(j => context.UserToApplications.Add(j));
        //    context.SaveChanges();
        //}
    }
}