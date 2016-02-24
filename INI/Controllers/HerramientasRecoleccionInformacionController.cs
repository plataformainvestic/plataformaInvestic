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
    //[Authorize(Roles = "Administrator,Maestro,Estudiante")]
    [Authorize]
    public class HerramientasRecoleccionInformacionController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: HerramientasRecoleccionInformacion
        public ActionResult Index()
        {
            return View(db.tblHerramientasRecoleccionInformacion.ToList());
        }

        // GET: HerramientasRecoleccionInformacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion = db.tblHerramientasRecoleccionInformacion.Find(id);
            if (tblHerramientasRecoleccionInformacion == null)
            {
                return HttpNotFound();
            }
            return View(tblHerramientasRecoleccionInformacion);
        }

        // GET: HerramientasRecoleccionInformacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HerramientasRecoleccionInformacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,HerramientaRecoleccion,Descripcion")] tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion)
        {
            if (ModelState.IsValid)
            {
                db.tblHerramientasRecoleccionInformacion.Add(tblHerramientasRecoleccionInformacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblHerramientasRecoleccionInformacion);
        }

        // GET: HerramientasRecoleccionInformacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion = db.tblHerramientasRecoleccionInformacion.Find(id);
            if (tblHerramientasRecoleccionInformacion == null)
            {
                return HttpNotFound();
            }
            return View(tblHerramientasRecoleccionInformacion);
        }

        // POST: HerramientasRecoleccionInformacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,HerramientaRecoleccion,Descripcion")] tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHerramientasRecoleccionInformacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblHerramientasRecoleccionInformacion);
        }

        // GET: HerramientasRecoleccionInformacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion = db.tblHerramientasRecoleccionInformacion.Find(id);
            if (tblHerramientasRecoleccionInformacion == null)
            {
                return HttpNotFound();
            }
            return View(tblHerramientasRecoleccionInformacion);
        }

        // POST: HerramientasRecoleccionInformacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHerramientasRecoleccionInformacion tblHerramientasRecoleccionInformacion = db.tblHerramientasRecoleccionInformacion.Find(id);
            db.tblHerramientasRecoleccionInformacion.Remove(tblHerramientasRecoleccionInformacion);
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
