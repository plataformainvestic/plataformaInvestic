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

namespace INI.Controllers.RevistaDigital
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class SubCategoriaRevistasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: SubCategoriaRevistas
        public ActionResult Index()
        {
            var subCategoriaRevista = db.SubCategoriaRevista.Include(s => s.CategoriaRevista);
            return View(subCategoriaRevista.ToList());
        }

        // GET: SubCategoriaRevistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRevista subCategoriaRevista = db.SubCategoriaRevista.Find(id);
            if (subCategoriaRevista == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaRevista);
        }

        // GET: SubCategoriaRevistas/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.CategoriaRevista, "id", "nombre");
            return View();
        }

        // POST: SubCategoriaRevistas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,urlimg,id_categoria")] SubCategoriaRevista subCategoriaRevista)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string subcategoria = subCategoriaRevista.nombre;
                string categoria = db.CategoriaRevista.Where(m=>m.id==subCategoriaRevista.id_categoria).First().nombre;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    folderPath = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria+"/");
                    PathforDB = "/images/Revista/" + categoria + "/" + subcategoria+"/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    subCategoriaRevista.urlimg = PathforDB + filename;

                }
                db.SubCategoriaRevista.Add(subCategoriaRevista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.CategoriaRevista, "id", "nombre", subCategoriaRevista.id_categoria);
            return View(subCategoriaRevista);
        }

        // GET: SubCategoriaRevistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRevista subCategoriaRevista = db.SubCategoriaRevista.Find(id);
            if (subCategoriaRevista == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.CategoriaRevista, "id", "nombre", subCategoriaRevista.id_categoria);
            return View(subCategoriaRevista);
        }

        // POST: SubCategoriaRevistas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,urlimg,id_categoria")] SubCategoriaRevista subCategoriaRevista)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string folderPathOld = "";
                string filePath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string subcategoria = subCategoriaRevista.nombre;
                string categoria = db.CategoriaRevista.Where(m=>m.id==subCategoriaRevista.id_categoria).First().nombre;
                string oldURL = (from m in db.SubCategoriaRevista where m.id == subCategoriaRevista.id select m.urlimg).FirstOrDefault();
                string oldCategoria = (from m in db.SubCategoriaRevista where m.id == subCategoriaRevista.id select m.CategoriaRevista.nombre).FirstOrDefault();
                string oldSubCategoria = (from m in db.SubCategoriaRevista where m.id == subCategoriaRevista.id select m.nombre).FirstOrDefault();

                folderPath = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria + "/");
                folderPathOld = Server.MapPath("~/images/Revista/" + oldCategoria + "/" + oldSubCategoria + "/");
                filePath = Server.MapPath("~" + oldURL);
                PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/";
                filePath = Server.MapPath("~" + oldURL);

                if (oldCategoria != categoria )
                {
                    
                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);

                    foreach (var item2 in db.Revista.Where(m=>m.id_SubCategoria==subCategoriaRevista.id))
                    {
                        string edicion = item2.title;
                        item2.urlPdf = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlPdf);
                        item2.urlfront = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);

                        db.Entry(item2).State = EntityState.Modified;
                    }
                    oldCategoria = categoria;
                    
                    folderPathOld = Server.MapPath("~/images/Revista/" + oldCategoria + "/" + oldSubCategoria + "/");
                    filePath = Server.MapPath("~" + oldURL);
                    PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/";                    
                    
                }
                else if (oldSubCategoria != subcategoria)
                {
                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);
                    foreach (var item2 in db.Revista.Where(m => m.id_SubCategoria == subCategoriaRevista.id))
                    {
                        string edicion = item2.title;
                        item2.urlPdf = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlPdf);
                        item2.urlfront = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);

                        db.Entry(item2).State = EntityState.Modified;
                    }
                    filePath = Server.MapPath("~" + oldURL);
                }
                subCategoriaRevista.urlimg = oldURL;
                
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;                    
                                    
                    if (!String.IsNullOrEmpty(oldURL))
                    {
                     if(System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                    }
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    subCategoriaRevista.urlimg = PathforDB + filename;

                }
                
                db.Entry(subCategoriaRevista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.CategoriaRevista, "id", "nombre", subCategoriaRevista.id_categoria);
            return View(subCategoriaRevista);
        }

        // GET: SubCategoriaRevistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRevista subCategoriaRevista = db.SubCategoriaRevista.Find(id);
            if (subCategoriaRevista == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaRevista);
        }

        // POST: SubCategoriaRevistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoriaRevista subCategoriaRevista = db.SubCategoriaRevista.Find(id);
            string subcategoria = subCategoriaRevista.nombre;
            string categoria = db.CategoriaRevista.Where(m => m.id == subCategoriaRevista.id_categoria).First().nombre;
            db.Revista.RemoveRange(subCategoriaRevista.Revista);
            db.SaveChanges();
            db.SubCategoriaRevista.Remove(subCategoriaRevista);
            db.SaveChanges();

            String folderPath = Server.MapPath("~/images/Revista/" + categoria+"/"+subcategoria+"/");
            try
            {
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true); 
            }
            catch
            {

            }            
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
