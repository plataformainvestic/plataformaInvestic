using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Integrantes
{
    //[Authorize(Roles = "Administrator,Estudiante,Maestro")]
    [Authorize]
    public class IntegrantesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Integrantes
        public ActionResult Index()
        {
            ViewBag.tblEquipo_ID = new SelectList(db.tblEquipo, "tblEquipo_ID", "equ_descripcion");

            var tblIntegrante = db.tblIntegrante.Include(t => t.tblEquipo);
            return View(tblIntegrante.ToList());

        }

        // GET: Integrantes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblIntegrante tblIntegrante = db.tblIntegrante.Find(id);
            if (tblIntegrante == null)
            {
                return HttpNotFound();
            }
            return View(tblIntegrante);
        }

        // GET: Integrantes/Create
        public ActionResult Create()
        {
            ViewBag.tblEquipo_ID = new SelectList(db.tblEquipo, "tblEquipo_ID", "equ_descripcion");
            return View();
        }

        // POST: Integrantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblIntegrante_ID,int_nombre,int_apellido,int_correo,int_telefono,tblEquipo_ID")] tblIntegrante tblIntegrante)
        {
            if (ModelState.IsValid)
            {
                db.tblIntegrante.Add(tblIntegrante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblEquipo_ID = new SelectList(db.tblEquipo, "tblEquipo_ID", "equ_descripcion", tblIntegrante.tblEquipo_ID);
            return View(tblIntegrante);
        }

        // GET: Integrantes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblIntegrante tblIntegrante = db.tblIntegrante.Find(id);
            if (tblIntegrante == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblEquipo_ID = new SelectList(db.tblEquipo, "tblEquipo_ID", "equ_descripcion", tblIntegrante.tblEquipo_ID);
            return View(tblIntegrante);
        }

        // POST: Integrantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblIntegrante_ID,int_nombre,int_apellido,int_correo,int_telefono,tblEquipo_ID")] tblIntegrante tblIntegrante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblIntegrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblEquipo_ID = new SelectList(db.tblEquipo, "tblEquipo_ID", "equ_descripcion", tblIntegrante.tblEquipo_ID);
            return View(tblIntegrante);
        }

        // GET: Integrantes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblIntegrante tblIntegrante = db.tblIntegrante.Find(id);
            if (tblIntegrante == null)
            {
                return HttpNotFound();
            }
            return View(tblIntegrante);
        }

        // POST: Integrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblIntegrante tblIntegrante = db.tblIntegrante.Find(id);
            db.tblIntegrante.Remove(tblIntegrante);
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
