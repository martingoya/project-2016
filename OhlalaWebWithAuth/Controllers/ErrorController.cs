using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OhlalaWebWithAuth.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }
        public ActionResult NotFound(string aspxerrorpath)
        {
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Response.StatusCode = 404;
            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
                return RedirectToAction("NotFound");
            return View("NotFound");
        }

    }
}
