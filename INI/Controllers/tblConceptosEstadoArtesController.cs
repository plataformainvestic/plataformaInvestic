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
    public class tblConceptosEstadoArtesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblConceptosEstadoArtes
        public ActionResult Index()
        {
            var tblConceptosEstadoArte = db.tblConceptosEstadoArte.Include(t => t.tblEstadoArteProyectoInvestigacion);
            return View(tblConceptosEstadoArte.ToList());
        }

        // GET: tblConceptosEstadoArtes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConceptosEstadoArte tblConceptosEstadoArte = db.tblConceptosEstadoArte.Find(id);
            if (tblConceptosEstadoArte == null)
            {
                return HttpNotFound();
            }
            return View(tblConceptosEstadoArte);
        }

        // GET: tblConceptosEstadoArtes/Create
        public ActionResult Create()
        {
            ViewBag.idEstadoArte = new SelectList(db.tblEstadoArteProyectoInvestigacion, "id", "TemaInvestigacion");
            return View();
        }

        // POST: tblConceptosEstadoArtes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idEstadoArte,Autor,Publicacion,Texto")] tblConceptosEstadoArte tblConceptosEstadoArte)
        {
            if (ModelState.IsValid)
            {
                db.tblConceptosEstadoArte.Add(tblConceptosEstadoArte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstadoArte = new SelectList(db.tblEstadoArteProyectoInvestigacion, "id", "TemaInvestigacion", tblConceptosEstadoArte.idEstadoArte);
            return View(tblConceptosEstadoArte);
        }

        // GET: tblConceptosEstadoArtes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConceptosEstadoArte tblConceptosEstadoArte = db.tblConceptosEstadoArte.Find(id);
            if (tblConceptosEstadoArte == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstadoArte = new SelectList(db.tblEstadoArteProyectoInvestigacion, "id", "TemaInvestigacion", tblConceptosEstadoArte.idEstadoArte);
            return View(tblConceptosEstadoArte);
        }

        // POST: tblConceptosEstadoArtes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idEstadoArte,Autor,Publicacion,Texto")] tblConceptosEstadoArte tblConceptosEstadoArte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblConceptosEstadoArte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstadoArte = new SelectList(db.tblEstadoArteProyectoInvestigacion, "id", "TemaInvestigacion", tblConceptosEstadoArte.idEstadoArte);
            return View(tblConceptosEstadoArte);
        }

        // GET: tblConceptosEstadoArtes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConceptosEstadoArte tblConceptosEstadoArte = db.tblConceptosEstadoArte.Find(id);
            if (tblConceptosEstadoArte == null)
            {
                return HttpNotFound();
            }
            return View(tblConceptosEstadoArte);
        }

        // POST: tblConceptosEstadoArtes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblConceptosEstadoArte tblConceptosEstadoArte = db.tblConceptosEstadoArte.Find(id);
            db.tblConceptosEstadoArte.Remove(tblConceptosEstadoArte);
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
