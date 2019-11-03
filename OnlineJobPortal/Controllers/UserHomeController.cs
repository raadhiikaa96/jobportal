using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineJobPortal.DAL;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Controllers
{
    public class UserHomeController : Controller
    {
        private JobContext db = new JobContext();
        // GET: UserHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        //public ActionResult Jobs()
        //{

        //    return View();
        //    //yha data fecth to kro
        //    //neeche wal kya h fr
        //}

        
        public ActionResult Jobs()
        {
            var data = db.PostJobs.ToList();
            return View(data);
        }

        public ActionResult InterviewQuestions()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Review()
        {
            var data = db.UploadImages.ToList();
            return View(data);
        }

        public ActionResult ResumeBuilder()
        {
            ViewBag.Message = "Resume Builder page coming soon....";
            return View();
        }

        // GET: UserHome/Details/5
        public ActionResult Details(int id)
        {
            UploadImage image = new UploadImage();
            image = db.UploadImages.Where(x => x.UploadImageID==id).FirstOrDefault();
            return View(image);
            
        }

        // GET: UserHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserHome/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserHome/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
