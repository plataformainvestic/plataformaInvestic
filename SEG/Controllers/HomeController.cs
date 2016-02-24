using System.Web.Mvc;

namespace SEG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "contratista")]
        public ActionResult SinAutorizar()
        {
            return View();
        }

        public ActionResult Confirma()
        {
            return View();
        }

        [Authorize(Roles = "contratista")]
        public ActionResult RegInfSem()
        {
            return View();
        }

        public ActionResult RegInfMens()
        {
            return View();
        }
    }
}