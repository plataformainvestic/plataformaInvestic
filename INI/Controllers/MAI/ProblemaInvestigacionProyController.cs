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
    public class ProblemaInvestigacionProyController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: ProblemaInvestigacionProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProblemaInvestigacionProy tblProblemaInvestigacionProy = db.tblProblemaInvestigacionProy.Find(id);
            if (tblProblemaInvestigacionProy == null)
            {
                return HttpNotFound();
            }
            return View(tblProblemaInvestigacionProy);
        }

        // POST: ProblemaInvestigacionProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblProblemaInvestigacionProy_ID,proInvProy_planteamientoProblemaProy,proInvProy_preguntaInvestigacionProy,proInvProy_subpreguntaInvestigacionProy,proInvProy_justificacionProy,proInvProy_objetivoGeneralProy,proInvProy_objetivosEspecificosProy")] tblProblemaInvestigacionProy tblProblemaInvestigacionProy)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion miProyecto = (from t in db.tblProyectosInvestigacion
                                                        where t.tblProyectosInvestigacion_ID == tblProblemaInvestigacionProy.tblProblemaInvestigacionProy_ID
                                                        select t).FirstOrDefault();
                miProyecto.proyInv_fechaUltimaModificacion = DateTime.Now;
                db.Entry(miProyecto).State = EntityState.Modified;
                db.Entry(tblProblemaInvestigacionProy).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Edit", new { id = tblProblemaInvestigacionProy.tblProblemaInvestigacionProy_ID });
            }
            return View(tblProblemaInvestigacionProy);
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
