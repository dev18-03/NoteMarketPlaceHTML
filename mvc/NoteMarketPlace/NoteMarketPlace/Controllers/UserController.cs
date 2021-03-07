using NoteMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace NoteMarketPlace.Controllers
{
    public class UserController : Controller
    {
        //NotesMarketdbEntities NMPEntities = new NotesMarketdbEntities();
        // GET: User
  
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Models.User model)
        {
            using (var context = new NotesMarketdbEntities())
            {
                bool IsValid = context.Users.Any(x => x.EmailID == model.EmailID && x.Password == model.Password);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    return RedirectToAction("BuyerReq", "Home");
                }

                ModelState.AddModelError("", "INvalid email and password");
                return View();
            }
            return View();
        }

        //logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        
        }
        //Signup Action
        [HttpGet]
        public ActionResult Signup()
        {


            return View();
        }
        //Signup Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)

        
        {
            
            bool Status = false;
            string message = "";

            //MODEL vALIDATION     
            if (ModelState.IsValid)
            {
                #region //Email Already Exist or not
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion

                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                //#region Password Hasing
                //user.Password = Crypto.Hash(user.Password);
                //user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                //#endregion
                //user.IsEmailVerified = false;


                #region Save tO Database 
                using (NotesMarketdbEntities dc = new NotesMarketdbEntities())
                {
                    user.RoleID = 1;
                    dc.Users.Add(user);
                    dc.SaveChanges();


                    //save email to user
                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    message = "registrstion done Account activation link is sent "+ RedirectToAction("login","User") + user.EmailID;
                    Status = true;

                }
                #endregion
            }
            else
            {
                message = "Invalid Req";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);



    
        }

        //verify acc

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (NotesMarketdbEntities dc = new NotesMarketdbEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;

                }
                else
                {
                    ViewBag.Message = "Invalid Req";
                }
            }
            ViewBag.Status = Status;
            return View();
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (NotesMarketdbEntities dc = new NotesMarketdbEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/"+emailFor+"/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);


            var fromEmail = new MailAddress("****@gmail.com", "dev");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "*******";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                 subject = "Your account is succesfully created";

                 body = "<br/><br/> We r excited to tell you nthat ur acc is " + "Successfully created. click on bleow link" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>we got req for reset password.click on the beloow link" +
                    "<br/><br/><a href="+link+">Reset Password Link</a>";
            }
           

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,


            })
            smtp.Send(message);
        }

        //forget password 

        public ActionResult ForgetPassword()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult ForgetPassword(string EmailID)
        //{
        //    //verify email id
        //    string message = "";
        //    bool status = false;

        //    using (NotesMarketdbEntities dc = new NotesMarketdbEntities())
        //    {
        //        var account = dc.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
        //        if (account != null)
        //        {
        //            //send email for reset password
        //            string resetCode = Guid.NewGuid.ToString();
        //            SendVerificationLinkEmail(account.EmailID, resetCode, "ResetPassword");
        //            account.ResetPasswordCode = resetCode;

        //            dc.Configuration.ValidateOnSaveEnabled = false;
        //            dc.SaveChanges();


        //        }
        //        else
        //        {

        //        }
        //    }
        //    return View();
        //}

        //public ActionResult ResetPassword(string id)
        //{
        //    //verify the rest pass link
        //    using (NotesMarketdbEntities dc = new NotesMarketdbEntities())
        //    {
        //        var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
        //        if (user != null)
        //        {

        //            ResetPassword;
        //        }
        //    }


        //}

    }


       


}
    

       


    
