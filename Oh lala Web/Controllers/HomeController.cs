using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Mail;
using Model.Models;
using System.Linq;
using System.Data.Entity;
using Oh_lala_Web.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oh_lala_Web.Controllers
{
    public class HomeController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();
        private int elementsForView = 5;

        public ActionResult Index()
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
        public async Task<ActionResult> Contact(EmailFormModel model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (verifyCaptcha(form["g-recaptcha-response"]))
                {
                    var body = "<p>Email From: {0} ({1}, {2})</p><p>Message:</p><p>{3}</p>";
                    var message = new MailMessage();
                    message.To.Add("postmaster@ohlalaph.com");  // replace with valid value 
                    message.From = new MailAddress("tincho.592@gmail.com", "Admin");  // replace with valid value
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
            }
            return View(model);
        }

        public bool verifyCaptcha(string response)
        {
            using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
            {
                var values = new Dictionary<string,
                    string> {
                        {
                            "secret",
                            Constants.secretKeyReCaptcha
                        },
                        {
                            "response",
                            response
                        }
                    };
                var content = new System.Net.Http.FormUrlEncodedContent(values);
                var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                var responseString = Response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    ReCaptchaResponse responseJSON = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                    return responseJSON.success;
                }
                else
                {
                    return false;
                }
            }
        }

        public class ReCaptchaResponse
        {
            public bool success
            {
                get;
                set;
            }
            public string challenge_ts
            {
                get;
                set;
            }
            public string hostname
            {
                get;
                set;
            }
            [JsonProperty(PropertyName = "error-codes")]
            public List<string> error_codes
            {
                get;
                set;
            }
        }

        public ActionResult Sent()
        {
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Fifteen(string identifier)
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == Constants.Events.Fifteen.ToString()).ToList();
            events.Take(elementsForView);

            if (identifier != null)
            {
                int num;
                if (Int32.TryParse(identifier, out num))
                {
                    events.Skip(elementsForView * (num + 1)).Take(elementsForView);
                }
                else
                {
                    var eventFifteen = db.Event.FirstOrDefault(x => x.Path == identifier);
                    Constants.setFullPathAllImages(eventFifteen);
                    return View("~/Views/Home/Gallery.cshtml", eventFifteen);
                }
            }
            Constants.setFullPathAllImagesForAllEvents(events);
            return View(events.ToList());
        }

        public ActionResult Bodas(string identifier)
        {
            var events = db.Event
            .Include(x => x.CoverImage)
            .Include(y => y.TypeEvent)
            .Where(l => l.TypeEvent.Name == Constants.Events.Bodas.ToString()).ToList();
            events.Take(elementsForView);

            if (identifier != null)
            {
                int num;
                if (Int32.TryParse(identifier, out num))
                {
                    events.Skip(elementsForView * (num + 1)).Take(elementsForView);
                }
                else
                {
                    var eventBodas = db.Event.FirstOrDefault(x => x.Path == identifier);
                    Constants.setFullPathAllImages(eventBodas);
                    return View("~/Views/Home/Gallery.cshtml", eventBodas);
                }
            }
            Constants.setFullPathAllImagesForAllEvents(events);
            return View(events.ToList());
        }

        public ActionResult Infantiles()
        {
            return View();
        }

        public ActionResult Newborn()
        {
            return View();
        }

        public ActionResult Ultimo()
        {
            var events = db.Event
            .Include(x => x.CoverImage)
            .Include(y => y.TypeEvent)
            .OrderByDescending(z => z.Date).ToList();
            events.Take(elementsForView);

            Constants.setFullPathOnlyCoverImageForAllEvents(events);
            return View(events.ToList());
        }
    }
}
