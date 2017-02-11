using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using System.IO;

namespace Model.Controllers
{
    public class EventsController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Event.Include(x => x.CoverImage).Include(y => y.TypeEvent);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title");
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name");
            var services = db.Service
                .Include(y => y.TypeServices)
                .ToList()
                .Select(x => new
                {
                    ServiceID = x.ID,
                    Data = string.Format("({0}) - {1}", x.TypeServices.Name, x.Name)
                });
            ViewBag.ServiceID = new SelectList(services, "ServiceID", "Data");          
            return View(new EventView());
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventView @eventView, FormCollection fc, string Command)
        {
            List<ServiceView> serviceSession = new List<ServiceView>();

                if (Request.Form["createEvent"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        //Controller
                        @eventView.Controller = db.TypeEvent.First(x => x.ID == eventView.TypeEventID).Name;
                        //Action
                        @eventView.Action = eventView.Title.Replace(" ", "_");

                        var typeEvent = db.TypeEvent.FirstOrDefault(x => x.ID == @eventView.TypeEventID);
                        var path = "Content/Fotos/" + typeEvent.Name + "/" + @eventView.Action + "/";
                        AutoMapper.Mapper.Initialize(x =>
                        {
                            x.CreateMap<EventView, Event>();
                        });
                    //Cover or Video
                    if (eventView.CoverFile != null && eventView.CoverFile.ContentLength > 0)
                    {
                        var fullPathCover = path + eventView.CoverFile.FileName;
                        var imageCoverExist = db.Image.FirstOrDefault(x => x.ImagePath == fullPathCover);
                        if (imageCoverExist != null)
                        {
                            eventView.CoverImage = imageCoverExist;
                        }
                        else
                        {
                            var newImage = new Image();
                            newImage.Title = eventView.Title;
                            newImage.ImagePath = fullPathCover;
                            eventView.CoverImage = newImage;
                        }
                    }
                    //Images
                    if (eventView.Files.First() != null) {
                        foreach (var file in eventView.Files)
                        {
                            if (file.ContentLength < 0)
                                continue;
                            var fullPath = path + file.FileName;
                            var image = db.Image.FirstOrDefault(x => x.ImagePath == fullPath);
                            if (image == null)
                            {
                                image = new Image();
                                image.ImagePath = fullPath;
                                image.Title = @eventView.Title;
                            }
                            @eventView.Images.Add(image);
                        }
                    }
                    //Save changes
                    db.Event.Add(AutoMapper.Mapper.Map<Event>(@eventView));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    int indexService = -1;
                    //Boton para borrar de la lista 
                    foreach (string item in fc.AllKeys)
                    {
                        if (item.Contains("deleteService "))
                        {
                            indexService = int.Parse(item.Remove(0, 14));
                        }
                    }
                    if (indexService != -1)
                    {
                        serviceSession = (List<ServiceView>)Session["ListService"];
                        serviceSession.RemoveAt(indexService);
                        Session["ListVuelo"] = serviceSession;
                    }
                    ModelState.Clear();
                }
            }
            //ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @eventView.ImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @eventView.TypeEventID);
            var serviceSessionIDs = serviceSession.Select(k => k.ID).ToList();
            var services = db.Service
                .Where(z => !serviceSessionIDs.Contains(z.ID))
                .Include(y => y.TypeServices)
                .ToList()
                .Select(x => new
                {
                    ServiceID = x.ID,
                    Data = string.Format("({0}) - {1}", x.TypeServices.Name, x.Name)
                });
            //ViewBag.ServiceID = new SelectList(services, "ServiceID", "Data", @eventView.ServiceID);
            return View(@eventView);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.CoverImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @event.TypeEventID);
            ViewBag.Services = new SelectList(db.Service, "ID", "Name", @event.Service);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,TypeEventID,Date,Text,ImageID,VideoLink,IsImage,Controller,Action,Services")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var services = db.Service.ToList();
                @event.Service.Add(services.First());
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.CoverImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @event.TypeEventID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            //Images
            var images = new List<Image>(@event.Images);
            foreach (var image in images)
            {
                @event.Images.Remove(image);
            }
            db.SaveChanges();
            //Services
            var services = new List<Service>(@event.Service);
            foreach (var service in services)
            {
                @event.Service.Remove(service);
            }
            db.SaveChanges();
            //Delete Event
            db.Event.Remove(@event);
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
