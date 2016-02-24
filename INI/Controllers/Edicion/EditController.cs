using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;


namespace INI.Controllers.Edicion
{
    //[Authorize(Roles="Editor")]
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Index()
        {
            //ViewBag.mensaje = "no es";

            //if (Roles.IsUserInRole("Editor"))
            //{
            //    ViewBag.mensaje = "Este es el editor";
            //} 
            return View();
        }
    }
}