using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using OnlineJobPortal.DAL;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Controllers
{
    public class UserController : Controller
    {
        private JobContext db = new JobContext();

        //Registation Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified, ActivationCode")] User user)
        {
            bool Status = false;
            string Message = "";
            //
            //Model Validation
            if(ModelState.IsValid)
            {
                #region Email already exist
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exists!");
                    return View(user);
                }
                #endregion

                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.Password = Cryptography.Hash(user.Password);
                user.ConfirmPassword = Cryptography.Hash(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;

                #region Save to Database
                db.Users.Add(user);
                db.SaveChanges();

                //Send Email to user
                SendVerificationLink(user.Email, user.ActivationCode.ToString());
                Message = "Registration successfully done. Account Activation Link" +
                    "has been sent to your Email Address: " + user.Email;
                Status = true;
                #endregion
            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            //Send Email to user
            return View(user);
        }
        //Verify Email
        //Verify Email Link
        //Login
        //Login POST
        //Logout
        
        [NonAction]
        public bool IsEmailExist(string emailid)
        {
            var v = db.Users.Where(a => a.Email == emailid).FirstOrDefault();
            return v != null;
        }

        [NonAction]
        public void SendVerificationLink(string emailid, string activationcode)
        {
            var verifyurl = "/user/VerifyAccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);

            //var fromEmail = new MailAddress("radhika1101gupta@gmail.com", "JobPaanyy");
            var fromEmail = new MailAddress("jobpaanyy@gmail.com", "JobPaanyy");
            var toEmail = new MailAddress(emailid);
            var fromEmailPassword = "JobPaanyy@admin6969";
            string subject = "Jobpaanyy Account Successfully Created!!";

            string body = "<h2>Welcome to Jobpaanyy.</h2> <br /><br /> Your account is successfully created." +
                "Please click on the below link to verify your account" +
                "<br /> <br /><a href ='"+link+"'>"+link+"</a> ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials=false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword )
            };
            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(Message);
        }












        public ActionResult Review()
        {
            return View();
        }

        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Gender,TenthPercentage,TwelfthPercentage,GraduationName,GraduationPercentage,HighestQualificationName,HQPercentage,ContactNumber,Email,Password,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Gender,TenthPercentage,TwelfthPercentage,GraduationName,GraduationPercentage,HighestQualificationName,HQPercentage,ContactNumber,Email,Password,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
