using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using INI.Models.DataBase;

namespace INI.Controllers.Noticias
{
    //[Authorize(Roles = "Administrator,Editor")]
    [Authorize]
    public class NoticiasController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: /Noticias/
        public ActionResult Index()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //return View(db.tblNoticias.Where(estaActivo => estaActivo.estaActivo == true).ToList());
            return View(db.tblNoticias.ToList());
        }

        // GET: /Noticias/Details/5
        public ActionResult Details(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNoticias tblnoticias = db.tblNoticias.Find(id);
            if (tblnoticias == null)
            {
                return HttpNotFound();
            }
            return View(tblnoticias);
        }

        // GET: /Noticias/Create
        public ActionResult Create()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        
        // POST: /Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Noticia_ID,not_titulo,not_descripcion,not_urlimage,not_contenido,not_piedefoto,not_fecha,not_autor,not_urlpotcast,estaActivo")] tblNoticias tblnoticias)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName;
                tblnoticias.estaActivo = true;
                
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    if (file.Equals("not_urlimage"))
                    {
                        folderPath = Server.MapPath("~/images/Gallery/Noticias/");
                        PathforDB = "/images/Gallery/Noticias/";

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        filename = string.Format("{0}-{1}",
                              DateTime.Now.ToString("ddMMyyyyHHmmss"),
                              Path.GetFileName(hpf.FileName));

                        savedfileName = folderPath + filename;

                        hpf.SaveAs(savedfileName);
                        tblnoticias.not_urlimage = PathforDB + filename;
                    }
                    else if (file.Equals("not_urlpotcast"))
                    {
                        folderPath = Server.MapPath("~/Audio/Noticias/");
                        PathforDB = "/Audio/Noticias/";

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        filename = string.Format("{0}-{1}",
                              DateTime.Now.ToString("ddMMyyyyHHmmss"),
                              Path.GetFileName(hpf.FileName));

                        savedfileName = folderPath + filename;

                        hpf.SaveAs(savedfileName);
                        tblnoticias.not_urlpotcast = PathforDB + filename;

                    }
                   
                }
                

                db.tblNoticias.Add(tblnoticias);
                db.SaveChanges();
                return RedirectToAction("Index");
               
            }

            return RedirectToAction("Create");
        }

        // GET: /Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNoticias tblnoticias = db.tblNoticias.Find(id);
            if (tblnoticias == null)
            {
                return HttpNotFound();
            }
            return View(tblnoticias);
        }

        // POST: /Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Noticia_ID,not_titulo,not_descripcion,not_urlimage,not_contenido,not_piedefoto,not_fecha,not_autor,not_urlpotcast,estaActivo")] tblNoticias tblnoticias)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                string folderPath = "";
                string PathforDB = "";
                string filename = "";
                string savedfileName="";

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    if (file.Equals("not_urlimage"))
                    {
                        
                        folderPath = Server.MapPath("~/images/Gallery/Noticias/");
                        PathforDB = "/images/Gallery/Noticias/";

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        if (string.IsNullOrEmpty(hpf.FileName))
                        {
                            filename = (from m in db.tblNoticias where m.Noticia_ID == tblnoticias.Noticia_ID select new { m.not_urlimage }).FirstOrDefault().ToString();
                        }
                        else
                        {
                            filename = string.Format("{0}-{1}",
                              DateTime.Now.ToString("ddMMyyyyHHmmss"),
                              Path.GetFileName(hpf.FileName));
                        }
                        savedfileName = folderPath + filename;

                        hpf.SaveAs(savedfileName);
                        tblnoticias.not_urlimage = PathforDB + filename;
                    }
                    else if (file.Equals("not_urlpotcast"))
                    {
                        folderPath = Server.MapPath("~/Audio/Noticias/");
                        PathforDB = "/Audio/Noticias/";

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        filename = string.Format("{0}-{1}",
                              DateTime.Now.ToString("ddMMyyyyHHmmss"),
                              Path.GetFileName(hpf.FileName));

                        savedfileName = folderPath + filename;

                        hpf.SaveAs(savedfileName);
                        tblnoticias.not_urlpotcast = PathforDB + filename;

                    }

                }

                string dtNews = Request.Form["not_fecha"].ToString();
                tblnoticias.not_fecha = DateTime.Parse(dtNews);
                

                db.Entry(tblnoticias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblnoticias);
        }

        // GET: /Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNoticias tblnoticias = db.tblNoticias.Find(id);
            if (tblnoticias == null)
            {
                return HttpNotFound();
            }
            return View(tblnoticias);
        }

        // POST: /Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            tblNoticias tblnoticias = db.tblNoticias.Find(id);
            db.tblNoticias.Remove(tblnoticias);
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
