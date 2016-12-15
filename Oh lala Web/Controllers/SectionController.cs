using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Oh_lala_Web.Models;

namespace Oh_lala_Web.Controllers
{
    public class SectionController : Controller
    {
        private EventEntities db = new EventEntities("OhLalaPHEntities");

        public ActionResult Fifteen()
        {
            return View();
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
