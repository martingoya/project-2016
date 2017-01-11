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
        private int elementsForView = 5;
        private ohlalaEntities db = new ohlalaEntities();
        public ActionResult Fifteen(string identifier)
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == "Fifteen")
                        .Include(z => z.Service)
                        .Include(k => k.Service.Select(j => j.TypeServices)).ToList();
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
                    var eventFifteen = db.Event.Where(x => x.Action == identifier).ToList();
                }
            }

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
