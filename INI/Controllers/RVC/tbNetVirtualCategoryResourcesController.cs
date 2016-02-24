
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
    public class tbNetVirtualCategoryResourcesController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tbNetVirtualCategoryResources
        public ActionResult Index()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            return View(db.tbNetVirtualCategoryResource.ToList());
        }

        // GET: tbNetVirtualCategoryResources/Details/5
        public ActionResult Details(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualCategoryResource tbNetVirtualCategoryResource = db.tbNetVirtualCategoryResource.Find(id);
            if (tbNetVirtualCategoryResource == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualCategoryResource);
        }

        // GET: tbNetVirtualCategoryResources/Create
        public ActionResult Create()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            return View();
        }

        // POST: tbNetVirtualCategoryResources/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] tbNetVirtualCategoryResource tbNetVirtualCategoryResource)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
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
                        PropertyInfo propInfo = typeof(tbNetVirtualCategoryResource).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualCategoryResource, buffer);                        
                        FileMetaData fmd = new FileMetaData() { FileId = item, CreatedOn = dn, ModifiedOn = dn, ContentType = file.ContentType, Size = length / 1024 };
                        mdfiles.Add(fmd);                       
                    }
                }

                tbNetVirtualCategoryResource.JsonMetadata = JsonConvert.SerializeObject(mdfiles);
                db.tbNetVirtualCategoryResource.Add(tbNetVirtualCategoryResource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbNetVirtualCategoryResource);
        }

        // GET: tbNetVirtualCategoryResources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualCategoryResource tbNetVirtualCategoryResource = db.tbNetVirtualCategoryResource.Find(id);

            if (tbNetVirtualCategoryResource == null)
            {
                return HttpNotFound();
            }        
    
            tbNetVirtualCategoryResource.image=null;
            
            return View(tbNetVirtualCategoryResource);
        }

        // POST: tbNetVirtualCategoryResources/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] tbNetVirtualCategoryResource tbNetVirtualCategoryResource)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            if (ModelState.IsValid)
            {
                var q = (from m in db.tbNetVirtualCategoryResource where m.id == tbNetVirtualCategoryResource.id select new { m.image, m.JsonMetadata}).FirstOrDefault();
                if (q != null)
                {
                    tbNetVirtualCategoryResource.image = q.image;
                    tbNetVirtualCategoryResource.JsonMetadata = q.JsonMetadata;
                }
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualCategoryResource.JsonMetadata);
                DateTime dn = System.DateTime.Now;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    int length = file.ContentLength;
                    if (length > 0 && file != null)
                    {
                        byte[] buffer = new byte[length];
                        file.InputStream.Read(buffer, 0, length);
                        PropertyInfo propInfo = typeof(tbNetVirtualCategoryResource).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualCategoryResource, buffer);                        
                        FileMetaData fmd=mdfiles.Find(m => m.FileId == item);
                        fmd.ModifiedOn = System.DateTime.Now;
                        fmd.ContentType = file.ContentType;
                        fmd.Size = length / 1024;                      
                    }
                }

                tbNetVirtualCategoryResource.JsonMetadata = JsonConvert.SerializeObject(mdfiles);

                db.Entry(tbNetVirtualCategoryResource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbNetVirtualCategoryResource);
        }

        // GET: tbNetVirtualCategoryResources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualCategoryResource tbNetVirtualCategoryResource = db.tbNetVirtualCategoryResource.Find(id);
            if (tbNetVirtualCategoryResource == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualCategoryResource);
        }

        // POST: tbNetVirtualCategoryResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualCategoryResources";
            tbNetVirtualCategoryResource tbNetVirtualCategoryResource = db.tbNetVirtualCategoryResource.Find(id);
            db.tbNetVirtualCategoryResource.Remove(tbNetVirtualCategoryResource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult DowloadFile(int? id, string FileId="")
       {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualCategoryResource tbNetVirtualCategoryResource = db.tbNetVirtualCategoryResource.Find(id);
            
            if (tbNetVirtualCategoryResource != null)
            {
                Type t = typeof(tbNetVirtualCategoryResource);
                PropertyInfo pi = t.GetProperty(FileId);
                byte[] file = (byte[]) pi.GetValue(tbNetVirtualCategoryResource);
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject <List<FileMetaData>>(tbNetVirtualCategoryResource.JsonMetadata);
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

