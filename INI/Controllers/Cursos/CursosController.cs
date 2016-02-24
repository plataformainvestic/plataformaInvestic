using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using INI.Models.Database.Adds;

namespace INI.Controllers.Cursos
{
    public class CursosController : Controller
    {
        private chamiloEntities db = new chamiloEntities();
        // GET: Cursos
        public ActionResult categoriacursos()
        {
            return View(db.course_category.ToList());
            
        }
    }
}