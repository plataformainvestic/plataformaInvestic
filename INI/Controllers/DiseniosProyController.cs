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
    //[Authorize(Roles = "Administrator,Maestro,Editor")]
    [Authorize]
    public class DiseniosProyController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: DiseniosProy
        public ActionResult Index()
        {
            return View(db.tblDiseniosProy.ToList());
        }

        // GET: DiseniosProy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiseniosProy tblDiseniosProy = db.tblDiseniosProy.Find(id);
            if (tblDiseniosProy == null)
            {
                return HttpNotFound();
            }
            return View(tblDiseniosProy);
        }

        // GET: DiseniosProy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiseniosProy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblDiseniosProy_ID,disProy_nombre")] tblDiseniosProy tblDiseniosProy)
        {
            if (ModelState.IsValid)
            {
                db.tblDiseniosProy.Add(tblDiseniosProy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDiseniosProy);
        }

        // GET: DiseniosProy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiseniosProy tblDiseniosProy = db.tblDiseniosProy.Find(id);
            if (tblDiseniosProy == null)
            {
                return HttpNotFound();
            }
            return View(tblDiseniosProy);
        }

        // POST: DiseniosProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblDiseniosProy_ID,disProy_nombre")] tblDiseniosProy tblDiseniosProy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDiseniosProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDiseniosProy);
        }

        // GET: DiseniosProy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiseniosProy tblDiseniosProy = db.tblDiseniosProy.Find(id);
            if (tblDiseniosProy == null)
            {
                return HttpNotFound();
            }
            return View(tblDiseniosProy);
        }

        // POST: DiseniosProy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDiseniosProy tblDiseniosProy = db.tblDiseniosProy.Find(id);
            db.tblDiseniosProy.Remove(tblDiseniosProy);
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
