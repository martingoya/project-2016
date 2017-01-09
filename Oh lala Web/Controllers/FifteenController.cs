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
            var eventAbril = db.Event.FirstOrDefault(x => x.Title == "Abril");
            return View("~/Views/Section/Fifteen/Abril.cshtml", eventAbril);
        }
        public ActionResult Cati()
        {
            return View("~/Views/Section/Fifteen/Cati.cshtml");
        }
        public ActionResult Martina_Campos_Perez()
        {
            return View("~/Views/Section/Fifteen/Martina_Campos_Perez.cshtml");
        }
        public ActionResult Cielo()
        {
            return View("~/Views/Section/Fifteen/Cielo.cshtml");
        }
        public ActionResult Giuli()
        {
            return View("~/Views/Section/Fifteen/Giuli.cshtml");
        }
        public ActionResult Sofia_Peluso()
        {
            return View("~/Views/Section/Fifteen/Sofia_Peluso.cshtml");
        }
        public ActionResult Martina_Poveda()
        {
            return View("~/Views/Section/Fifteen/Martina_Poveda.cshtml");
        }
        public ActionResult Zoe()
        {
            return View("~/Views/Section/Fifteen/Zoe.cshtml");
        }
        public ActionResult Mica()
        {
            return View("~/Views/Section/Fifteen/Mica.cshtml");
        }
        public ActionResult Sofia()
        {
            return View("~/Views/Section/Fifteen/Sofia.cshtml");
        }
        public ActionResult Martina_Isabella()
        {
            return View("~/Views/Section/Fifteen/Martina_Isabella.cshtml");
        }
    }
}
