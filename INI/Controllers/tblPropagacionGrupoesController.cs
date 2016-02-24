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
    public class tblPropagacionGrupoesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tblPropagacionGrupoes
        public ActionResult Index()
        {
            var tblPropagacionGrupo = db.tblPropagacionGrupo.Include(t => t.tblGrupoInvestigacion).Include(t => t.tblTipoFeria);
            return View(tblPropagacionGrupo.ToList());
        }

        // GET: tblPropagacionGrupoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPropagacionGrupo tblPropagacionGrupo = db.tblPropagacionGrupo.Find(id);
            if (tblPropagacionGrupo == null)
            {
                return HttpNotFound();
            }
            return View(tblPropagacionGrupo);
        }

        // GET: tblPropagacionGrupoes/Create
        public ActionResult Create()
        {
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo");
            ViewBag.idTipoFeria = new SelectList(db.tblTipoFeria, "id", "TipoFeria");
            return View();
        }

        // POST: tblPropagacionGrupoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idGrupoInvestigacion,idTipoFeria,Archivo,Descripcion")] tblPropagacionGrupo tblPropagacionGrupo)
        {
            if (ModelState.IsValid)
            {
                db.tblPropagacionGrupo.Add(tblPropagacionGrupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblPropagacionGrupo.idGrupoInvestigacion);
            ViewBag.idTipoFeria = new SelectList(db.tblTipoFeria, "id", "TipoFeria", tblPropagacionGrupo.idTipoFeria);
            return View(tblPropagacionGrupo);
        }

        // GET: tblPropagacionGrupoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPropagacionGrupo tblPropagacionGrupo = db.tblPropagacionGrupo.Find(id);
            if (tblPropagacionGrupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblPropagacionGrupo.idGrupoInvestigacion);
            ViewBag.idTipoFeria = new SelectList(db.tblTipoFeria, "id", "TipoFeria", tblPropagacionGrupo.idTipoFeria);
            return View(tblPropagacionGrupo);
        }

        // POST: tblPropagacionGrupoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idGrupoInvestigacion,idTipoFeria,Archivo,Descripcion")] tblPropagacionGrupo tblPropagacionGrupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPropagacionGrupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupoInvestigacion = new SelectList(db.tblGrupoInvestigacion, "id", "Codigo", tblPropagacionGrupo.idGrupoInvestigacion);
            ViewBag.idTipoFeria = new SelectList(db.tblTipoFeria, "id", "TipoFeria", tblPropagacionGrupo.idTipoFeria);
            return View(tblPropagacionGrupo);
        }

        // GET: tblPropagacionGrupoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPropagacionGrupo tblPropagacionGrupo = db.tblPropagacionGrupo.Find(id);
            if (tblPropagacionGrupo == null)
            {
                return HttpNotFound();
            }
            return View(tblPropagacionGrupo);
        }

        // POST: tblPropagacionGrupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPropagacionGrupo tblPropagacionGrupo = db.tblPropagacionGrupo.Find(id);
            db.tblPropagacionGrupo.Remove(tblPropagacionGrupo);
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
