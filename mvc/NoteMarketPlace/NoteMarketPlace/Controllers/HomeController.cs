using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NoteMarketPlace.Models;


namespace NoteMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult ContactUs()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(string Fullname, string Email, string subject, string comment)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(Email, Fullname);
                    var receiverEmail = new MailAddress("******@gmail.com", "Receiver");
                    var password = "*****";
                    var sub = subject;
                    var body = Fullname;
                    var cmnt = comment;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body + cmnt 
                       
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }


        public ActionResult BuyerReq()
        {
            NotesMarketdbEntities dc = new NotesMarketdbEntities();
            
            return View(dc.Downloads.ToList());
        }

        public ActionResult SearchYourNotes()
        {
            NotesMarketdbEntities dc = new NotesMarketdbEntities();

            return View(dc.SellerNotes.ToList());
        }
    }
}