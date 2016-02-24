using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Colecciones
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class ColeccionesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /Colecciones/
        public ActionResult Index()
        {
            return View(db.tblColecciones.ToList());
        }


        public ActionResult RecursosColeccion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblRecursosEducativos = db.tblRecursosEducativos.Where(t => t.id_coleccion == id);

            if (tblRecursosEducativos == null)
            {
                return HttpNotFound();
            }

            //return View(tblgaleriaimagenes.ToList());
            return PartialView("~/Views/RecursosEducativos/_Busqueda.cshtml", tblRecursosEducativos);
        }

        // GET: /Colecciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblColecciones tblcolecciones = db.tblColecciones.Find(id);
            if (tblcolecciones == null)
            {
                return HttpNotFound();
            }
            return View(tblcolecciones);
        }

        // GET: /Colecciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Colecciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id_coleccion,nom_coleccion,desc_coleccion")] tblColecciones tblcolecciones)
        {
            if (ModelState.IsValid)
            {
                db.tblColecciones.Add(tblcolecciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblcolecciones);
        }

        // GET: /Colecciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblColecciones tblcolecciones = db.tblColecciones.Find(id);
            if (tblcolecciones == null)
            {
                return HttpNotFound();
            }
            return View(tblcolecciones);
        }

        // POST: /Colecciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id_coleccion,nom_coleccion,desc_coleccion")] tblColecciones tblcolecciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblcolecciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblcolecciones);
        }

        // GET: /Colecciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblColecciones tblcolecciones = db.tblColecciones.Find(id);
            if (tblcolecciones == null)
            {
                return HttpNotFound();
            }
            return View(tblcolecciones);
        }

        // POST: /Colecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblColecciones tblcolecciones = db.tblColecciones.Find(id);
            db.tblColecciones.Remove(tblcolecciones);
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
