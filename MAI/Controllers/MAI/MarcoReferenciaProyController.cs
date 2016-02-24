using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.Proyectos
{
    [Authorize]
    public class MarcoReferenciaProyController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: MarcoReferenciaProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMarcoReferenciaProy tblMarcoReferenciaProy = db.tblMarcoReferenciaProy.Find(id);
            if (tblMarcoReferenciaProy == null)
            {
                return HttpNotFound();
            }
            return View(tblMarcoReferenciaProy);
        }

        // POST: MarcoReferenciaProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblMarcoReferenciaProy_ID,marRefProy_marcoTeoricoProy,marRefProy_marcoAntecedentesProy,marRefProy_marcoConceptualProy")] tblMarcoReferenciaProy tblMarcoReferenciaProy)
        {
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion miProyecto = (from t in db.tblProyectosInvestigacion
                                                        where t.tblMarcoReferenciaProy_ID == tblMarcoReferenciaProy.tblMarcoReferenciaProy_ID
                                                        select t).FirstOrDefault();
                miProyecto.proyInv_fechaUltimaModificacion = DateTime.Now;
                db.Entry(miProyecto).State = EntityState.Modified;
                db.Entry(tblMarcoReferenciaProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblMarcoReferenciaProy.tblMarcoReferenciaProy_ID });
            }
            return View(tblMarcoReferenciaProy);
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
