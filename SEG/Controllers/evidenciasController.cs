using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SEG.Models;
using SEG.Models.DataBase;

namespace SEG.Controllers
{
    public class evidenciasController : Controller
    {
        private Entities db = new Entities();

        // GET: evidencias
        public ActionResult Index(int id=0)
        {
            var evidencias = db.evidencias.Include(e => e.actividade).Include(e => e.AspNetUser);

            if(id !=0)
            {
                evidencias = db.evidencias.Where(x => x.Id_Actividad == id).Include(p => p.AspNetUser).Include(p => p.actividade);
            }

            return View(evidencias.ToList());
        }

        public ActionResult lista(int id = 0)
        {
            var evidencias = db.evidencias.Include(p => p.AspNetUser).Include(p => p.actividade);

            if (id != 0)
            {
                evidencias = db.evidencias.Where(x => x.Id_Actividad == id).Include(p => p.AspNetUser).Include(p => p.actividade);

            }

            return View(evidencias.ToList());
        }

        // GET: evidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evidencia evidencia = db.evidencias.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            return View(evidencia);
        }

        // GET: evidencias/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista");
            return View();
        }

        // POST: evidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Evidencia,Id_Contratista,Id_Actividad,Nombre_Evidencia,Descripcion_Evidencia")] evidencia evidencia)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0) 
                            continue;

                        if (hpf.ContentLength >= 52428800)
                        {
                            ModelState.AddModelError("ErrMessage", "El tamaño debe ser menor de 50MB");
                            return Content("<script language='javascript' type='text/javascript'>alert('El tamaño debe ser menor de 50MB'); history.go(-1); </script>");
                        }

                        if(hpf.ContentLength<=52428800)
                         {

                             string ext = Path.GetExtension(hpf.FileName);
                             ext = ext.ToLower();
                             if (ext.Equals(".zip") || ext.Equals(".rar"))
                             {
                                 string folderPath = Server.MapPath("~/evidencias/docs/");
                                 Directory.CreateDirectory(folderPath);

                                 string filename = string.Format("{0}-{1}-{2}",
                                     currentUser.Cedula,
                                     DateTime.Now.ToString("ddMMyyyyHHmmss"),
                                     hpf.FileName);
                                 string savedfileName = folderPath + filename;
                                 hpf.SaveAs(savedfileName);
                                 evidencia.Nombre_Evidencia = filename;
                                 db.evidencias.Add(evidencia);
                                 db.SaveChanges();

                             }
                             else
                             {
                                 ModelState.AddModelError("ErrMessage", "Solamente se permite archivos comprimidos, extension zip o rar");
                                 return Content("<script language='javascript' type='text/javascript'>alert('Solamente se permite archivos comprimidos, extension zip o rar'); history.go(-1); </script>");
                               

                             }


                        
                        
                         }
                        else
                        {
                            ModelState.AddModelError("ErrMessage", "El tamaño debe ser menor de 50MB");
                            return Content("<script language='javascript' type='text/javascript'>alert('El tamaño debe ser menor de 50MB'); history.go(-1); </script>");

                        }



                                             
                    }
                   
                    return RedirectToAction("Create", "actividad", new { id = evidencia.Id_Actividad});
                }
                catch (Exception)
                {
                    return RedirectToAction("Create", "actividad", new { id = evidencia.Id_Actividad });
                }
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName",evidencia.Id_Contratista);
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", evidencia.Id_Actividad);
            return RedirectToAction("Create", "actividad", new { id = evidencia.Id_Actividad });
        }

        // GET: evidencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evidencia evidencia = db.evidencias.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", evidencia.Id_Actividad);
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", evidencia.Id_Contratista);
            return View(evidencia);
        }

        // POST: evidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Evidencia,Id_Contratista,Id_Actividad,Nombre_Evidencia,Descripcion_Evidencia")] evidencia evidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", evidencia.Id_Actividad);
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", evidencia.Id_Contratista);
            return View(evidencia);
        }

        // GET: evidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evidencia evidencia = db.evidencias.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            return View(evidencia);
        }

        // POST: evidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            evidencia evidencia = db.evidencias.Find(id);
            db.evidencias.Remove(evidencia);
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
