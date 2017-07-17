using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.Models;
using System.Data.Entity;

namespace OhlalaWebWithAuth.Controllers
{
    public class SectionController : Controller
    {
        private int elementsForView = 5;
        private ohlalaEntities db = new ohlalaEntities();
        public ActionResult Quince(string identifier)
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == Constants.Events.Quince.ToString()).ToList();
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
                    var eventQuince = db.Event.Where(x => x.Path == identifier).ToList();
                }
            }
            Constants.setFullPathOnlyCoverImageForAllEvents(events);
            return View(events.ToList());
        }

        public ActionResult Bodas(string identifier)
        {
            var events = db.Event
                        .Include(x => x.CoverImage)
                        .Include(y => y.TypeEvent)
                        .Where(l => l.TypeEvent.Name == Constants.Events.Bodas.ToString()).ToList();
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
                    var eventQuince = db.Event.Where(x => x.Path == identifier).ToList();
                }
            }
            Constants.setFullPathOnlyCoverImageForAllEvents(events);
            return View(events.ToList());
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
