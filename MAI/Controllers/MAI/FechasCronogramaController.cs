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
    public class FechasCronogramaController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblFechaCronogramas/Create
        public ActionResult Create(long id)
        {
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
        public ActionResult Create([Bind(Include = "tblFechaCronograma_ID,tblCronogramaProy_ID,cro_Actividad,cro_FechaInicio,cro_FechaFin,cro_Indicador")] tblFechaCronograma tblFechaCronograma)
        {
            if (ModelState.IsValid)
            {
                db.tblFechaCronograma.Add(tblFechaCronograma);
                db.SaveChanges();
                return RedirectToAction("Edit", "CronogramaProy", new { id = tblFechaCronograma.tblCronogramaProy_ID });
            }

            ViewBag.tblCronogramaProy_ID = new SelectList(db.tblCronogramaProy, "tblCronogramaProy_ID", "tblCronogramaProy_ID", tblFechaCronograma.tblCronogramaProy_ID);
            return View(tblFechaCronograma);
        }


        // GET: tblFechaCronogramas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFechaCronograma tblFechaCronograma = db.tblFechaCronograma.Find(id);
            if (tblFechaCronograma == null)
            {
                return HttpNotFound();
            }
            return View(tblFechaCronograma);
        }

        // POST: tblFechaCronogramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblFechaCronograma tblFechaCronograma = db.tblFechaCronograma.Find(id);
            db.tblFechaCronograma.Remove(tblFechaCronograma);
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
