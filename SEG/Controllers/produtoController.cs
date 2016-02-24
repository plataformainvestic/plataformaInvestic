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
    public class produtoController : Controller
    {
        private Entities db = new Entities();

        // GET: produto
        public ActionResult Index(int id=0)
        {
            var productos = db.productos.Include(p => p.AspNetUser).Include(p => p.actividade);

            if(id!=0)
            {
                productos = db.productos.Where(x=>x.Id_Actividad==id).Include(p => p.AspNetUser).Include(p => p.actividade);

            }

            return View(productos.ToList());
        }

        public ActionResult lista(int id = 0)
        {
            var productos = db.productos.Include(p => p.AspNetUser).Include(p => p.actividade);

            if (id != 0)
            {
                productos = db.productos.Where(x => x.Id_Actividad == id).Include(p => p.AspNetUser).Include(p => p.actividade);

            }

            return View(productos.ToList());
        }
        // GET: produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: produto/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");            
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista");
            return View();
        }

        // POST: produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Producto,Id_Contratista,Id_Actividad,Nombre_Producto,Descripcion_Producto")] producto producto)
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
                        if(hpf.ContentLength==0)                        
                            continue;

                        if(hpf.ContentLength>=52428800)
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
                                
                                string folderPath = Server.MapPath("~/productos/docs/");
                                Directory.CreateDirectory(folderPath);
                                string filename = string.Format("{0}-{1}-{2}",
                                    currentUser.Cedula,
                                    DateTime.Now.ToString("ddMMyyyyHHmmss"),
                                    hpf.FileName);
                                string savedfileName = folderPath + filename;
                                hpf.SaveAs(savedfileName);
                                producto.Nombre_Producto = filename;
                                db.productos.Add(producto);
                                db.SaveChanges();

                            }
                            else
                            {
                                ModelState.AddModelError("ErrMessage", "Solamente se permite archivos comprimidos, extension zip o rar");
                                return Content("<script language='javascript' type='text/javascript'>alert('Solamente se permite archivos comprimidos, extension zip o rar'); history.go(-1); </script>");
                                
                                //ViewBag.Message = "Solamente se permite archivos comprimidos, extension zip o rar";
                              //return RedirectToAction("Create", "actividad", new { id=producto.Id_Actividad});
                            
                            }
                           
                        }
                        else
                        {
                            ModelState.AddModelError("ErrMessage", "El tamaño debe ser menor de 50MB");
                            return Content("<script language='javascript' type='text/javascript'>alert('El tamaño debe ser menor de 50MB'); history.go(-1); </script>");
                            //ViewBag.Message = "Tamaño maximo permitido es 50MB";
                           //return RedirectToAction("Create", "actividad", new { id = producto.Id_Actividad });

                        }
                        
                        
                      
                    }
                    
                    return RedirectToAction("Create", "actividad", new { id=producto.Id_Actividad});
                }
                catch (Exception)
                {
                    return RedirectToAction("Create", "actividad", new { id = producto.Id_Actividad });
                }
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", producto.Id_Contratista);
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", producto.Id_Actividad);
            return RedirectToAction("Create", "actividad", new { id = producto.Id_Actividad });
        }

        // GET: produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", producto.Id_Contratista);
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", producto.Id_Actividad);
            return View(producto);
        }

        // POST: produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Producto,Id_Contratista,Id_Actividad,Nombre_Producto,Descripcion_Producto")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", producto.Id_Contratista);
            ViewBag.Id_Actividad = new SelectList(db.actividades, "Id_Actividad", "Id_Contratista", producto.Id_Actividad);
            return View(producto);
        }

        // GET: produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producto producto = db.productos.Find(id);
            db.productos.Remove(producto);
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
