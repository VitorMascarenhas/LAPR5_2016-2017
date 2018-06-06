using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortoGO.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            Response.StatusCode = 200;
            return View();
        }

        public ActionResult InternalError()
        {
            Response.StatusCode = 200;
            return View();
        }
    }
}