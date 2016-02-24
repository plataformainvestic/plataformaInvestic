using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cl = ClassLibrary;

namespace IEP.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Bitacoras()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
