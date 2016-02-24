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
    public class ParadigmaEpistemologicoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: ParadigmaEpistemologico
        public ActionResult Index()
        {
            return View(db.tblParadigmaEpistemologico.ToList());
        }

        // GET: ParadigmaEpistemologico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaEpistemologico tblParadigmaEpistemologico = db.tblParadigmaEpistemologico.Find(id);
            if (tblParadigmaEpistemologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaEpistemologico);
        }

        // GET: ParadigmaEpistemologico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParadigmaEpistemologico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblParadigmaEpistemologico_ID,parEpi_nombre")] tblParadigmaEpistemologico tblParadigmaEpistemologico)
        {
            if (ModelState.IsValid)
            {
                db.tblParadigmaEpistemologico.Add(tblParadigmaEpistemologico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblParadigmaEpistemologico);
        }

        // GET: ParadigmaEpistemologico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaEpistemologico tblParadigmaEpistemologico = db.tblParadigmaEpistemologico.Find(id);
            if (tblParadigmaEpistemologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaEpistemologico);
        }

        // POST: ParadigmaEpistemologico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblParadigmaEpistemologico_ID,parEpi_nombre")] tblParadigmaEpistemologico tblParadigmaEpistemologico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblParadigmaEpistemologico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblParadigmaEpistemologico);
        }

        // GET: ParadigmaEpistemologico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParadigmaEpistemologico tblParadigmaEpistemologico = db.tblParadigmaEpistemologico.Find(id);
            if (tblParadigmaEpistemologico == null)
            {
                return HttpNotFound();
            }
            return View(tblParadigmaEpistemologico);
        }

        // POST: ParadigmaEpistemologico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblParadigmaEpistemologico tblParadigmaEpistemologico = db.tblParadigmaEpistemologico.Find(id);
            db.tblParadigmaEpistemologico.Remove(tblParadigmaEpistemologico);
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
