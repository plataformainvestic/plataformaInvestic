using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Proyectos
{
    //[Authorize(Roles = "Administrator,Maestro")]
    [Authorize]
    public class RubrosPresupuestoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: RubrosPresupuesto/Create
        public ActionResult CreatePresupuesto(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPresupuestoProy miPresupuesto = db.tblPresupuestoProy.Find(id);
            tblRubroPresupuesto nuevoRubro = new tblRubroPresupuesto();
            nuevoRubro.tblPresupuestoProy = miPresupuesto;
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre");
            return View(nuevoRubro);
        }

        // POST: RubrosPresupuesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePresupuesto([Bind(Include = "tblRubroPresupuesto_ID,tblPresupuestoProy_ID,tblRubro_ID,rubPre_valor,rubPre_fuente,rubPre_justificacion")] tblRubroPresupuesto tblRubroPresupuesto)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                var miProyecto = db.tblProyectosInvestigacion.Where(t => t.tblPresupuestoProy_ID == tblRubroPresupuesto.tblPresupuestoProy_ID);
                if (miProyecto == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if(!(tblRubroPresupuesto.rubPre_fuente == null ||
                        tblRubroPresupuesto.rubPre_justificacion == null ||
                        tblRubroPresupuesto.rubPre_valor.Equals("0")))
                    {
                        db.tblRubroPresupuesto.Add(tblRubroPresupuesto);
                        db.SaveChanges();
                        return RedirectToAction("EditarProyecto", "ProyectosInvestigacion", new { id = miProyecto.FirstOrDefault().tblProyectosInvestigacion_ID });
                    }
                    else
                    {
                        return RedirectToAction("EditarProyecto", "ProyectosInvestigacion", new { id = miProyecto.FirstOrDefault().tblProyectosInvestigacion_ID });
                    }
                }
            }
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre", tblRubroPresupuesto.tblRubro_ID);
            return View(tblRubroPresupuesto);
        }

        public ActionResult BorrarRubroPresupuesto(long? id)
        {
            var miRubroPresupuesto = db.tblRubroPresupuesto.Find(id);
            var miProyecto = db.tblProyectosInvestigacion.Where(t => t.tblPresupuestoProy_ID == miRubroPresupuesto.tblPresupuestoProy_ID);
            db.tblRubroPresupuesto.Remove(miRubroPresupuesto);
            db.SaveChanges();
            return RedirectToAction("EditarProyecto", "ProyectosInvestigacion", new { id = miProyecto.FirstOrDefault().tblProyectosInvestigacion_ID });
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
