using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Equipos
{
    //[Authorize(Roles = "Administrator,Estudiante,Maestro")]
    [Authorize]
    public class EquipoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Equipo
        public ActionResult Index()
        {
            return View(db.tblEquipo.ToList());
        }

        // GET: Equipo/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquipo tblEquipo = db.tblEquipo.Find(id);
            if (tblEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tblEquipo);
        }

        // GET: Equipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblEquipo_ID,equ_descripcion,equ_Observacion")] tblEquipo tblEquipo)
        {
            if (ModelState.IsValid)
            {
                db.tblEquipo.Add(tblEquipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEquipo);
        }

        // GET: Equipo/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquipo tblEquipo = db.tblEquipo.Find(id);
            if (tblEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tblEquipo);
        }

        // POST: Equipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblEquipo_ID,equ_descripcion,equ_Observacion")] tblEquipo tblEquipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEquipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEquipo);
        }

        // GET: Equipo/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEquipo tblEquipo = db.tblEquipo.Find(id);
            if (tblEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tblEquipo);
        }

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblEquipo tblEquipo = db.tblEquipo.Find(id);
            db.tblEquipo.Remove(tblEquipo);
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
