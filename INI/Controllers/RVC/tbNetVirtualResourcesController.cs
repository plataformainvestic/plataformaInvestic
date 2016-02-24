
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using System.Reflection;
using Newtonsoft.Json;
using INI.Models;

namespace INI.Controllers.RVC
{
    public class tbNetVirtualResourcesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tbNetVirtualResources
        public ActionResult Index()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";     
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            var tbNetVirtualResource = db.tbNetVirtualResource.Where(m => m.idNetVirtualUser == userid);
            return View(tbNetVirtualResource.ToList());
        }

        // GET: tbNetVirtualResources/Details/5
        public ActionResult Details(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualResource tbNetVirtualResource = db.tbNetVirtualResource.Find(id);
            if (tbNetVirtualResource == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualResource);
        }

        // GET: tbNetVirtualResources/Create
        public ActionResult Create()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";            
            ViewBag.idCategory = new SelectList(db.tbNetVirtualCategoryResource, "id", "name");
            return View();
        }

        // POST: tbNetVirtualResources/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,description,idCategory")] tbNetVirtualResource tbNetVirtualResource)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            if (ModelState.IsValid)
            {
                List<FileMetaData> mdfiles = new List<FileMetaData>();
                DateTime dn = System.DateTime.Now;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    int length = file.ContentLength;
                    if (length > 0 && file != null)
                    {
                        byte[] buffer = new byte[length];
                        file.InputStream.Read(buffer, 0, length);
                        PropertyInfo propInfo = typeof(tbNetVirtualResource).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualResource, buffer);                        
                        FileMetaData fmd = new FileMetaData() { FileId = item, CreatedOn = dn, ModifiedOn = dn, ContentType = file.ContentType, Size = length / 1024 };
                        mdfiles.Add(fmd);                       
                    }
                }

                tbNetVirtualResource.JsonMetadata = JsonConvert.SerializeObject(mdfiles);
                tbNetVirtualResource.idNetVirtualUser = userid;
                db.tbNetVirtualResource.Add(tbNetVirtualResource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.idCategory = new SelectList(db.tbNetVirtualCategoryResource, "id", "name", tbNetVirtualResource.idCategory);
            return View(tbNetVirtualResource);
        }

        // GET: tbNetVirtualResources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualResource tbNetVirtualResource = db.tbNetVirtualResource.Find(id);

            if (tbNetVirtualResource == null)
            {
                return HttpNotFound();
            }        
    
            tbNetVirtualResource.resource=null;            
            
            ViewBag.idCategory = new SelectList(db.tbNetVirtualCategoryResource, "id", "name", tbNetVirtualResource.idCategory);
            return View(tbNetVirtualResource);
        }

        // POST: tbNetVirtualResources/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,idCategory")] tbNetVirtualResource tbNetVirtualResource)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            if (ModelState.IsValid)
            {
                var q = (from m in db.tbNetVirtualResource where m.id == tbNetVirtualResource.id select new { m.resource, m.JsonMetadata}).FirstOrDefault();
                if (q != null)
                {
                    tbNetVirtualResource.resource = q.resource;
                    tbNetVirtualResource.JsonMetadata = q.JsonMetadata;
                }
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualResource.JsonMetadata);
                DateTime dn = System.DateTime.Now;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    int length = file.ContentLength;
                    if (length > 0 && file != null)
                    {
                        byte[] buffer = new byte[length];
                        file.InputStream.Read(buffer, 0, length);
                        PropertyInfo propInfo = typeof(tbNetVirtualResource).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualResource, buffer);                        
                        FileMetaData fmd=mdfiles.Find(m => m.FileId == item);
                        fmd.ModifiedOn = System.DateTime.Now;
                        fmd.ContentType = file.ContentType;
                        fmd.Size = length / 1024;                      
                    }
                }

                tbNetVirtualResource.JsonMetadata = JsonConvert.SerializeObject(mdfiles);
                tbNetVirtualResource.idNetVirtualUser = userid;

                db.Entry(tbNetVirtualResource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.idCategory = new SelectList(db.tbNetVirtualCategoryResource, "id", "name", tbNetVirtualResource.idCategory);
            return View(tbNetVirtualResource);
        }

        // GET: tbNetVirtualResources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualResource tbNetVirtualResource = db.tbNetVirtualResource.Find(id);
            if (tbNetVirtualResource == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualResource);
        }

        // POST: tbNetVirtualResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualResources";
            tbNetVirtualResource tbNetVirtualResource = db.tbNetVirtualResource.Find(id);
            db.tbNetVirtualResource.Remove(tbNetVirtualResource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult DowloadFile(int? id, string FileId="")
       {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualResource tbNetVirtualResource = db.tbNetVirtualResource.Find(id);
            
            if (tbNetVirtualResource != null)
            {
                Type t = typeof(tbNetVirtualResource);
                PropertyInfo pi = t.GetProperty(FileId);
                byte[] file = (byte[]) pi.GetValue(tbNetVirtualResource);
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject <List<FileMetaData>>(tbNetVirtualResource.JsonMetadata);
                string content_type = mdfiles.Where(m => m.FileId == FileId).Select(m=>m.ContentType).First();
                return File(file, content_type);
            }
            else
            {
                return null;
            }
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

