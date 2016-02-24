using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEG.Models.DataBase;
using System.Web.Security;

namespace SEG.Controllers
{
    public class equiposController : Controller
    {
        private Entities db = new Entities();

        // GET: equipos
        public ActionResult Index()
        {
            var equipos = db.equipos.Include(e => e.AspNetUser);

            
            return View(equipos.ToList());
        }


        public JsonResult ListaNombre(string Cedula)
        {
            var nombres = (from n in db.AspNetUsers where n.Cedula.Equals(Cedula) select n.Nombres);

            return Json(new SelectList(nombres.ToArray(),JsonRequestBehavior.AllowGet));       
        }


        // GET: equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipos equipos = db.equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: equipos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName");


            List<AspNetUser> usuarios = db.AspNetUsers.Where(x=>x.Cargo.Equals("coordinador")).ToList();

            ViewBag.Nombre_Coordinador = new SelectList(usuarios, "Id", "LastName");
            
            return View();
        }

        // POST: equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Equipo,Nombre_Equipo,Id_Coordinador,Nombre_Coordinador")] equipos equipos)
        {
            if (ModelState.IsValid)
            {
                equipos.Id_Coordinador = equipos.Nombre_Coordinador;
                equipos.Nombre_Coordinador = (from n in db.AspNetUsers where n.Id.Equals(equipos.Nombre_Coordinador) select n.Nombres).SingleOrDefault();
                db.equipos.Add(equipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // GET: equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipos equipos = db.equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // POST: equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Equipo,Nombre_Equipo,Id_Coordinador,Nombre_Coordinador")] equipos equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Coordinador = new SelectList(db.AspNetUsers, "Id", "UserName", equipos.Id_Coordinador);
            return View(equipos);
        }

        // GET: equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipos equipos = db.equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipos equipos = db.equipos.Find(id);
            db.equipos.Remove(equipos);
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
