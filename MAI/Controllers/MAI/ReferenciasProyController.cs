using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.Proyectos
{
    [Authorize]
    public class ReferenciasProyController : Controller
    {
        private investicEntities db = new investicEntities();


        // GET: ReferenciasProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReferenciasProy tblReferenciasProy = db.tblReferenciasProy.Find(id);
            if (tblReferenciasProy == null)
            {
                return HttpNotFound();
            }
            return View(tblReferenciasProy);
        }

        // POST: ReferenciasProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblReferenciasProy_ID,refProy_referencias")] tblReferenciasProy tblReferenciasProy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblReferenciasProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblReferenciasProy.tblReferenciasProy_ID });
            }
            return View(tblReferenciasProy);
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
