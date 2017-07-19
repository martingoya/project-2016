using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Mail;
using Model.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AutoMapper;
using System.Net;

namespace OhlalaWebWithAuth.Controllers
{
    public class HomeController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();

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
                    MailMessage mail = new MailMessage();

                    //set the addresses 
                    mail.From = new MailAddress("postmaster@ohlalaph.com.ar"); //IMPORTANT: This must be same as your smtp authentication address.
                    mail.To.Add("silvana_cq@hotmail.com;martin.goya@hotmail.com");

                    //set the content 
                    mail.Subject = model.FromName;
                    var body = "<p>Email From: {0} ({1}, {2})</p><p>Message:</p><p>{3}</p>";
                    mail.Body = string.Format(body, model.FromName, model.FromEmail, model.Phone, model.Message);
                    //send the message 
                    SmtpClient smtp = new SmtpClient("mail.ohlalaph.com.ar");

                    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    NetworkCredential Credentials = new NetworkCredential("postmaster@ohlalaph.com.ar", "lhl247MAIL!");
                    smtp.Credentials = Credentials;
                    smtp.Send(mail);
                    //var body = "<p>Email From: {0} ({1}, {2})</p><p>Message:</p><p>{3}</p>";
                    //var message = new MailMessage();
                    //message.To.Add("postmaster@ohlalaph.com");  // replace with valid value 
                    //message.From = new MailAddress("tincho.592@gmail.com", "Admin");  // replace with valid value
                    //message.SubjectEncoding = System.Text.Encoding.UTF8;
                    //message.Body = string.Format(body, model.FromName, model.FromEmail, model.Phone, model.Message);
                    //message.BodyEncoding = System.Text.Encoding.UTF8;
                    //message.IsBodyHtml = true;

                    //SmtpClient client = new SmtpClient();
                    //client.Credentials = new System.Net.NetworkCredential("tincho.592@gmail.com", "10110010110010011");
                    //client.Port = 25;
                    //client.Host = "smtp.gmail.com";
                    //client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail
                    //await client.SendMailAsync(message);
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

        public ActionResult Quince(string identifier = "0")
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == Constants.Events.Quince.ToString())
                        .OrderByDescending(z => z.Date).ToList();

            var eventsPaginated = setPagination(events, identifier);
            if (eventsPaginated != null)
            {
                return View(eventsPaginated.ToList());
            }
            else
            {
                var eventSelected = searchEvent(identifier);
                if (eventSelected != null)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Event, EventView>();
                    });
                    var eventSelectedVM = Mapper.Map<Event, EventView>(eventSelected);
                    setNavigationToOtherEvents(events, eventSelected, eventSelectedVM);
                    return View("~/Views/Home/Gallery.cshtml", eventSelectedVM);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        public ActionResult Bodas(string identifier = "0")
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == Constants.Events.Bodas.ToString())
                        .OrderByDescending(z => z.Date).ToList();

            var eventsPaginated = setPagination(events, identifier);
            if (eventsPaginated != null)
            {
                return View(eventsPaginated.ToList());
            }
            else
            {
                var eventSelected = searchEvent(identifier);
                if (eventSelected != null)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Event, EventView>();
                    });
                    var eventSelectedVM = Mapper.Map<Event, EventView>(eventSelected);
                    setNavigationToOtherEvents(events, eventSelected, eventSelectedVM);
                    return View("~/Views/Home/Gallery.cshtml", eventSelectedVM);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        public ActionResult Infantiles()
        {
            return View();
        }

        public ActionResult Newborn()
        {
            return View();
        }

        public ActionResult Ultimo(string identifier = "0")
        {
            var events = db.Event
            .Include(x => x.CoverImage)
            .Include(y => y.TypeEvent)
            .OrderByDescending(z => z.Date).ToList();

            var eventsPaginated = setPagination(events, identifier);
            if (eventsPaginated != null)
            {
                return View(eventsPaginated.ToList());
            }
            else
            {
                var eventSelected = searchEvent(identifier);
                if (eventSelected != null)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Event, EventView>();
                    });
                    var eventSelectedVM = Mapper.Map<Event, EventView>(eventSelected);
                    setNavigationToOtherEvents(events, eventSelected, eventSelectedVM);
                    return View("~/Views/Home/Gallery.cshtml", eventSelectedVM);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        [Authorize]
        public ActionResult Loader()
        {
            return View();
        }

        public List<Event> setPagination(List<Event> events, string identifier)
        {
            var totalPages = Math.Ceiling((double)events.Count() / Constants.elementsForView);

            if (identifier != null)
            {
                int num;
                if (Int32.TryParse(identifier, out num))
                {
                    events = events.Skip(Constants.elementsForView * (num)).Take(Constants.elementsForView).ToList();
                    ViewBag.previousPage = (num < totalPages - 1) ? (num + 1) : (int?)null;
                    ViewBag.nextPage = (num > 0) ? (num - 1) : (int?)null;
                }
                else
                {
                    return null;
                }
            }
            Constants.setFullPathAllImagesForAllEvents(events);
            return events;
        }

        public Event searchEvent(string identifier)
        {
            var eventSelected = db.Event.FirstOrDefault(x => x.Path == identifier);
            if (eventSelected == null)
            {
                return null;
            }
            Constants.setFullPathAllImages(eventSelected);
            return eventSelected;
        }

        public void setPreviousAndNextEvent(LinkedList<Event> linkedEvents, Event eventSelected, EventView eventSelectedVM)
        {
            var node = linkedEvents.Find(eventSelected);
            eventSelectedVM.PreviousEvent = node.Next != null ? node.Next.Value.Path : null;
            eventSelectedVM.NextEvent = node.Previous != null ? node.Previous.Value.Path : null;
        }

        public void setRelativeEvents(LinkedList<Event> linkedEvents, Event eventSelected, EventView eventSelectedVM)
        {
            if (linkedEvents.Count > Constants.relativesEventsForView)
            {
                var relativeEvents = new List<Event>();
                var node = linkedEvents.First;
                for (int i = 0; i < Constants.relativesEventsForView; i++)
                {
                    while (node.Value.ID == eventSelected.ID)
                    {
                        node = node.Next;
                        if (node == null)
                        {
                            return;
                        }
                    }
                    relativeEvents.Add(node.Value);
                    node = node.Next;
                }
                Constants.setFullPathOnlyCoverImageForAllEvents(relativeEvents);
                eventSelectedVM.RelativeEvents = relativeEvents;
            }
        }

        public void setNavigationToOtherEvents(List<Event> events, Event eventSelected, EventView eventSelectedVM)
        {
            var linkedEvents = new LinkedList<Event>(events);
            setPreviousAndNextEvent(linkedEvents, eventSelected, eventSelectedVM);
            //Shuffle list
            linkedEvents = new LinkedList<Event>(events.OrderBy(a => Guid.NewGuid()).ToList());
            setRelativeEvents(linkedEvents, eventSelected, eventSelectedVM);
        }
    }
}
