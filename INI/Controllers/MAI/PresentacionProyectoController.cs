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
    public class PresentacionProyectoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: PresentacionProyectoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPresentacionProyecto tblPresentacionProyecto = db.tblPresentacionProyecto.Find(id);
            if (tblPresentacionProyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre", tblPresentacionProyecto.tblEjeInvestigacion_ID);
            return View(tblPresentacionProyecto);
        }

        // POST: PresentacionProyectoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblPresentacionProyecto_ID,preProy_tituloProy,preProy_resumenProy,preProy_palabrasClavesProy,tblEjeInvestigacion_ID,preProy_ejeInvestigacionProy")] tblPresentacionProyecto tblPresentacionProyecto)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion miProyecto = (from t in db.tblProyectosInvestigacion
                                                        where t.tblPresentacionProyecto_ID == tblPresentacionProyecto.tblPresentacionProyecto_ID
                                                        select t).FirstOrDefault();
                miProyecto.proyInv_nombreProyecto = tblPresentacionProyecto.preProy_tituloProy;
                miProyecto.proyInv_fechaUltimaModificacion = DateTime.Now;
                db.Entry(miProyecto).State = EntityState.Modified;
                db.Entry(tblPresentacionProyecto).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Edit", new { id = tblPresentacionProyecto.tblPresentacionProyecto_ID });
            }
            ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre", tblPresentacionProyecto.tblEjeInvestigacion_ID);
            return View(tblPresentacionProyecto);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
