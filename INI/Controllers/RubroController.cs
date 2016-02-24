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
    public class RubroController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Rubro
        public ActionResult Index()
        {
            return View(db.tblRubro.ToList());
        }

        // GET: Rubro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRubro tblRubro = db.tblRubro.Find(id);
            if (tblRubro == null)
            {
                return HttpNotFound();
            }
            return View(tblRubro);
        }

        // GET: Rubro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rubro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Rubro,Descripcion")] tblRubro tblRubro)
        {
            if (ModelState.IsValid)
            {
                db.tblRubro.Add(tblRubro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblRubro);
        }

        // GET: Rubro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRubro tblRubro = db.tblRubro.Find(id);
            if (tblRubro == null)
            {
                return HttpNotFound();
            }
            return View(tblRubro);
        }

        // POST: Rubro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Rubro,Descripcion")] tblRubro tblRubro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRubro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblRubro);
        }

        // GET: Rubro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRubro tblRubro = db.tblRubro.Find(id);
            if (tblRubro == null)
            {
                return HttpNotFound();
            }
            return View(tblRubro);
        }

        // POST: Rubro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRubro tblRubro = db.tblRubro.Find(id);
            db.tblRubro.Remove(tblRubro);
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
