using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oh_lala_Web.Controllers.Bodas
{
    public class BodasController : Controller
    {
        //
        // GET: /Bodas/
        public ActionResult Vero_Y_Lau()
        {
            return View("~/Views/Section/Bodas/Vero_y_Lau.cshtml");
        }

    }
}
