using INI.ChamiloWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INI.Controllers.Aprendizaje
{
    //[Authorize(Roles = "Administrator,Estudiante,Maestro")]
    [Authorize]
    public class APRController : Controller
    {
        //
        // GET: /APR/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChamiloCourses()
        {
            String nameuser = User.Identity.Name;
            IchamiloClient ch = new IchamiloClient();
            ch.Open();
            var chamilouser = ch.getUserChamilo(nameuser);
            List<chamiloCource> chamilocourses = ch.getCourcesByUser(chamilouser.Username).ToList();
            return View(chamilocourses);
            //return PartialView("_chamiloCourses", chamilocourses);
        }

        public ActionResult ChamiloLessons(String coursecode, String name)
        {
            ViewBag.CourseCode = coursecode;
            ViewBag.Name = name;
            IchamiloClient ch = new IchamiloClient();
            ch.Open();
            List<ChamiloLesson> chamilolessons = ch.getLessonByCource(coursecode).ToList();
            return View(chamilolessons);
            //return PartialView("_chamiloLessons", chamilolessons);

        }
	}
}