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
            var events = db.Event.Include(x => x.Image).Include(y => y.TypeEvent);
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
        public ActionResult Create(EventView @eventView, FormCollection fc, HttpPostedFileBase[] files, HttpPostedFileBase coverFile)
        {
            List<ServiceView> serviceSession = new List<ServiceView>();
            if (Request.Form["addService"] != null)
            {
                if (Session["ListService"] != null)
                {
                    serviceSession = (List<ServiceView>)Session["ListService"];
                }
                if (@eventView.ServiceID != null)
                {
                    if (!serviceSession.Any(x => x.ID == @eventView.ServiceID))
                    {
                        var serviceDB = db.Service.Include(y => y.TypeServices).FirstOrDefault(x => x.ID == @eventView.ServiceID);
                        ServiceView service = new ServiceView();
                        service.ID = serviceDB.ID;
                        service.Name = serviceDB.Name;
                        service.TypeService = serviceDB.TypeServices.Name;
                        serviceSession.Add(service);
                        Session["ListService"] = serviceSession;
                    }
                }   
            }
            else
            {
                if (Request.Form["createEvent"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        var path = "~/Content/" + @eventView.Controller + "/" + @eventView.Action + "/";
                        AutoMapper.Mapper.Initialize(x =>
                        {
                            x.CreateMap<EventView, Event>();
                        });
                        //Cover or Video
                        if (@eventView.IsImage)
                        {
                            eventView.VideoLink = string.Empty;
                            if (coverFile != null && coverFile.ContentLength > 0)
                            {
                                var fullPathCover = path + coverFile.FileName;
                                var imageCoverExist = db.Image.FirstOrDefault(x => x.ImagePath == fullPathCover);
                                if (imageCoverExist != null)
                                {
                                    eventView.Image = imageCoverExist;
                                }
                                else
                                {
                                    var newImage = new Image();
                                    newImage.Title = eventView.Title;
                                    newImage.ImagePath = fullPathCover;
                                }
                            }
                        }
                        //Services
                        serviceSession = (List<ServiceView>)Session["ListService"];
                        if (serviceSession != null)
                        {
                            var serviceIDs = serviceSession.Select(k => k.ID).ToList();
                            var servicesToAdd = db.Service.Join(serviceIDs, x => x.ID, y => y, (x, y) => x);
                            foreach (var item in servicesToAdd)
                            {
                                @eventView.Service.Add(item);
                            }
                        }
                        //Images
                        if (files[0] != null) {
                            foreach (var file in files)
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
                        //Controller
                        @eventView.Controller = db.TypeEvent.First(x => x.ID == eventView.TypeEventID).Name;
                        //Action
                        @eventView.Action = eventView.Title;
                        //Save changes
                        db.Event.Add(AutoMapper.Mapper.Map<Event>(@eventView));
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
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
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @eventView.ImageID);
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
            ViewBag.ServiceID = new SelectList(services, "ServiceID", "Data", @eventView.ServiceID);
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
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.ImageID);
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
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.ImageID);
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
