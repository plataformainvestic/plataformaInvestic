using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.Asesores
{
    [Authorize]
    public class AsesorZonasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: AsesorZonas
        public ActionResult Index()
        {
            //return View(db.tblAsesorZona.Where(estaActivo=> estaActivo.estaActivo==true).ToList());
            return View(db.tblAsesorZona.ToList());
        }

        // GET: AsesorZonas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAsesorZona tblAsesorZona = db.tblAsesorZona.Find(id);
            if (tblAsesorZona == null)
            {
                return HttpNotFound();
            }
            return View(tblAsesorZona);
        }

        // GET: AsesorZonas/Create
        public ActionResult Create()
        {
            
            ViewBag.tblMunicipio_ID = new SelectList(db.tblMunicipios.Where(m => m.idDepartamento == "52"), "idMunicipio", "NombreMunicipio");
            return View();
        }

        // POST: AsesorZonas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblAsesorZona_ID,aseZon_nombre,aseZon_apellido,aseZon_correo,aseZon_telefono,tblEquipo_ID,tblMunicipio_ID,estaActivo")] tblAsesorZona tblAsesorZona)
        {
            
            tblAsesorZona.tblEquipo_ID = 10010; //Este Id debe estar en la tabla tblEquipos
            if (ModelState.IsValid)
            {
                ViewBag.tblMunicipio_ID = new SelectList(db.tblMunicipios.Where(m => m.idDepartamento == "52" ), "tblMunicipio_ID", "NombreMunicipio");
                tblAsesorZona.estaActivo = true;
                db.tblAsesorZona.Add(tblAsesorZona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAsesorZona);
        }

        // GET: AsesorZonas/Edit/5
  
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAsesorZona tblAsesorZona = db.tblAsesorZona.Find(id);
            if (tblAsesorZona == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblMunicipio_ID = new SelectList(db.tblMunicipios.Where(m => m.idDepartamento == "52"), "idMunicipio", "NombreMunicipio",tblAsesorZona.tblMunicipio_ID);
            return View(tblAsesorZona);
        }

        // POST: AsesorZonas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblAsesorZona_ID,aseZon_nombre,aseZon_apellido,aseZon_correo,aseZon_telefono,tblEquipo_ID,tblMunicipio_ID,estaActivo")] tblAsesorZona tblAsesorZona)
        {
            tblAsesorZona.tblEquipo_ID = 10010; //Este Id debe estar en la tabla tblEquipos
            if (ModelState.IsValid)
            {
                ViewBag.tblMunicipio_ID = new SelectList(db.tblMunicipios.Where(m => m.idDepartamento == "52"), "tblMunicipio_ID", "NombreMunicipio");
                db.Entry(tblAsesorZona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAsesorZona);
        }

        // GET: AsesorZonas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAsesorZona tblAsesorZona = db.tblAsesorZona.Find(id);
            if (tblAsesorZona == null)
            {
                return HttpNotFound();
            }
            return View(tblAsesorZona);
        }

        // POST: AsesorZonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblAsesorZona tblAsesorZona = db.tblAsesorZona.Find(id);
            db.tblAsesorZona.Remove(tblAsesorZona);
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
