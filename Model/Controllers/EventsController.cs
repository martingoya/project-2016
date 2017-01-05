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
            var services = db.Service
                .Include(y => y.TypeService1)
                .ToList()
                .Select(x => new
                {
                    ServiceID = x.ID,
                    Data = string.Format("({0}) - {1}", x.TypeService1.Name, x.Name)
                });
            ViewBag.ServiceID = new SelectList(services, "ServiceID", "Data");
            return View(new EventView());
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,TypeEventID,Date,Text,ImageID,ServiceID,VideoLink,IsImage,Controller,Action")] EventView @eventView, FormCollection fc)
        {
            List<ServiceView> serviceSession = new List<ServiceView>();
            if (Request.Form["addService"] != null)
            {           
                if (Session["ListService"] != null)
                {
                    serviceSession = (List<ServiceView>)Session["ListService"];
                }
                if (!serviceSession.Any(x => x.ID == @eventView.ServiceID))
                {
                    var serviceDB = db.Service.Include(y => y.TypeService1).FirstOrDefault(x => x.ID == @eventView.ServiceID);
                    ServiceView service = new ServiceView();
                    service.ID = serviceDB.ID;
                    service.Name = serviceDB.Name;
                    service.TypeService = serviceDB.TypeService1.Name;
                    serviceSession.Add(service);
                    Session["ListService"] = serviceSession;
                }
            }
            else
            {
                if (Request.Form["createEvent"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        AutoMapper.Mapper.Initialize(x =>
                        {
                            x.CreateMap<EventView, Event>();
                        });
                        serviceSession = (List<ServiceView>)Session["ListService"];
                        var serviceIDs = serviceSession.Select(k => k.ID).ToList();
                        var servicesToAdd = db.Service.Join(serviceIDs, x => x.ID, y => y, (x, y) => x);
                        foreach (var item in servicesToAdd)
                        {
                            @eventView.Service.Add(item);
                        }
                        db.Event.Add(AutoMapper.Mapper.Map<Event>(@eventView));
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.ImageID = new SelectList(db.Image, "ID", "Title", @eventView.ImageID);
            ViewBag.TypeEventID = new SelectList(db.TypeEvent, "ID", "Name", @eventView.TypeEventID);
            var serviceSessionIDs = serviceSession.Select(k => k.ID).ToList();
            var services = db.Service
                .Where(z => !serviceSessionIDs.Contains(z.ID))
                .Include(y => y.TypeService1)
                .ToList()
                .Select(x => new
                {
                    ServiceID = x.ID,
                    Data = string.Format("({0}) - {1}", x.TypeService1.Name, x.Name)
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
