using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineJobPortal.DAL;
using OnlineJobPortal.Models;
using System.IO;

namespace OnlineJobPortal.Controllers
{
    public class HomeController : Controller
    {
        private JobContext db = new JobContext();
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        [HttpGet]
        public ActionResult PostJob()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostJob(PostJob postJob)
        {
            bool Status = false;
            string Message = "";
            //
            //Model Validation
            if (ModelState.IsValid)
            {
                db.PostJobs.Add(postJob);
                db.SaveChanges();
                Message = "Registration successfully done. Account Activation Link" +
                    " has been sent to your Email Address: ";
                Status = true;
                return View(postJob);
            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(postJob);
        }

        [HttpGet]
        public ActionResult ReviewImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReviewImage(UploadImage image)
        {
            string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/images/" + filename;
            filename = Path.Combine(Server.MapPath("~/images/"), filename);
            image.ImageFile.SaveAs(filename);
            db.UploadImages.Add(image);
            db.SaveChanges();
            
            return View(image);
        }
    }
}