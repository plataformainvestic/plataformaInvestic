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
using System.ComponentModel;
using System.Drawing;

namespace INI.Controllers.RepositorioDigital
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class RepositoriosController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Repositorios
        public ActionResult Index()
        {
            var Repositorio = db.Repositorio.Include(r => r.SubCategoriaRepositorio);
            return View(Repositorio.ToList());
        }

        // GET: Repositorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repositorio Repositorio = db.Repositorio.Find(id);
            if (Repositorio == null)
            {
                return HttpNotFound();
            }
            return View(Repositorio);
        }

        // GET: Repositorios/Create
        public ActionResult Create()
        {
            var q = db.CategoriaRepositorio.OrderBy(m => m.id).FirstOrDefault();
            if (q == null) return RedirectToAction("Index");
            var qsc = db.SubCategoriaRepositorio.Where(m => m.id_categoria == q.id);
            if (qsc == null) return RedirectToAction("Index");
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRepositorio.OrderBy(m => m.id), "id", "nombre");
            return View();
        }

        // POST: Repositorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,urlRepositorio,id_SubCategoria,Description")] Repositorio Repositorio)
        {
            
            if (ModelState.IsValid)
            {

                string folderPath = "";
                string folderPath2 = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string edicion = Repositorio.title;
                string subcategoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().nombre;
                string categoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().CategoriaRepositorio.nombre;


                folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                folderPath2 = folderPath + "images/";
                PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/";



                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;



                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    if (!Directory.Exists(folderPath2))
                        Directory.CreateDirectory(folderPath2);


                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));


                    if (file == "urlfront")
                    {
                        
                        savedfileName = folderPath + filename;
                        hpf.SaveAs(savedfileName);
                        Repositorio.urlfront = PathforDB + filename;
                    }

                }
                db.Repositorio.Add(Repositorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var q = db.CategoriaRepositorio.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRepositorio.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRepositorio.OrderBy(m => m.id), "id", "nombre");
            return View(Repositorio);
        }


        // GET: Repositorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repositorio Repositorio = db.Repositorio.Find(id);
            if (Repositorio == null)
            {
                return HttpNotFound();
            }
            var q = db.CategoriaRepositorio.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRepositorio.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRepositorio.OrderBy(m => m.id), "id", "nombre");
            return View(Repositorio);
        }

        // POST: Repositorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,urlRepositorio,id_SubCategoria,Description")] Repositorio Repositorio)
        {
            if (ModelState.IsValid)
            {
                string folderPathPDf = "";
                string folderPathFront = "";


                string filePathPDF = "";
                string filePathFront = "";

                string folderPath = "";
                string folderPath2 = "";
                string folderPathOld = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string edicion = Repositorio.title;
                string subcategoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().nombre;
                string categoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().CategoriaRepositorio.nombre;

                string oldCategoria = (from m in db.Repositorio where m.id == Repositorio.id select m.SubCategoriaRepositorio.CategoriaRepositorio.nombre).FirstOrDefault();
                string oldSubCategoria = (from m in db.Repositorio where m.id == Repositorio.id select m.SubCategoriaRepositorio.nombre).FirstOrDefault();
                string oldEdicion = (from m in db.Repositorio where m.id == Repositorio.id select m.title).FirstOrDefault();                
                string oldUrlFront = (from m in db.Repositorio where m.id == Repositorio.id select m.urlfront).FirstOrDefault();

                folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                folderPathFront = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                folderPathOld = Server.MapPath("~/images/Repositorio/" + oldCategoria + "/" + oldSubCategoria + "/" + oldEdicion + "/");
                folderPath2 = folderPath + "images/";

                
                filePathFront = Server.MapPath("~" + oldUrlFront);

                PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/";

                if (oldCategoria != categoria || oldSubCategoria != subcategoria)
                {

                    Directory.Move(folderPathOld, folderPath);                    
                    oldUrlFront = PathforDB + Path.GetFileName(filePathFront);

                    oldCategoria = categoria;
                    oldSubCategoria = subcategoria;

                    folderPathOld = Server.MapPath("~/images/Repositorio/" + oldCategoria + "/" + oldSubCategoria + "/" + oldEdicion + "/");
                    
                    filePathFront = Server.MapPath("~" + oldUrlFront);

                    PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/";

                }
                else if (oldEdicion != edicion)
                {
                    Directory.Move(folderPathOld, folderPath);
                    
                    oldUrlFront = PathforDB + Path.GetFileName(filePathFront);
                    
                    filePathFront = Server.MapPath("~" + oldUrlFront);
                }

                
                Repositorio.urlfront = oldUrlFront;



                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    folderPathPDf = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                    folderPath2 = folderPathPDf + "images/";
                    PathforDB = "/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/";



                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    if (file == "urlfront")
                    {
                        if (!String.IsNullOrEmpty(oldUrlFront))
                        {
                            if (System.IO.File.Exists(filePathFront)) System.IO.File.Delete(filePathFront);
                        }
                        savedfileName = folderPathFront + filename;
                        hpf.SaveAs(savedfileName);
                        Repositorio.urlfront = PathforDB + filename;
                    }

                }
                db.Entry(Repositorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var q = db.CategoriaRepositorio.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRepositorio.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRepositorio.OrderBy(m => m.id), "id", "nombre");
            return View(Repositorio);
        }

        // GET: Repositorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repositorio Repositorio = db.Repositorio.Find(id);
            if (Repositorio == null)
            {
                return HttpNotFound();
            }
            return View(Repositorio);
        }

        // POST: Repositorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Repositorio Repositorio = db.Repositorio.Find(id);
            string edicion = Repositorio.title;
            string subcategoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().nombre;
            string categoria = db.SubCategoriaRepositorio.Where(m => m.id == Repositorio.id_SubCategoria).First().CategoriaRepositorio.nombre;
            db.Repositorio.Remove(Repositorio);
            String folderPath = Server.MapPath("~/images/Repositorio/" + categoria + "/" + subcategoria + "/" + edicion + "/");
            try
            {
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true);
            }
            catch
            {

            }
            finally
            {
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult GetSubcategoria(int id = 0)
        {
            var qsc = from q in db.SubCategoriaRepositorio where q.id_categoria == id select new { id = q.id, nombre = q.nombre };
            return Json(qsc, JsonRequestBehavior.AllowGet);
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
