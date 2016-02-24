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
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class TutoresController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /Tutores/
        public ActionResult Index()
        {

            //var tbltutorzona = db.tblTutorZona.Include(t => t.tblInstitucion).Where(estaActivo => estaActivo.estaActivo == true).ToList();
            var tbltutorzona = db.tblTutorZona.Include(t => t.tblInstitucion).ToList();
            return View(tbltutorzona.ToList());
        }

        // GET: /Tutores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTutorZona tbltutorzona = db.tblTutorZona.Find(id);
            if (tbltutorzona == null)
            {
                return HttpNotFound();
            }
            return View(tbltutorzona);
        }

        // GET: /Tutores/Create
        public ActionResult Create()
        {
            ViewBag.tblInstitucionEducativa_ID = new SelectList(db.tblInstitucion, "id", "CodigoDane");
            return View();
        }

        // POST: /Tutores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "tblTutorZona_ID,tutZon_nombre,tutZon_apellido,tutZon_correo,tutZon_telefono,tblInstitucionEducativa_ID,estaActivo")] tblTutorZona tbltutorzona)
        {
            if (ModelState.IsValid)
            {
                tbltutorzona.estaActivo = true;
                db.tblTutorZona.Add(tbltutorzona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblInstitucionEducativa_ID = new SelectList(db.tblInstitucion, "id", "CodigoDane", tbltutorzona.tblInstitucionEducativa_ID);
            return View(tbltutorzona);
        }

        // GET: /Tutores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTutorZona tbltutorzona = db.tblTutorZona.Find(id);
            if (tbltutorzona == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblInstitucionEducativa_ID = new SelectList(db.tblInstitucion, "id", "CodigoDane", tbltutorzona.tblInstitucionEducativa_ID);
            return View(tbltutorzona);
        }

        // POST: /Tutores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "tblTutorZona_ID,tutZon_nombre,tutZon_apellido,tutZon_correo,tutZon_telefono,tblInstitucionEducativa_ID,estaActivo")] tblTutorZona tbltutorzona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbltutorzona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblInstitucionEducativa_ID = new SelectList(db.tblInstitucion, "id", "CodigoDane", tbltutorzona.tblInstitucionEducativa_ID);
            return View(tbltutorzona);
        }

        // GET: /Tutores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTutorZona tbltutorzona = db.tblTutorZona.Find(id);
            if (tbltutorzona == null)
            {
                return HttpNotFound();
            }
            return View(tbltutorzona);
        }

        // POST: /Tutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblTutorZona tbltutorzona = db.tblTutorZona.Find(id);
            db.tblTutorZona.Remove(tbltutorzona);
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
