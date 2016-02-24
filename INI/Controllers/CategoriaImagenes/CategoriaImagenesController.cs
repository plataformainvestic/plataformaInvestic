using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using System.IO;
using Microsoft.SqlServer;

namespace INI.Controllers.CategoriaImagenes
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class CategoriaImagenesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /CategoriaImagenes/
        public ActionResult Index()
        {
            return View(db.tblCategoriaImagenes.ToList());
        }

        // GET: /CategoriaImagenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaImagenes tblcategoriaimagenes = db.tblCategoriaImagenes.Find(id);
            if (tblcategoriaimagenes == null)
            {
                return HttpNotFound();
            }
            return View(tblcategoriaimagenes);
        }

        // GET: /CategoriaImagenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CategoriaImagenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cat_ID,cat_Nombre,cat_Imagen_Portada")] tblCategoriaImagenes tblcategoriaimagenes)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0)
                            continue;
                        
                        string folderPath = Server.MapPath("~/images/Gallery/Categorias/Portadas/");
                        string PathforDB = "/images/Gallery/Categorias/Portadas/";

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        string filename = string.Format("{0}-{1}",                          
                              DateTime.Now.ToString("ddMMyyyyHHmmss"),
                              Path.GetFileName(hpf.FileName));

                        string savedfileName = folderPath + filename;

                        hpf.SaveAs(savedfileName);

                        tblcategoriaimagenes.cat_Imagen_Portada = PathforDB + filename;
                    }

                    db.tblCategoriaImagenes.Add(tblcategoriaimagenes);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tblcategoriaimagenes);

            }
            catch (Exception)
            {
                return null;
            }
           
        }

        // GET: /CategoriaImagenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaImagenes tblcategoriaimagenes = db.tblCategoriaImagenes.Find(id);
            if (tblcategoriaimagenes == null)
            {
                return HttpNotFound();
            }
            return View(tblcategoriaimagenes);
        }

        // POST: /CategoriaImagenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cat_ID,cat_Nombre,cat_Imagen_Portada")] tblCategoriaImagenes tblcategoriaimagenes)
        {
            if (ModelState.IsValid)
            {

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    string folderPath = Server.MapPath("~/images/Gallery/Categorias/Portadas/");
                    string PathforDB = "/images/Gallery/Categorias/Portadas/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = string.Format("{0}-{1}",
                          DateTime.Now.ToString("ddMMyyyyHHmmss"),
                          Path.GetFileName(hpf.FileName));

                    string savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);

                    tblcategoriaimagenes.cat_Imagen_Portada = PathforDB + filename;
                }

                db.Entry(tblcategoriaimagenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblcategoriaimagenes);
        }

        // GET: /CategoriaImagenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoriaImagenes tblcategoriaimagenes = db.tblCategoriaImagenes.Find(id);
            if (tblcategoriaimagenes == null)
            {
                return HttpNotFound();
            }
            return View(tblcategoriaimagenes);
        }

        // POST: /CategoriaImagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tblCategoriaImagenes tblcategoriaimagenes = db.tblCategoriaImagenes.Find(id);
                db.tblCategoriaImagenes.Remove(tblcategoriaimagenes);
                db.SaveChanges();
                return RedirectToAction("Index");
               
            }
            catch (Exception e)
            {
                e.ToString();
                ViewBag.Message = "No se puede eliminar esta Categoría porque tiene imágenes asociadas.";
                return RedirectToAction("Index");
            }
           
           
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
