using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using System.Web.Security;

namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class equiposController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: equipos
        [Authorize]
        public ActionResult Index()
        {

            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {
                var equipos = db.tblEquiposTrabajo.Include(e => e.AspNetUsers);


                return View(equipos.ToList());
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }


        public JsonResult ListaNombre(string Cedula)
        {
            var nombres = (from n in db.AspNetUsers where n.Cedula.Equals(Cedula) select n.Nombres);

            return Json(new SelectList(nombres.ToArray(),JsonRequestBehavior.AllowGet));       
        }


        // GET: equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquiposTrabajo equipos = db.tblEquiposTrabajo.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: equipos/Create
        [Authorize]
        public ActionResult Create()
        {
            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {
                ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName");


             //   List<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.Cargo.Equals("Coordinador")).ToList();

                List<AspNetUsers> usucoord = AspNetUsers.GetUsersInRole("Coordinador");
                //List<AspNetUsers> ususinequip = new List<AspNetUsers>();

               //foreach(var x in usucoord)
               //{
               //    List<tblEquiposTrabajo> et = new List<tblEquiposTrabajo>();

               //    foreach(var y in et)
               //    { 

               //     if(y.Nombre_Coordinador == null)
               //     {
               //        ususinequip.Add(x);

               //     }
               //    }

               //}

                ViewBag.Nombre_Coordinador = new SelectList(usucoord, "Id", "LastName");

                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
                
            }
        }

        // POST: equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Equipo,Nombre_Equipo,Id_Coordinador,Nombre_Coordinador")] tblEquiposTrabajo equipos)
        {
            if (ModelState.IsValid)
            {
                equipos.Id_Coordinador = equipos.Nombre_Coordinador;
                equipos.Nombre_Coordinador = (from n in db.AspNetUsers where n.Id.Equals(equipos.Nombre_Coordinador) select n.LastName).SingleOrDefault();
                db.tblEquiposTrabajo.Add(equipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // GET: equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquiposTrabajo equipos = db.tblEquiposTrabajo.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // POST: equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Equipo,Nombre_Equipo,Id_Coordinador,Nombre_Coordinador")] tblEquiposTrabajo equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // GET: equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquiposTrabajo equipos = db.tblEquiposTrabajo.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEquiposTrabajo equipos = db.tblEquiposTrabajo.Find(id);
            db.tblEquiposTrabajo.Remove(equipos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
