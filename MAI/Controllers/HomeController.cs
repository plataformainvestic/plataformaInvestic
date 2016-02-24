using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        investicEntities db = new investicEntities();
        // GET: Home

        public ActionResult Index()
        {
            var usuario = AspNetUsers.GetUserId(User.Identity.Name);
            AspNetUsers u = db.AspNetUsers.Find(usuario);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
    }
}