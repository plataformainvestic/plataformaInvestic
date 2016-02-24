using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Maestro")]
    [Authorize]
    public class EjeInvestigacionController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: EjeInvestigacion
        public ActionResult Index()
        {
            return View(db.tblEjeInvestigacion.ToList());
        }

        // GET: EjeInvestigacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEjeInvestigacion tblEjeInvestigacion = db.tblEjeInvestigacion.Find(id);
            if (tblEjeInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblEjeInvestigacion);
        }

        // GET: EjeInvestigacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EjeInvestigacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblEjeInvestigacion_ID,ejeInv_nombre")] tblEjeInvestigacion tblEjeInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.tblEjeInvestigacion.Add(tblEjeInvestigacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEjeInvestigacion);
        }

        // GET: EjeInvestigacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEjeInvestigacion tblEjeInvestigacion = db.tblEjeInvestigacion.Find(id);
            if (tblEjeInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblEjeInvestigacion);
        }

        // POST: EjeInvestigacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblEjeInvestigacion_ID,ejeInv_nombre")] tblEjeInvestigacion tblEjeInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEjeInvestigacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEjeInvestigacion);
        }

        // GET: EjeInvestigacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEjeInvestigacion tblEjeInvestigacion = db.tblEjeInvestigacion.Find(id);
            if (tblEjeInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblEjeInvestigacion);
        }

        // POST: EjeInvestigacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEjeInvestigacion tblEjeInvestigacion = db.tblEjeInvestigacion.Find(id);
            db.tblEjeInvestigacion.Remove(tblEjeInvestigacion);
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
