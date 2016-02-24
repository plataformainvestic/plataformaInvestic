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
    public class ParadigmaMetodologicoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: ParadigmaMetodologico
        public ActionResult Index()
        {
            return View(db.tblParadigmaMetodologico.ToList());
        }

        // GET: ParadigmaMetodologico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaMetodologico tblParadigmaMetodologico = db.tblParadigmaMetodologico.Find(id);
            if (tblParadigmaMetodologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaMetodologico);
        }

        // GET: ParadigmaMetodologico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParadigmaMetodologico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblParadigmaMetodologico_ID,parMet_nombre")] tblParadigmaMetodologico tblParadigmaMetodologico)
        {
            if (ModelState.IsValid)
            {
                db.tblParadigmaMetodologico.Add(tblParadigmaMetodologico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblParadigmaMetodologico);
        }

        // GET: ParadigmaMetodologico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaMetodologico tblParadigmaMetodologico = db.tblParadigmaMetodologico.Find(id);
            if (tblParadigmaMetodologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaMetodologico);
        }

        // POST: ParadigmaMetodologico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblParadigmaMetodologico_ID,parMet_nombre")] tblParadigmaMetodologico tblParadigmaMetodologico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblParadigmaMetodologico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblParadigmaMetodologico);
        }

        // GET: ParadigmaMetodologico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaMetodologico tblParadigmaMetodologico = db.tblParadigmaMetodologico.Find(id);
            if (tblParadigmaMetodologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaMetodologico);
        }

        // POST: ParadigmaMetodologico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblParadigmaMetodologico tblParadigmaMetodologico = db.tblParadigmaMetodologico.Find(id);
            db.tblParadigmaMetodologico.Remove(tblParadigmaMetodologico);
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
