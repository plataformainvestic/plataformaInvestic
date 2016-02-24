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
    public class TipoEstudioProyController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: TipoEstudioProy
        public ActionResult Index()
        {
            return View(db.tblTipoEstudioProy.ToList());
        }

        // GET: TipoEstudioProy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTipoEstudioProy tblTipoEstudioProy = db.tblTipoEstudioProy.Find(id);
            if (tblTipoEstudioProy == null)
            {
                return HttpNotFound();
            }
            return View(tblTipoEstudioProy);
        }

        // GET: TipoEstudioProy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEstudioProy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblTipoEstudioProy_ID,tipEst_nombre")] tblTipoEstudioProy tblTipoEstudioProy)
        {
            if (ModelState.IsValid)
            {
                db.tblTipoEstudioProy.Add(tblTipoEstudioProy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblTipoEstudioProy);
        }

        // GET: TipoEstudioProy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTipoEstudioProy tblTipoEstudioProy = db.tblTipoEstudioProy.Find(id);
            if (tblTipoEstudioProy == null)
            {
                return HttpNotFound();
            }
            return View(tblTipoEstudioProy);
        }

        // POST: TipoEstudioProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblTipoEstudioProy_ID,tipEst_nombre")] tblTipoEstudioProy tblTipoEstudioProy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTipoEstudioProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblTipoEstudioProy);
        }

        // GET: TipoEstudioProy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTipoEstudioProy tblTipoEstudioProy = db.tblTipoEstudioProy.Find(id);
            if (tblTipoEstudioProy == null)
            {
                return HttpNotFound();
            }
            return View(tblTipoEstudioProy);
        }

        // POST: TipoEstudioProy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTipoEstudioProy tblTipoEstudioProy = db.tblTipoEstudioProy.Find(id);
            db.tblTipoEstudioProy.Remove(tblTipoEstudioProy);
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
