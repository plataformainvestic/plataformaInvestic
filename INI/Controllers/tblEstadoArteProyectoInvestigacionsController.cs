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
    public class tblEstadoArteProyectoInvestigacionsController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblEstadoArteProyectoInvestigacions
        public ActionResult Index()
        {
            var tblEstadoArteProyectoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Include(t => t.tblGrupoInvestigacion);
            return View(tblEstadoArteProyectoInvestigacion.ToList());
        }

        // GET: tblEstadoArteProyectoInvestigacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Find(id);
            if (tblEstadoArteProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadoArteProyectoInvestigacion);
        }

        // GET: tblEstadoArteProyectoInvestigacions/Create
        public ActionResult Create()
        {
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo");
            return View();
        }

        // POST: tblEstadoArteProyectoInvestigacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idGrupoInvestigacion,TemaInvestigacion,MapaConceptual")] tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.tblEstadoArteProyectoInvestigacion.Add(tblEstadoArteProyectoInvestigacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblEstadoArteProyectoInvestigacion.idGrupoInvestigacion);
            return View(tblEstadoArteProyectoInvestigacion);
        }

        // GET: tblEstadoArteProyectoInvestigacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Find(id);
            if (tblEstadoArteProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblEstadoArteProyectoInvestigacion.idGrupoInvestigacion);
            return View(tblEstadoArteProyectoInvestigacion);
        }

        // POST: tblEstadoArteProyectoInvestigacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idGrupoInvestigacion,TemaInvestigacion,MapaConceptual")] tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEstadoArteProyectoInvestigacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblEstadoArteProyectoInvestigacion.idGrupoInvestigacion);
            return View(tblEstadoArteProyectoInvestigacion);
        }

        // GET: tblEstadoArteProyectoInvestigacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Find(id);
            if (tblEstadoArteProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblEstadoArteProyectoInvestigacion);
        }

        // POST: tblEstadoArteProyectoInvestigacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEstadoArteProyectoInvestigacion tblEstadoArteProyectoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Find(id);
            db.tblEstadoArteProyectoInvestigacion.Remove(tblEstadoArteProyectoInvestigacion);
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
