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
    public class SubCategoriaRepositoriosController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: SubCategoriaRepositorios
        public ActionResult Index()
        {
            var subCategoriaRepositorio = db.SubCategoriaRepositorio.Include(s => s.CategoriaRepositorio);
            return View(subCategoriaRepositorio.ToList());
        }

        // GET: SubCategoriaRepositorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRepositorio subCategoriaRepositorio = db.SubCategoriaRepositorio.Find(id);
            if (subCategoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaRepositorio);
        }

        // GET: SubCategoriaRepositorios/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.CategoriaRepositorio, "id", "nombre");
            return View();
        }

        // POST: SubCategoriaRepositorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,urlimg,id_categoria")] SubCategoriaRepositorio subCategoriaRepositorio)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string subcategoria = subCategoriaRepositorio.nombre;
                string categoria = db.CategoriaRepositorio.Where(m => m.id == subCategoriaRepositorio.id_categoria).First().nombre;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/");
                    PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);
                    subCategoriaRepositorio.urlimg = PathforDB + filename;

                }
                db.SubCategoriaRepositorio.Add(subCategoriaRepositorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.CategoriaRepositorio, "id", "nombre", subCategoriaRepositorio.id_categoria);
            return View(subCategoriaRepositorio);
        }

        // GET: SubCategoriaRepositorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRepositorio subCategoriaRepositorio = db.SubCategoriaRepositorio.Find(id);
            if (subCategoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.CategoriaRepositorio, "id", "nombre", subCategoriaRepositorio.id_categoria);
            return View(subCategoriaRepositorio);
        }

        // POST: SubCategoriaRepositorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,urlimg,id_categoria")] SubCategoriaRepositorio subCategoriaRepositorio)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string folderPathOld = "";
                string filePath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string subcategoria = subCategoriaRepositorio.nombre;
                string categoria = db.CategoriaRepositorio.Where(m => m.id == subCategoriaRepositorio.id_categoria).First().nombre;
                string oldURL = (from m in db.SubCategoriaRepositorio where m.id == subCategoriaRepositorio.id select m.urlimg).FirstOrDefault();
                string oldCategoria = (from m in db.SubCategoriaRepositorio where m.id == subCategoriaRepositorio.id select m.CategoriaRepositorio.nombre).FirstOrDefault();
                string oldSubCategoria = (from m in db.SubCategoriaRepositorio where m.id == subCategoriaRepositorio.id select m.nombre).FirstOrDefault();

                folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/");
                folderPathOld = Server.MapPath("~/images/Repositorio/" + oldCategoria + "/" + oldSubCategoria + "/");
                filePath = Server.MapPath("~" + oldURL);
                PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/";
                filePath = Server.MapPath("~" + oldURL);

                if (oldCategoria != categoria)
                {

                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);

                    foreach (var item2 in db.Repositorio.Where(m => m.id_SubCategoria == subCategoriaRepositorio.id))
                    {
                        string edicion = item2.title;
                        
                        item2.urlfront = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);

                        db.Entry(item2).State = EntityState.Modified;
                    }
                    oldCategoria = categoria;

                    folderPathOld = Server.MapPath("~/images/Repositorio/" + oldCategoria + "/" + oldSubCategoria + "/");
                    filePath = Server.MapPath("~" + oldURL);
                    PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/";

                }
                else if (oldSubCategoria != subcategoria)
                {
                    Directory.Move(folderPathOld, folderPath);
                    oldURL = PathforDB + Path.GetFileName(filePath);
                    foreach (var item2 in db.Repositorio.Where(m => m.id_SubCategoria == subCategoriaRepositorio.id))
                    {
                        string edicion = item2.title;                        
                        item2.urlfront = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/" + Path.GetFileName(item2.urlfront);
                        db.Entry(item2).State = EntityState.Modified;
                    }
                    filePath = Server.MapPath("~" + oldURL);
                }
                subCategoriaRepositorio.urlimg = oldURL;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

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
                    subCategoriaRepositorio.urlimg = PathforDB + filename;

                }

                db.Entry(subCategoriaRepositorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.CategoriaRepositorio, "id", "nombre", subCategoriaRepositorio.id_categoria);
            return View(subCategoriaRepositorio);
        }

        // GET: SubCategoriaRepositorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaRepositorio subCategoriaRepositorio = db.SubCategoriaRepositorio.Find(id);
            if (subCategoriaRepositorio == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaRepositorio);
        }

        // POST: SubCategoriaRepositorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoriaRepositorio subCategoriaRepositorio = db.SubCategoriaRepositorio.Find(id);
            string subcategoria = subCategoriaRepositorio.nombre;
            string categoria = db.CategoriaRepositorio.Where(m => m.id == subCategoriaRepositorio.id_categoria).First().nombre;
            db.Repositorio.RemoveRange(subCategoriaRepositorio.Repositorio);
            db.SaveChanges();
            db.SubCategoriaRepositorio.Remove(subCategoriaRepositorio);
            db.SaveChanges();

            String folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/");
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
