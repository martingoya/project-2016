using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Oh_lala_Web.Models;
using System;

namespace Oh_lala_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fifteen()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add("postmaster@ohlalaph.com");  // replace with valid value 
                message.From = new MailAddress("tincho.592@gmail.com", "Admin");  // replace with valid value
                message.Subject = model.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("tincho.592@gmail.com", "10110010110010011");
                client.Port = 25;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail
                await client.SendMailAsync(message);
                return RedirectToAction("Sent");
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}
