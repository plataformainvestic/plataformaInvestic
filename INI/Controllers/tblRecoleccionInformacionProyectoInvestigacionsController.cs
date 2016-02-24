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
    public class tblRecoleccionInformacionProyectoInvestigacionsController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblRecoleccionInformacionProyectoInvestigacions
        public ActionResult Index()
        {
            var tblRecoleccionInformacionProyectoInvestigacion = db.tblRecoleccionInformacionProyectoInvestigacion.Include(t => t.tblGrupoInvestigacion).Include(t => t.tblHerramientasRecoleccionInformacion);
            return View(tblRecoleccionInformacionProyectoInvestigacion.ToList());
        }

        // GET: tblRecoleccionInformacionProyectoInvestigacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion = db.tblRecoleccionInformacionProyectoInvestigacion.Find(id);
            if (tblRecoleccionInformacionProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblRecoleccionInformacionProyectoInvestigacion);
        }

        // GET: tblRecoleccionInformacionProyectoInvestigacions/Create
        public ActionResult Create()
        {
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo");
            ViewBag.idInstrumento = new SelectList(db.tblHerramientasRecoleccionInformacion, "id", "HerramientaRecoleccion");
            return View();
        }

        // POST: tblRecoleccionInformacionProyectoInvestigacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idGrupoInvestigacion,idInstrumento,Evidencia,Descripcion")] tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.tblRecoleccionInformacionProyectoInvestigacion.Add(tblRecoleccionInformacionProyectoInvestigacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblRecoleccionInformacionProyectoInvestigacion.idGrupoInvestigacion);
            ViewBag.idInstrumento = new SelectList(db.tblHerramientasRecoleccionInformacion, "id", "HerramientaRecoleccion", tblRecoleccionInformacionProyectoInvestigacion.idInstrumento);
            return View(tblRecoleccionInformacionProyectoInvestigacion);
        }

        // GET: tblRecoleccionInformacionProyectoInvestigacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion = db.tblRecoleccionInformacionProyectoInvestigacion.Find(id);
            if (tblRecoleccionInformacionProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblRecoleccionInformacionProyectoInvestigacion.idGrupoInvestigacion);
            ViewBag.idInstrumento = new SelectList(db.tblHerramientasRecoleccionInformacion, "id", "HerramientaRecoleccion", tblRecoleccionInformacionProyectoInvestigacion.idInstrumento);
            return View(tblRecoleccionInformacionProyectoInvestigacion);
        }

        // POST: tblRecoleccionInformacionProyectoInvestigacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idGrupoInvestigacion,idInstrumento,Evidencia,Descripcion")] tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRecoleccionInformacionProyectoInvestigacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblRecoleccionInformacionProyectoInvestigacion.idGrupoInvestigacion);
            ViewBag.idInstrumento = new SelectList(db.tblHerramientasRecoleccionInformacion, "id", "HerramientaRecoleccion", tblRecoleccionInformacionProyectoInvestigacion.idInstrumento);
            return View(tblRecoleccionInformacionProyectoInvestigacion);
        }

        // GET: tblRecoleccionInformacionProyectoInvestigacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion = db.tblRecoleccionInformacionProyectoInvestigacion.Find(id);
            if (tblRecoleccionInformacionProyectoInvestigacion == null)
            {
                return HttpNotFound();
            }
            return View(tblRecoleccionInformacionProyectoInvestigacion);
        }

        // POST: tblRecoleccionInformacionProyectoInvestigacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRecoleccionInformacionProyectoInvestigacion tblRecoleccionInformacionProyectoInvestigacion = db.tblRecoleccionInformacionProyectoInvestigacion.Find(id);
            db.tblRecoleccionInformacionProyectoInvestigacion.Remove(tblRecoleccionInformacionProyectoInvestigacion);
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
