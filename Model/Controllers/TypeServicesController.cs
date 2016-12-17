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
    public class TypeServicesController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();

        // GET: TypeServices
        public ActionResult Index()
        {
            return View(db.TypeService.ToList());
        }

        // GET: TypeServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeService typeService = db.TypeService.Find(id);
            if (typeService == null)
            {
                return HttpNotFound();
            }
            return View(typeService);
        }

        // GET: TypeServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] TypeServiceView typeServiceView)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.Initialize(x => {
                    x.CreateMap<TypeServiceView, TypeService>();
                });
                db.TypeService.Add(AutoMapper.Mapper.Map<TypeService>(typeServiceView));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeServiceView);
        }

        // GET: TypeServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeService typeService = db.TypeService.Find(id);
            if (typeService == null)
            {
                return HttpNotFound();
            }
            return View(typeService);
        }

        // POST: TypeServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] TypeService typeService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeService);
        }

        // GET: TypeServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeService typeService = db.TypeService.Find(id);
            if (typeService == null)
            {
                return HttpNotFound();
            }
            return View(typeService);
        }

        // POST: TypeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeService typeService = db.TypeService.Find(id);
            db.TypeService.Remove(typeService);
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
