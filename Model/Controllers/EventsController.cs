using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Models;

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
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,TypeEventID,Date,Text,ImageID,VideoLink,IsImage,Controller,Action")] EventView @eventView)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.Initialize(x => {
                    x.CreateMap<EventView, Event>();
                });
                db.Event.Add(AutoMapper.Mapper.Map<Event>(@eventView));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @eventView.ImageID);
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
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @event.ImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @event.TypeEventID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,TypeEventID,Date,Text,ImageID,VideoLink,IsImage,Controller,Action")] Event @event)
        {
            if (ModelState.IsValid)
            {
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
