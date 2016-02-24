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
using Microsoft.AspNet.Identity;

namespace INI.Controllers.RecursosEducativos
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class RecursosEducativosController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /RecursosEducativos/
        public ActionResult Index()
        {
            var userId= User.Identity.GetUserId();
            var tblrecursoseducativos = db.tblRecursosEducativos.Include(t => t.tblColecciones).Where(t => t.id_user.Equals(userId));
            return View(tblrecursoseducativos.ToList());
        }

        public ActionResult getRecByCollections(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblrecursoseducativos = db.tblRecursosEducativos.Where(t => t.id_coleccion == id);

            if (tblrecursoseducativos == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/RecursosEducativos/_RecursosEducativos.cshtml", tblrecursoseducativos);

        }

        public ActionResult getResourcesDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblrecursoseducativos = db.tblRecursosEducativos.Find(id);

            if (tblrecursoseducativos == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/RecursosEducativos/Details.cshtml", tblrecursoseducativos);

        }

        // GET: /RecursosEducativos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecursosEducativos tblrecursoseducativos = db.tblRecursosEducativos.Find(id);
            if (tblrecursoseducativos == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/RecursosEducativos/Detalles.cshtml", tblrecursoseducativos);
        }

        // GET: /RecursosEducativos/Create
        public ActionResult Create()
        {
            ViewBag.id_coleccion = new SelectList(db.tblColecciones, "id_coleccion", "nom_coleccion");
            return View();
        }

        // POST: /RecursosEducativos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id_rec_educativo,nom_rec_educativo,desc_rec_educativo,icono_rec_educativo,archivo_rec_educativo,id_coleccion,id_user")] tblRecursosEducativos tblrecursoseducativos)
        {
            if (ModelState.IsValid)
            {

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    string folderPath = Server.MapPath("~/Archivos/RecursosEducativos/");
                    string PathforDB = "/Archivos/RecursosEducativos/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = string.Format("{0}-{1}",
                          DateTime.Now.ToString("ddMMyyyyHHmmss"),
                          Path.GetFileName(hpf.FileName));

                    string savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);

                    tblrecursoseducativos.archivo_rec_educativo = PathforDB + filename;
                    tblrecursoseducativos.icono_rec_educativo = "/images/icono_agenda.png";
                    tblrecursoseducativos.autor_rec_educativo = User.Identity.GetUserName();
                    tblrecursoseducativos.id_user = User.Identity.GetUserId();

                    db.tblRecursosEducativos.Add(tblrecursoseducativos);
                    db.SaveChanges();

                }


            }
            return RedirectToAction("Index");
        }

        // GET: /RecursosEducativos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecursosEducativos tblrecursoseducativos = db.tblRecursosEducativos.Find(id);
            if (tblrecursoseducativos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_coleccion = new SelectList(db.tblColecciones, "id_coleccion", "nom_coleccion", tblrecursoseducativos.id_coleccion);
            return View(tblrecursoseducativos);
        }

        // POST: /RecursosEducativos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id_rec_educativo,nom_rec_educativo,desc_rec_educativo,icono_rec_educativo,archivo_rec_educativo,id_coleccion,id_user")] tblRecursosEducativos tblrecursoseducativos)
        {
            if (ModelState.IsValid)
            {

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    string folderPath = Server.MapPath("~/Archivos/RecursosEducativos/");
                    string PathforDB = "/Archivos/RecursosEducativos/";

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = string.Format("{0}-{1}",
                          DateTime.Now.ToString("ddMMyyyyHHmmss"),
                          Path.GetFileName(hpf.FileName));

                    string savedfileName = folderPath + filename;

                    hpf.SaveAs(savedfileName);

                    tblrecursoseducativos.archivo_rec_educativo = PathforDB + filename;
                    tblrecursoseducativos.icono_rec_educativo = "/images/icono_agenda.png";
                    tblrecursoseducativos.autor_rec_educativo = User.Identity.GetUserName();
                    tblrecursoseducativos.id_user = User.Identity.GetUserId();


                    db.Entry(tblrecursoseducativos).State = EntityState.Modified;
                    db.SaveChanges();
                }


            }
            return RedirectToAction("Index");
        }

        // GET: /RecursosEducativos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRecursosEducativos tblrecursoseducativos = db.tblRecursosEducativos.Find(id);
            if (tblrecursoseducativos == null)
            {
                return HttpNotFound();
            }
            return View(tblrecursoseducativos);
        }

        // POST: /RecursosEducativos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRecursosEducativos tblrecursoseducativos = db.tblRecursosEducativos.Find(id);
            db.tblRecursosEducativos.Remove(tblrecursoseducativos);
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
