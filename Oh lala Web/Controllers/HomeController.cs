using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Mail;
using Model.Models;
using System.Linq;
using System.Data.Entity;
using Oh_lala_Web.Models;

namespace Oh_lala_Web.Controllers
{
    public class HomeController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();

        public ActionResult Index()
        {
            var events = db.Event.Include(x => x.Image).Include(y => y.TypeEvent).Include(z => z.Service).Include(k => k.Service.Select(j => j.TypeService1));
            return View(events.ToList());
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
                var body = "<p>Email From: {0} ({1}, {2})</p><p>Message:</p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add("postmaster@ohlalaph.com");  // replace with valid value 
                message.From = new MailAddress("tincho.592@gmail.com", "Admin");  // replace with valid value
                message.Subject = model.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Phone, model.Message);
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
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
