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
    public class CronogramaProyController : Controller
    {
        private investicEntities db = new investicEntities();


        // GET: tblCronogramaProys/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCronogramaProy tblCronogramaProy = db.tblCronogramaProy.Find(id);
            if (tblCronogramaProy == null)
            {
                return HttpNotFound();
            }
            return View(tblCronogramaProy);
        }

        // POST: tblCronogramaProys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblCronogramaProy_ID")] tblCronogramaProy tblCronogramaProy)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(tblCronogramaProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblCronogramaProy.tblCronogramaProy_ID });
            }
            return View(tblCronogramaProy);
        }

        // GET: tblFechaCronogramas/Create
        public ActionResult Create()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.tblCronogramaProy_ID = new SelectList(db.tblCronogramaProy, "tblCronogramaProy_ID", "tblCronogramaProy_ID");
            return View();
        }

        // POST: tblFechaCronogramas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblFechaCronograma_ID,tblCronogramaProy_ID,cro_Actividad,cro_FechaInicio,cro_FechaFin,cro_Indicador")] tblFechaCronograma tblFechaCronograma)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.tblFechaCronograma.Add(tblFechaCronograma);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblFechaCronograma.tblCronogramaProy_ID });
            }

            ViewBag.tblCronogramaProy_ID = new SelectList(db.tblCronogramaProy, "tblCronogramaProy_ID", "tblCronogramaProy_ID", tblFechaCronograma.tblCronogramaProy_ID);
            return View(tblFechaCronograma);
        }

        // GET: tblFechaCronogramas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFechaCronograma tblFechaCronograma = db.tblFechaCronograma.Find(id);
            if (tblFechaCronograma == null)
            {
                return HttpNotFound();
            }
            return View(tblFechaCronograma);
        }

        // POST: tblFechaCronogramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(long id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            tblFechaCronograma tblFechaCronograma = db.tblFechaCronograma.Find(id);
            db.tblFechaCronograma.Remove(tblFechaCronograma);
            long ID = tblFechaCronograma.tblCronogramaProy_ID;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = ID });
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
