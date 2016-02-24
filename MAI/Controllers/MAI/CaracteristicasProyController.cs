using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;
using MAI.Controllers.MAI;

namespace MAI.Controllers.Proyectos
{
    [Authorize]
    public class CaracteristicasProyController : Controller
    {
        private investicEntities db = new investicEntities();


        // GET: CaracteristicasProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCaracteristicasProy tblCaracteristicasProy = db.tblCaracteristicasProy.Find(id);
            if (tblCaracteristicasProy == null)
            {
                return HttpNotFound();
            }
            return View(tblCaracteristicasProy);
        }

        // POST: CaracteristicasProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblCaracteristicasProy_ID,carProy_resultadosEsperadosProy,carProy_caracterizacionProy")] tblCaracteristicasProy tblCaracteristicasProy)
        {
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion miProyecto = (from t in db.tblProyectosInvestigacion
                                                        where t.tblCaracteristicasProy_ID == tblCaracteristicasProy.tblCaracteristicasProy_ID
                                                        select t).FirstOrDefault();
                miProyecto.proyInv_fechaUltimaModificacion = DateTime.Now;
                db.Entry(miProyecto).State = EntityState.Modified;
                db.Entry(tblCaracteristicasProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblCaracteristicasProy.tblCaracteristicasProy_ID });
            }
            return View(tblCaracteristicasProy);
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
