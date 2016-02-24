using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;


namespace INI.Controllers.Administracion
{
    //[Authorize(Roles="Administrator,Administrador")]
    [Authorize]
    public class AdminController : Controller
    {
        private investicEntities db = new investicEntities();
        // GET: Admin
        
        public ActionResult Editor(string nombreu)
        {
            var perfil = db.AspNetUsers.Where(m => m.UserName == nombreu).First();

            return View(perfil);
        }

        public ActionResult Contratista(string nombreu){
            var perfil = db.AspNetUsers.Where(m => m.UserName == nombreu).First();
            return View(perfil);
        }
        public ActionResult Maestro(string nombreu)
        {
            var perfil = db.AspNetUsers.Where(m => m.UserName == nombreu).First();
            return View(perfil);
        }
        public ActionResult Estudiante(string nombreu)
        {
            var perfil = db.AspNetUsers.Where(m => m.UserName == nombreu).First();
            return View(perfil);
        }

        public ActionResult Administrador(string nombreau)
        {
            var perfil = db.AspNetUsers.Where(m => m.UserName == nombreau).First();
            return View(perfil);
        }
        
    }
}