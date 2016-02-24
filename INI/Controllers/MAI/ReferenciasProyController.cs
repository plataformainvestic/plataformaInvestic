using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Proyectos
{
    //[Authorize(Roles = "Administrator,Maestro")]
    [Authorize]
    public class ReferenciasProyController : Controller
    {
        private investicEntities db = new investicEntities();


        // GET: ReferenciasProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
