using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Oh_lala_Web.Models;
using Model.Models;
using System.Data.Entity;

namespace Oh_lala_Web.Controllers
{
    public class SectionController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();
        public ActionResult Fifteen()
        {
            var events = db.Event
                    .Include(x => x.Image)
                    .Include(y => y.TypeEvent)
                    .Where(l => l.TypeEvent.Name == "Fifteen")
                    .Include(z => z.Service)
                    .Include(k => k.Service.Select(j => j.TypeServices));
            return View(events.ToList());
        }

        public ActionResult Bodas()
        {
            return View();
        }

        public ActionResult Infantiles()
        {
            return View();
        }

        public ActionResult Newborn()
        {
            return View();
        }
    }
}
