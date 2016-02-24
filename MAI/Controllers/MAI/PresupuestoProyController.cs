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
    public class PresupuestoProyController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: PresupuestoProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPresupuestoProy tblPresupuestoProy = db.tblPresupuestoProy.Find(id);
            if (tblPresupuestoProy == null)
            {
                return HttpNotFound();
            }
            return View(tblPresupuestoProy);
        }

        // POST: PresupuestoProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblPresupuestoProy_ID,pre_financiacionInvestic,pre_totatInvestic,pre_totalOtraFuente,pre_total")] tblPresupuestoProy tblPresupuestoProy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPresupuestoProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblPresupuestoProy.tblPresupuestoProy_ID });
            }
            return View(tblPresupuestoProy);
        }


        // GET: RubroPresupuesto/Create
        public ActionResult Create()
        {
            ViewBag.tblPresupuestoProy_ID = new SelectList(db.tblPresupuestoProy, "tblPresupuestoProy_ID", "tblPresupuestoProy_ID");
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre");
            return View();
        }

        // POST: RubroPresupuesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblRubroPresupuesto_ID,tblPresupuestoProy_ID,tblRubro_ID,rubPre_valor,rubPre_fuente")] tblRubroPresupuesto tblRubroPresupuesto)
        {
            if (ModelState.IsValid)
            {
                db.tblRubroPresupuesto.Add(tblRubroPresupuesto);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblRubroPresupuesto.tblPresupuestoProy_ID });
            }

            ViewBag.tblPresupuestoProy_ID = new SelectList(db.tblPresupuestoProy, "tblPresupuestoProy_ID", "tblPresupuestoProy_ID", tblRubroPresupuesto.tblPresupuestoProy_ID);
            ViewBag.tblRubro_ID = new SelectList(db.tblRubro, "tblRubro_ID", "rub_nombre", tblRubroPresupuesto.tblRubro_ID);
            return View(tblRubroPresupuesto);
        }

        // GET: RubroPresupuesto/Delete/5
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

        // POST: RubroPresupuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblRubroPresupuesto tblRubroPresupuesto = db.tblRubroPresupuesto.Find(id);
            db.tblRubroPresupuesto.Remove(tblRubroPresupuesto);
            long ID = tblRubroPresupuesto.tblPresupuestoProy_ID;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = ID });
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
