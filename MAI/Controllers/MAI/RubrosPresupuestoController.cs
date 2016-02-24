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
    public class RubrosPresupuestoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: RubrosPresupuesto/Create
        public ActionResult Create()
        {
            ViewBag.tblPresupuestoProy_ID = new SelectList(db.tblPresupuestoProy, "tblPresupuestoProy_ID", "tblPresupuestoProy_ID");
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre");
            return View();
        }

        // POST: RubrosPresupuesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblRubroPresupuesto_ID,tblPresupuestoProy_ID,tblRubro_ID,rubPre_valor,rubPre_fuente,rubPre_justificacion")] tblRubroPresupuesto tblRubroPresupuesto)
        {
            if (ModelState.IsValid)
            {
                db.tblRubroPresupuesto.Add(tblRubroPresupuesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblPresupuestoProy_ID = new SelectList(db.tblPresupuestoProy, "tblPresupuestoProy_ID", "tblPresupuestoProy_ID", tblRubroPresupuesto.tblPresupuestoProy_ID);
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre", tblRubroPresupuesto.tblRubro_ID);
            return View(tblRubroPresupuesto);
        }

        // GET: RubrosPresupuesto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRubroPresupuesto tblRubroPresupuesto = db.tblRubroPresupuesto.Find(id);
            if (tblRubroPresupuesto == null)
            {
                return HttpNotFound();
            }
            return View(tblRubroPresupuesto);
        }

        // POST: RubrosPresupuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblRubroPresupuesto tblRubroPresupuesto = db.tblRubroPresupuesto.Find(id);
            db.tblRubroPresupuesto.Remove(tblRubroPresupuesto);
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
