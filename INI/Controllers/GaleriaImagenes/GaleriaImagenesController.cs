using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using INI.Models.DataBase;

namespace INI.Controllers.GaleriaImagenes
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class GaleriaImagenesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /GaleriaImagenes/
        public ActionResult Index()
        {
            var tblgaleriaimagenes = db.tblGaleriaImagenes.Include(t => t.tblCategoriaImagenes);
            return View(tblgaleriaimagenes.ToList());
        }
        [AllowAnonymous]
        public ActionResult getImagesByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblgaleriaimagenes = db.tblGaleriaImagenes.Where(t => t.cat_ID == id);

            if (tblgaleriaimagenes == null)
            {
                return HttpNotFound();
            }

            //return View(tblgaleriaimagenes.ToList());
            return PartialView("~/Views/Shared/_GaleriaImagenesRecuerdos.cshtml", tblgaleriaimagenes);
        }

        // GET: /GaleriaImagenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleriaImagenes tblgaleriaimagenes = db.tblGaleriaImagenes.Find(id);
            if (tblgaleriaimagenes == null)
            {
                return HttpNotFound();
            }
            return View(tblgaleriaimagenes);
        }

        // GET: /GaleriaImagenes/Create
        public ActionResult Create()
        {
            ViewBag.cat_ID = new SelectList(db.tblCategoriaImagenes, "cat_ID", "cat_Nombre");
            return View();
        }

        // POST: /GaleriaImagenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Imagen_ID,Imagen_titulo,Imagen_descripcion,Imagen_url,cat_ID")] tblGaleriaImagenes tblgaleriaimagenes)
        {
            if (ModelState.IsValid)
            {

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    string folderPath = Server.MapPath("~/images/Gallery/Recuerdos/");
                    string PathforDB = "/images/Gallery/Recuerdos/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = string.Format("{0}-{1}",
                          DateTime.Now.ToString("ddMMyyyyHHmmss"),
                          Path.GetFileName(hpf.FileName));

                    string savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);

                    tblgaleriaimagenes.Imagen_url = PathforDB + filename;
                    db.tblGaleriaImagenes.Add(tblgaleriaimagenes);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }

            ViewBag.cat_ID = new SelectList(db.tblCategoriaImagenes, "cat_ID", "cat_Nombre", tblgaleriaimagenes.cat_ID);
            return View(tblgaleriaimagenes);
        }

        // GET: /GaleriaImagenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleriaImagenes tblgaleriaimagenes = db.tblGaleriaImagenes.Find(id);
            if (tblgaleriaimagenes == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_ID = new SelectList(db.tblCategoriaImagenes, "cat_ID", "cat_Nombre", tblgaleriaimagenes.cat_ID);
            return View(tblgaleriaimagenes);
        }

        // POST: /GaleriaImagenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Imagen_ID,Imagen_titulo,Imagen_descripcion,Imagen_url,cat_ID")] tblGaleriaImagenes tblgaleriaimagenes)
        {
            if (ModelState.IsValid)
            {

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    string folderPath = Server.MapPath("~/images/Gallery/Recuerdos/");
                    string PathforDB = "/images/Gallery/Recuerdos/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = string.Format("{0}-{1}",
                          DateTime.Now.ToString("ddMMyyyyHHmmss"),
                          Path.GetFileName(hpf.FileName));

                    string savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);

                    tblgaleriaimagenes.Imagen_url = PathforDB + filename;
                }

                db.Entry(tblgaleriaimagenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_ID = new SelectList(db.tblCategoriaImagenes, "cat_ID", "cat_Nombre", tblgaleriaimagenes.cat_ID);
            return View(tblgaleriaimagenes);
        }

        // GET: /GaleriaImagenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleriaImagenes tblgaleriaimagenes = db.tblGaleriaImagenes.Find(id);
            if (tblgaleriaimagenes == null)
            {
                return HttpNotFound();
            }
            return View(tblgaleriaimagenes);
        }

        // POST: /GaleriaImagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGaleriaImagenes tblgaleriaimagenes = db.tblGaleriaImagenes.Find(id);
            db.tblGaleriaImagenes.Remove(tblgaleriaimagenes);
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
