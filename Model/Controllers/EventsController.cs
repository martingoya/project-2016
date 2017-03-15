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
            return View(new EventView());
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventView @eventView, FormCollection fc, string Command)
        {
            if (Request.Form["createEvent"] != null)
            {
                if (ModelState.IsValid)
                {
                    //Action
                    @eventView.Path = eventView.Title.Replace(" ", "_");

                    var typeEvent = db.TypeEvent.FirstOrDefault(x => x.ID == @eventView.TypeEventID);
                    var path = "Content/Fotos/" + typeEvent.Name + "/" + @eventView.Path + "/";
                    AutoMapper.Mapper.Initialize(x =>
                    {
                        x.CreateMap<EventView, Event>();
                    });
                    //Cover
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
                    if (eventView.Files.First() != null)
                    {
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
                            if (eventView.CoverImage.ImagePath != image.ImagePath)
                            {
                                @eventView.Images.Add(image);
                            }                        
                        }
                    }
                    //Save changes
                    db.Event.Add(AutoMapper.Mapper.Map<Event>(@eventView));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @eventView.TypeEventID);
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
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,TypeEventID,Text")] Event @event)
        {//TO DO: Fix
            if (ModelState.IsValid)
            {
                @event = db.Event.Find(@event.ID);
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
