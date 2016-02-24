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
using System.IO;

namespace INI.Controllers.RVC
{
    public class tbNetVirtualUsersController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tbNetVirtualUsers
        public ActionResult Index()
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            return View(db.tbNetVirtualUser.ToList());
        }

        // GET: tbNetVirtualUsers/Details/5
        public ActionResult Details(Guid? id)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualUser tbNetVirtualUser = db.tbNetVirtualUser.Find(id);
            if (tbNetVirtualUser == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualUser);
        }

        // GET: tbNetVirtualUsers/Create
        public ActionResult Create()
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            return View();
        }

        // POST: tbNetVirtualUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,state")] tbNetVirtualUser tbNetVirtualUser)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
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
                        PropertyInfo propInfo = typeof(tbNetVirtualUser).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualUser, buffer);                        
                        FileMetaData fmd = new FileMetaData() { FileId = item, CreatedOn = dn, ModifiedOn = dn, ContentType = file.ContentType, Size = length / 1024 };
                        mdfiles.Add(fmd);                       
                    }
                }

                tbNetVirtualUser.JsonMetadata = JsonConvert.SerializeObject(mdfiles);
                tbNetVirtualUser.id = Guid.NewGuid();
                db.tbNetVirtualUser.Add(tbNetVirtualUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbNetVirtualUser);
        }

        // GET: tbNetVirtualUsers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualUser tbNetVirtualUser = db.tbNetVirtualUser.Find(id);

            if (tbNetVirtualUser == null)
            {
                return HttpNotFound();
            }        
    
            tbNetVirtualUser.photo=null;
            
            return View(tbNetVirtualUser);
        }

        // POST: tbNetVirtualUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,state")] tbNetVirtualUser tbNetVirtualUser)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            if (ModelState.IsValid)
            {
                var q = (from m in db.tbNetVirtualUser where m.id == tbNetVirtualUser.id select new { m.photo, m.JsonMetadata}).FirstOrDefault();
                if (q != null)
                {
                    tbNetVirtualUser.photo = q.photo;
                    tbNetVirtualUser.JsonMetadata = q.JsonMetadata;
                }
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualUser.JsonMetadata);
                DateTime dn = System.DateTime.Now;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    int length = file.ContentLength;
                    if (length > 0 && file != null)
                    {
                        byte[] buffer = new byte[length];
                        file.InputStream.Read(buffer, 0, length);
                        PropertyInfo propInfo = typeof(tbNetVirtualUser).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualUser, buffer);                        
                        FileMetaData fmd=mdfiles.Find(m => m.FileId == item);
                        fmd.ModifiedOn = System.DateTime.Now;
                        fmd.ContentType = file.ContentType;
                        fmd.Size = length / 1024;                      
                    }
                }

                tbNetVirtualUser.JsonMetadata = JsonConvert.SerializeObject(mdfiles);

                db.Entry(tbNetVirtualUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbNetVirtualUser);
        }

        // GET: tbNetVirtualUsers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualUser tbNetVirtualUser = db.tbNetVirtualUser.Find(id);
            if (tbNetVirtualUser == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualUser);
        }

        // POST: tbNetVirtualUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ViewBag.ControllerName="tbNetVirtualUsers";
            tbNetVirtualUser tbNetVirtualUser = db.tbNetVirtualUser.Find(id);
            db.tbNetVirtualUser.Remove(tbNetVirtualUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult DowloadFile(Guid? id, string FileId="")
       {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualUser tbNetVirtualUser = db.tbNetVirtualUser.Find(id);
            
            if (tbNetVirtualUser != null)
            {
                Type t = typeof(tbNetVirtualUser);
                PropertyInfo pi = t.GetProperty(FileId);
                byte[] file = (byte[]) pi.GetValue(tbNetVirtualUser);
                if (file!=null)
                {
                    List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualUser.JsonMetadata);
                    string content_type = mdfiles.Where(m => m.FileId == FileId).Select(m => m.ContentType).First();
                    return File(file, content_type);
                }
                else
                {
                    var dir = Server.MapPath("/Images");
                    var path = Path.Combine(dir, "beto.png");
                    return File(path, "image/png");                    
                }
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

