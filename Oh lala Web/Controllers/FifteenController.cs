using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oh_lala_Web.Controllers.Fifteen
{
    public class FifteenController : Controller
    {
        private ohlalaEntities db = new ohlalaEntities();
        //
        // GET: /Fifteen/

        public ActionResult Abril()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Abril");
            return View("~/Views/Section/Fifteen/Abril.cshtml", actualEvent);
        }
        public ActionResult Cati()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Cati");
            return View("~/Views/Section/Fifteen/Cati.cshtml", actualEvent);
        }
        public ActionResult Martina_Campos_Perez()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Martina_Campos_Perez");
            return View("~/Views/Section/Fifteen/Martina_Campos_Perez.cshtml", actualEvent);
        }
        public ActionResult Cielo()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Cielo");
            return View("~/Views/Section/Fifteen/Cielo.cshtml", actualEvent);
        }
        public ActionResult Giuli()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Giuli");
            return View("~/Views/Section/Fifteen/Giuli.cshtml", actualEvent);
        }
        public ActionResult Sofia_Peluso()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Sofia_Peluso");
            return View("~/Views/Section/Fifteen/Sofia_Peluso.cshtml", actualEvent);
        }
        public ActionResult Martina_Poveda()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Martina_Poveda");
            return View("~/Views/Section/Fifteen/Martina_Poveda.cshtml", actualEvent);
        }
        public ActionResult Zoe()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Zoe");
            return View("~/Views/Section/Fifteen/Zoe.cshtml", actualEvent);
        }
        public ActionResult Mica()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Mica");
            return View("~/Views/Section/Fifteen/Mica.cshtml", actualEvent);
        }
        public ActionResult Sofia()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Sofia");
            return View("~/Views/Section/Fifteen/Sofia.cshtml", actualEvent);
        }
        public ActionResult Martina_Isabella()
        {
            var actualEvent = db.Event.FirstOrDefault(x => x.Path == "Martina_Isabella");
            return View("~/Views/Section/Fifteen/Martina_Isabella.cshtml", actualEvent);
        }
    }
}
