using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult Registration([Bind(Exclude = "UserID, IsEmailVerified, ActivationCode, ResetPasswordCode")] User user)
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
                //user.UserID = 999;
                
                db.Users.Add(user);
                db.SaveChanges();

                //Send Email to user
                SendVerificationLink(user.Email, user.ActivationCode.ToString());
                Message = "Registration successfully done. Account Activation Link" +
                    " has been sent to your Email Address: " + user.Email;
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
        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount (string id)
        {
            bool Status = false;
            db.Configuration.ValidateOnSaveEnabled = false; //to avoid confirm password does not match issue on save changes
            var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
            if(v != null)
            {
                v.IsEmailVerified = true;
                db.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid request!";
            }
            ViewBag.Status = Status;
            return View();
        }
        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl="")
        {
            string Message = "";
            var v = db.Users.Where(a => a.Email == login.Email).FirstOrDefault();
            if (v != null)
            {
                if (string.Compare(Cryptography.Hash(login.Password), v.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20; //525600 minutes = 1 year
                    var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        //Message = "Invalid Credentials - Email ID or Password doesn't match!";
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Message = "Invalid Credentials - Email ID or Password doesn't match!";
                }
            }
            else
            {
                Message = "Invalid Credentials - Email ID or Password doesn't match!";
            }

            ViewBag.Message = Message;
            return View();
        }
        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
        
        [NonAction]
        public bool IsEmailExist(string emailid)
        {
            var v = db.Users.Where(a => a.Email == emailid).FirstOrDefault();
            return v != null;
        }

        [NonAction]
        public void SendVerificationLink(string emailid, string activationcode, string emailFor = "VerifyAccount")
        {
            var verifyurl = "/User/" + emailFor + "/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);

            var fromEmail = new MailAddress("jobpaanyy@gmail.com", "JobPaanyy");
            var toEmail = new MailAddress(emailid);
            var fromEmailPassword = "JobPaanyy@admin6969";
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Jobpaanyy Account Successfully Created!!";
                body = "<h2>Welcome to Jobpaanyy.</h2> <br /><br /> Your account is successfully created." +
                    "Please click on the below link to verify your account" +
                    "<br /> <br /><a href ='" + link + "'>" + link + "</a>" +
                    "<br /><br /><br />Regards, <br />Jobpaanyy ";
            }
            else if(emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br /><br /> You've just send a request to recover the password of your account in <strong>Jobpaanyy.</strong><br />" +
                    "If you did not make this request, please ignore this mail; otherwise, please click on the below link to reset your password" +
                    "<br /><br /><a href="+link+">Reset Password Link</a>" +
                    "<br /><br /><br />Regards, <br />Jobpaanyy";
            }
            
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

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //Verify Email ID
            //Generate Reset Password Link
            //Send Email
            string Message = "";
            bool Status = false;

            var account = db.Users.Where(a => a.Email == Email).FirstOrDefault();
            if(account != null)
            {
                //send Email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLink(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                Message = "Reset password link has been sent to your Email Account.";
                //Status = true;
            }
            else
            {
                Message = "Account not found!";
            }
            ViewBag.Message = Message;
            //ViewBag.tatus = Status;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //verify the reset password link
            //find account associated with this link
            //redirect to update password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if(user != null)
            {
                ResetPassword model = new ResetPassword();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            var Message = "";
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = Cryptography.Hash(model.NewPassword);
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    Message = "New Password Updated Successfully!";
                }
            }
            else
            {
                Message = "Invalid Credentials";
            }
            ViewBag.Message = Message;
            return View(model);
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
