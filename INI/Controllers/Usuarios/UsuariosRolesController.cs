using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using INI.ChamiloWS;


namespace INI.Controllers.Usuarios
{
    [Authorize(Roles = "Administrator")]
    public class UsuariosRolesController : Controller
    {
        private investicEntities db = new investicEntities();
        // GET: UsuariosRoles
        public ActionResult Index()
        {
            var userprofiles = db.AspNetUsers;
            ViewBag.UserId = userprofiles;
            var roles = db.AspNetRoles;
            ViewBag.RoleId = roles;
            return View(db.AspNetUserRoles.ToList());
        }

        // GET: /AdministrarRoles/Create

        public ActionResult Create()
        {
            //var userprofiles = db.UserProfile.Include(u => u.webpages_Membership);
            //userprofiles = userprofiles.Where(m => m.UserId != 1);
            //var roles = db.webpages_Roles.Where(r => r.RoleId != 1);
            //ViewBag.UserId = new SelectList(userprofiles, "UserId", "UserName");
            //ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            //return View();

            var userprofiles = db.AspNetUsers;
            ViewBag.UserId = new SelectList(userprofiles, "Id", "Name");
            var roles = db.AspNetRoles;
            ViewBag.RoleId = new SelectList(roles,"Id", "Name");
            return View();
        }


        //
        // POST: /UsuariosRoles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspNetUserRoles crear)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IchamiloClient client = new IchamiloClient();
                    client.Open();
                    ChamiloUser chuser = client.getUserChamilo(crear.AspNetUsers.Name);
                    if (crear.AspNetRoles.Name == "Maestro" && chuser.Status == 5)
                    {
                        chuser.Status = 1;
                        client.updateUser(chuser, chuser.Username);
                    }
                    if (crear.AspNetRoles.Name == "Estudiante" && chuser.Status == 1)
                    {
                        chuser.Status = 5;
                        client.updateUser(chuser, chuser.Username);
                    }
                    db.AspNetUserRoles.Add(crear);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "El usuario ya pertenece al rol.";
                return RedirectToAction("Index");
            }

            return View(crear);
        }
        //
        // GET: /AdministrarRoles/Delete/5

        public ActionResult Delete(int id)
        {
            AspNetUserRoles Eliminaregistro = db.AspNetUserRoles.Find(id);
            if (Eliminaregistro == null)
            {
                return HttpNotFound();
            }
            return View(Eliminaregistro);
        }
        //
        // POST: /AdministrarRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserRoles eliminar = db.AspNetUserRoles.Find(id);
            db.AspNetUserRoles.Remove(eliminar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}