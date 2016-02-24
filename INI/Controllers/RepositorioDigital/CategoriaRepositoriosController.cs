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

namespace INI.Controllers.RepositorioDigital
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class CategoriaRepositoriosController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: CategoriaRepositorios
        public ActionResult Index()
        {
            return View(db.CategoriaRepositorio.ToList());
        }

        // GET: CategoriaRepositorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRepositorio categoriaRepositorio = db.CategoriaRepositorio.Find(id);
            if (categoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRepositorio);
        }

        // GET: CategoriaRepositorios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaRepositorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,urlimg")] CategoriaRepositorio categoriaRepositorio)
        {
            if (ModelState.IsValid)
            {

                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string categoria = categoriaRepositorio.nombre;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/");
                    PathforDB = "/images/Repositorio/" + categoria + "/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    categoriaRepositorio.urlimg = PathforDB + filename;

                }

                db.CategoriaRepositorio.Add(categoriaRepositorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaRepositorio);
        }


        // GET: CategoriaRepositorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRepositorio categoriaRepositorio = db.CategoriaRepositorio.Find(id);
            if (categoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRepositorio);
        }

        // POST: CategoriaRepositorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "id,nombre,urlimg")] CategoriaRepositorio categoriaRepositorio)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string folderPathOld = "";
                string filePath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string categoria = categoriaRepositorio.nombre;
                string oldURL = (from m in db.CategoriaRepositorio where m.id == categoriaRepositorio.id select m.urlimg).FirstOrDefault();
                string oldCategoria = (from m in db.CategoriaRepositorio where m.id == categoriaRepositorio.id select m.nombre).FirstOrDefault();
                folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/");
                folderPathOld = Server.MapPath("~/images/Repositorio/" + oldCategoria + "/");
                filePath = Server.MapPath("~" + oldURL);
                PathforDB = "/images/Repositorio/" + categoria + "/";


                if (oldCategoria != categoria)
                {
                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);
                    foreach (var item in db.SubCategoriaRepositorio.Where(m => m.id_categoria == categoriaRepositorio.id))
                    {
                        string subcategoria = item.nombre;
                        item.urlimg = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + Path.GetFileName(item.urlimg);
                        foreach (var item2 in item.Repositorio)
                        {
                            string edicion = item2.title;                            
                            item2.urlfront = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);

                            db.Entry(item2).State = EntityState.Modified;
                        }
                        db.Entry(item).State = EntityState.Modified;
                    }

                    filePath = Server.MapPath("~" + oldURL);
                }
                categoriaRepositorio.urlimg = oldURL;
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
                    categoriaRepositorio.urlimg = PathforDB + filename;

                }
                db.Entry(categoriaRepositorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaRepositorio);
        }


        // GET: CategoriaRepositorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaRepositorio categoriaRepositorio = db.CategoriaRepositorio.Find(id);
            if (categoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            return View(categoriaRepositorio);
        }

        // POST: CategoriaRepositorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaRepositorio categoriaRepositorio = db.CategoriaRepositorio.Find(id);
            foreach (var item in categoriaRepositorio.SubCategoriaRepositorio)
            {
                db.Repositorio.RemoveRange(item.Repositorio);
            }
            db.SaveChanges();
            db.SubCategoriaRepositorio.RemoveRange(categoriaRepositorio.SubCategoriaRepositorio);
            db.SaveChanges();
            db.CategoriaRepositorio.Remove(categoriaRepositorio);
            db.SaveChanges();

            String folderPath = Server.MapPath("~/images/Repositorio/" + categoriaRepositorio.nombre + "/");
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
