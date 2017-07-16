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
    [Authorize]
    public class EventsController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Event.Include(x => x.CoverImage).Include(y => y.TypeEvent);
            return View("~/Views/Loader/Events/Index.cshtml", events.ToList());
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
            return View("~/Views/Loader/Events/Details.cshtml", @event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title");
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name");
            return View("~/Views/Loader/Events/Create.cshtml", new EventView());
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
                    eventView.Path = eventView.Title.Replace(" ", "_") + "_" + eventView.Date.ToShortDateString().Replace("/", "_");
                    var typeEvent = db.TypeEvent.FirstOrDefault(x => x.ID == @eventView.TypeEventID);
                    AutoMapper.Mapper.Initialize(x =>
                    {
                        x.CreateMap<EventView, Event>();
                    });

                    int nextEventId;
                    var lastEvent = db.Event.OrderByDescending(x => x.ID).FirstOrDefault();
                    if (lastEvent == null)
                    {
                        nextEventId = 1;
                    }
                    else
                    {
                        nextEventId = lastEvent.ID + 1;
                    }

                    //Cover
                    if (eventView.CoverFile != null && eventView.CoverFile.ContentLength > 0)
                    {
                        var coverName = eventView.CoverFile.FileName;
                        var imageCoverExist = db.Image.FirstOrDefault(x => x.ImagePath == coverName);
                        
                        var path = Server.MapPath("~/Content/Photos/" + typeEvent.Name + "/" + nextEventId);
                        //path = path.Replace("Model", Constants.projectName);
                        //Check if directory exist
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        var coverPath = Path.Combine(path, coverName);
                        eventView.CoverFile.SaveAs(coverPath);

                        var newImage = new Image();
                        newImage.Title = eventView.Title;
                        newImage.ImagePath = coverName;
                        eventView.CoverImage = newImage;
                    }
                    //Images
                    if (eventView.Files.First() != null)
                    {
                        foreach (var file in eventView.Files)
                        {
                            if (file.ContentLength < 0)
                                continue;

                            var path = Server.MapPath("~/Content/Photos/" + typeEvent.Name + "/" + nextEventId);
                            //path = path.Replace("Model", Constants.projectName);
                            //Check if directory exist
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path); //Create directory if it doesn't exist
                            }

                            var imgName = file.FileName;
                            var imgPath = Path.Combine(path, imgName);
                            file.SaveAs(imgPath);

                            var image = new Image();
                            image.ImagePath = imgName;
                            image.Title = @eventView.Title;
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
            return View("~/Views/Loader/Events/Create.cshtml", @eventView);
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
            return View("~/Views/Loader/Events/Edit.cshtml", @event);
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
                db.Entry(@event).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.CoverImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @event.TypeEventID);
            return View("~/Views/Loader/Events/Edit.cshtml", @event);
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
            return View("~/Views/Loader/Events/Delete.cshtml", @event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            //Images
            var coverImage = @event.CoverImage;
            var images = new List<Image>(@event.Images);
            foreach (var image in images)
            {
                @event.Images.Remove(image);
                db.Image.Remove(image);
            }
            db.SaveChanges();
            //Delete Event
            db.Event.Remove(@event);
            //Cover Image
            db.Image.Remove(coverImage);
            db.SaveChanges();
            return RedirectToAction("~/Views/Loader/Events/Index.cshtml");
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
