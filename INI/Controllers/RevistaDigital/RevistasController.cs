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

namespace INI.Controllers.RevistaDigital
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class RevistasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Revistas
        public ActionResult Index()
        {
            var revista = db.Revista.Include(r => r.SubCategoriaRevista);
            return View(revista.ToList());
        }

        // GET: Revistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revista revista = db.Revista.Find(id);
            if (revista == null)
            {
                return HttpNotFound();
            }
            return View(revista);
        }

        // GET: Revistas/Create
        public ActionResult Create()
        {
            var q = db.CategoriaRevista.OrderBy(m => m.id).FirstOrDefault();
            if (q == null) return RedirectToAction("Index");
            var qsc = db.SubCategoriaRevista.Where(m => m.id_categoria == q.id);
            if (qsc == null) return RedirectToAction("Index");
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRevista.OrderBy(m=>m.id), "id", "nombre");
            return View();
        }

        // POST: Revistas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,urlPdf,id_SubCategoria,Description")] Revista revista)
        {
            if (ModelState.IsValid)
            {
               
                string folderPath = "";
                string folderPath2 = "";                
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                string edicion = revista.title;
                string subcategoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().nombre;
                string categoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().CategoriaRevista.nombre;
                

                folderPath = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/");                
                folderPath2 = folderPath + "images/";
                PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/";

                
                
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

                    

                    if (file == "urlPdf")
                    {
                        
                        savedfileName = folderPath + filename;
                        hpf.SaveAs(savedfileName);
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C PDF2IMAGE \"" + savedfileName + "\" \"" + folderPath2 + filename + "\"";
                        process.StartInfo = startInfo; process.Start();

                        revista.urlPdf = PathforDB + filename;
                        revista.urlfront = PathforDB +  filename + "1" + ".png";
                    }
                    else
                    {
                        
                        savedfileName = folderPath + filename;
                        hpf.SaveAs(savedfileName);
                        revista.urlfront = PathforDB + filename;
                    }

                }
                db.Revista.Add(revista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var q = db.CategoriaRevista.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRevista.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRevista.OrderBy(m => m.id), "id", "nombre");
            return View(revista);
        }
        

        // GET: Revistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revista revista = db.Revista.Find(id);
            if (revista == null)
            {
                return HttpNotFound();
            }
            var q = db.CategoriaRevista.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRevista.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRevista.OrderBy(m => m.id), "id", "nombre");
            return View(revista);
        }

        // POST: Revistas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,urlPdf,id_SubCategoria,Description")] Revista revista)
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
                string edicion = revista.title;
                string subcategoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().nombre;
                string categoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().CategoriaRevista.nombre;

                string oldCategoria = (from m in db.Revista where m.id == revista.id select m.SubCategoriaRevista.CategoriaRevista.nombre).FirstOrDefault();
                string oldSubCategoria = (from m in db.Revista where m.id == revista.id select m.SubCategoriaRevista.nombre).FirstOrDefault();
                string oldEdicion = (from m in db.Revista where m.id == revista.id select m.title).FirstOrDefault();
                string oldUrlPDf = (from m in db.Revista where m.id == revista.id select m.urlPdf).FirstOrDefault();
                string oldUrlFront = (from m in db.Revista where m.id == revista.id select m.urlfront).FirstOrDefault();

                folderPath = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                folderPathFront = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                folderPathOld = Server.MapPath("~/images/Revista/" + oldCategoria + "/" + oldSubCategoria + "/" + oldEdicion + "/");
                folderPath2 = folderPath + "images/";

                filePathPDF = Server.MapPath("~" + oldUrlPDf);
                filePathFront = Server.MapPath("~" + oldUrlFront);

                PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/";

                if (oldCategoria != categoria || oldSubCategoria != subcategoria)
                {

                    Directory.Move(folderPathOld, folderPath);
                    oldUrlPDf = PathforDB + Path.GetFileName(filePathPDF);
                    oldUrlFront = PathforDB + Path.GetFileName(filePathFront);

                    oldCategoria = categoria;
                    oldSubCategoria = subcategoria;

                    folderPathOld = Server.MapPath("~/images/Revista/" + oldCategoria + "/" + oldSubCategoria + "/" + oldEdicion + "/");
                    filePathPDF = Server.MapPath("~" + oldUrlPDf);
                    filePathFront = Server.MapPath("~" + oldUrlFront);

                    PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/";

                }
                else if (oldEdicion != edicion)
                {
                    Directory.Move(folderPathOld, folderPath);
                    oldUrlPDf = PathforDB + Path.GetFileName(filePathPDF);
                    oldUrlFront = PathforDB + Path.GetFileName(filePathFront);
                    filePathPDF = Server.MapPath("~" + oldUrlPDf);
                    filePathFront = Server.MapPath("~" + oldUrlFront);
                }
                
                revista.urlPdf = oldUrlPDf;
                revista.urlfront = oldUrlFront;

                

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    folderPathPDf = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/");
                    folderPath2 = folderPathPDf + "images/";
                    PathforDB = "/images/Revista/" + categoria + "/" + subcategoria + "/" + edicion + "/";
                    
                    

                    filename = string.Format("{0}-{1}",
                            DateTime.Now.ToString("ddMMyyyyHHmmss"),
                            Path.GetFileName(hpf.FileName));

                    if (file == "urlPdf")
                    {                       

                        if (!String.IsNullOrEmpty(oldUrlPDf))
                        {
                            if (System.IO.File.Exists(filePathPDF)) System.IO.File.Delete(filePathPDF);
                            if (Directory.Exists(folderPath2)) Directory.Delete(folderPathPDf, true);                            
                        }
                        if (!Directory.Exists(folderPathPDf))
                            Directory.CreateDirectory(folderPathPDf);
                        if (!Directory.Exists(folderPath2))
                            Directory.CreateDirectory(folderPath2);

                        savedfileName = folderPathPDf + filename;
                        hpf.SaveAs(savedfileName);
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C PDF2IMAGE \"" + savedfileName + "\" \"" + folderPath2 + filename + "\"";
                        process.StartInfo = startInfo; process.Start();

                        revista.urlPdf = PathforDB + filename;
                        if (!String.IsNullOrEmpty(oldUrlFront))
                        {
                            if (System.IO.File.Exists(filePathFront)) System.IO.File.Delete(filePathFront);
                        }
                        revista.urlfront = PathforDB  + filename + "1" + ".png";
                        oldUrlFront = revista.urlfront;
                    }
                    else if (file == "urlfront")
                    {
                        if (!String.IsNullOrEmpty(oldUrlFront))
                        {
                            if (System.IO.File.Exists(filePathFront)) System.IO.File.Delete(filePathFront);
                        }
                        savedfileName = folderPathFront + filename;
                        hpf.SaveAs(savedfileName);
                        revista.urlfront = PathforDB +  filename;
                    }

                }
                db.Entry(revista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var q = db.CategoriaRevista.OrderBy(m => m.id).First();
            var qsc = db.SubCategoriaRevista.Where(m => m.id_categoria == q.id);
            ViewBag.id_SubCategoria = new SelectList(qsc, "id", "nombre");
            ViewBag.id_Categoria = new SelectList(db.CategoriaRevista.OrderBy(m => m.id), "id", "nombre");
            return View(revista);
        }

        // GET: Revistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revista revista = db.Revista.Find(id);
            if (revista == null)
            {
                return HttpNotFound();
            }
            return View(revista);
        }

        // POST: Revistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Revista revista = db.Revista.Find(id);
            string edicion = revista.title;
            string subcategoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().nombre;
            string categoria = db.SubCategoriaRevista.Where(m => m.id == revista.id_SubCategoria).First().CategoriaRevista.nombre;
            db.Revista.Remove(revista);
            String folderPath = Server.MapPath("~/images/Revista/" + categoria + "/" + subcategoria+"/"+edicion+"/");
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
            var qsc =from q in db.SubCategoriaRevista where q.id_categoria==id select new {id=q.id, nombre=q.nombre};
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
