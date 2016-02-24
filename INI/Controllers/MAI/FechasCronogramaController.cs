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
    public class FechasCronogramaController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblFechaCronogramas/Create
        public ActionResult CreateFecha(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCronogramaProy miCronograma = db.tblCronogramaProy.Find(id);
            tblFechaCronograma nuevaFecha = new tblFechaCronograma();
            nuevaFecha.tblCronogramaProy = miCronograma;
            return PartialView(nuevaFecha);
        }

        // POST: tblFechaCronogramas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFecha([Bind(Include = "tblFechaCronograma_ID,tblCronogramaProy_ID,cro_Actividad,cro_FechaInicio,cro_FechaFin,cro_Indicador")] tblFechaCronograma tblFechaCronograma)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                var miProyecto = db.tblProyectosInvestigacion.Where(t => t.tblCronogramaProy_ID == tblFechaCronograma.tblCronogramaProy_ID);
                if (miProyecto == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!(tblFechaCronograma.cro_Actividad == null ||
                        tblFechaCronograma.cro_Indicador == null ||
                        tblFechaCronograma.cro_FechaInicio == tblFechaCronograma.cro_FechaFin))
                    {
                        db.tblFechaCronograma.Add(tblFechaCronograma);
                        db.SaveChanges();
                        return RedirectToAction("EditarProyecto", "ProyectosInvestigacion", new { id = miProyecto.FirstOrDefault().tblProyectosInvestigacion_ID });
                    }
                    else
                    {
                        return RedirectToAction("EditarProyecto", "ProyectosInvestigacion", new { id = miProyecto.FirstOrDefault().tblProyectosInvestigacion_ID });
                    }
                }
            }
            ViewBag.tblCronogramaProy_ID = new SelectList(db.tblCronogramaProy, "tblCronogramaProy_ID", "tblCronogramaProy_ID", tblFechaCronograma.tblCronogramaProy_ID);
            return View(tblFechaCronograma);
        }

        public ActionResult BorrarFechaCronograma(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var miFechaBorrar = db.tblFechaCronograma.Find(id);
            var miProyecto = db.tblProyectosInvestigacion.Where(t => t.tblCronogramaProy_ID == miFechaBorrar.tblCronogramaProy_ID);
            db.tblFechaCronograma.Remove(miFechaBorrar);
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
