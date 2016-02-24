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
    public class CategoriaRevistasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: CategoriaRevistas
        public ActionResult Index()
        {
            return View(db.CategoriaRevista.ToList());
        }

        // GET: CategoriaRevistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRevista categoriaRevista = db.CategoriaRevista.Find(id);
            if (categoriaRevista == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRevista);
        }

        // GET: CategoriaRevistas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaRevistas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,urlimg")] CategoriaRevista categoriaRevista)
        {
            if (ModelState.IsValid)
            {
            
                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string categoria = categoriaRevista.nombre;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                   
                    folderPath = Server.MapPath("~/images/Revista/"+categoria+"/");
                    PathforDB = "/images/Revista/"+categoria+"/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    categoriaRevista.urlimg = PathforDB + filename;                    
                   
                }
                
                db.CategoriaRevista.Add(categoriaRevista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaRevista);
        }

        
        // GET: CategoriaRevistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRevista categoriaRevista = db.CategoriaRevista.Find(id);
            if (categoriaRevista == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRevista);
        }

        // POST: CategoriaRevistas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "id,nombre,urlimg")] CategoriaRevista categoriaRevista)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string folderPathOld = "";
                string filePath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string categoria = categoriaRevista.nombre;
                string oldURL = (from m in db.CategoriaRevista where m.id == categoriaRevista.id select m.urlimg).FirstOrDefault();
                string oldCategoria = (from m in db.CategoriaRevista where m.id == categoriaRevista.id select m.nombre).FirstOrDefault();
                folderPath = Server.MapPath("~/images/Revista/" + categoria + "/");
                folderPathOld = Server.MapPath("~/images/Revista/" + oldCategoria + "/");
                filePath = Server.MapPath("~" +oldURL);
                PathforDB = "/images/Revista/" + categoria + "/";


                if (oldCategoria != categoria)
                {
                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);
                    foreach (var item in db.SubCategoriaRevista.Where(m=>m.id_categoria==categoriaRevista.id))
                    {
                        string subcategoria = item.nombre;
                        item.urlimg = "/images/Revista/" + categoria + "/" + subcategoria + "/"+Path.GetFileName(item.urlimg);
                        foreach (var item2 in item.Revista)   
                        {
                            string edicion = item2.title;
                            item2.urlPdf = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlPdf);
                            item2.urlfront = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);

                            db.Entry(item2).State = EntityState.Modified;
                        }
                        db.Entry(item).State = EntityState.Modified;
                    }

                    filePath = Server.MapPath("~" + oldURL);
                }
                categoriaRevista.urlimg = oldURL;
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    
                    
                    //Directory.Delete(folderPath, true);                    
                    if (!String.IsNullOrEmpty(oldURL))
                    {
                        if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);                        

                    }
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    categoriaRevista.urlimg = PathforDB + filename;

                }
                db.Entry(categoriaRevista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaRevista);
        }

       
        // GET: CategoriaRevistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRevista categoriaRevista = db.CategoriaRevista.Find(id);
            if (categoriaRevista == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRevista);
        }

        // POST: CategoriaRevistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaRevista categoriaRevista = db.CategoriaRevista.Find(id);
            foreach (var item in categoriaRevista.SubCategoriaRevista)
            {
                db.Revista.RemoveRange(item.Revista);
            }
            db.SaveChanges();
            db.SubCategoriaRevista.RemoveRange(categoriaRevista.SubCategoriaRevista);
            db.SaveChanges();
            db.CategoriaRevista.Remove(categoriaRevista);
            db.SaveChanges();
            
            String folderPath = Server.MapPath("~/images/Revista/" + categoriaRevista.nombre+"/");
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
