using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Metas
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class MetasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /Metas/
        public ActionResult Index()
        {
            var tblmeta = db.tblMeta.Include(t => t.tblMunicipios);
            return View(tblmeta.ToList());
        }

        // GET: /Metas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMeta tblmeta = db.tblMeta.Find(id);
            if (tblmeta == null)
            {
                return HttpNotFound();
            }
            return View(tblmeta);
        }

        // GET: /Metas/Create
        public ActionResult Create()
        {
            ViewBag.idMunicipio = new SelectList(db.tblMunicipios, "idMunicipio", "NombreMunicipio");
            return View();
        }

        // POST: /Metas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tblMeta_ID,met_grupoInvestigacionEstudiantil,met_estudianteInvestigando,met_grupoInvestigacionDocente,met_docenteInvestigando,met_establecimientosEducativos,met_padresFormados40,met_estudiantesFormados40,met_docentesFormados120,met_estudiantesFormados180,met_docentesFormados180,idMunicipio")] tblMeta tblmeta)
        {
            if (ModelState.IsValid)
            {
                db.tblMeta.Add(tblmeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idMunicipio = new SelectList(db.tblMunicipios, "idMunicipio", "NombreMunicipio", tblmeta.idMunicipio);
            return View(tblmeta);
        }

        // GET: /Metas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMeta tblmeta = db.tblMeta.Find(id);
            if (tblmeta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMunicipio = new SelectList(db.tblMunicipios, "idMunicipio", "NombreMunicipio", tblmeta.idMunicipio);
            return View(tblmeta);
        }

        // POST: /Metas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tblMeta_ID,met_grupoInvestigacionEstudiantil,met_estudianteInvestigando,met_grupoInvestigacionDocente,met_docenteInvestigando,met_establecimientosEducativos,met_padresFormados40,met_estudiantesFormados40,met_docentesFormados120,met_estudiantesFormados180,met_docentesFormados180,idMunicipio")] tblMeta tblmeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblmeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMunicipio = new SelectList(db.tblMunicipios, "idMunicipio", "NombreMunicipio", tblmeta.idMunicipio);
            return View(tblmeta);
        }

        // GET: /Metas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMeta tblmeta = db.tblMeta.Find(id);
            if (tblmeta == null)
            {
                return HttpNotFound();
            }
            return View(tblmeta);
        }

        // POST: /Metas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblMeta tblmeta = db.tblMeta.Find(id);
            db.tblMeta.Remove(tblmeta);
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
