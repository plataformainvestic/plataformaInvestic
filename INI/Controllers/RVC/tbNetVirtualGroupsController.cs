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
using INI.Models.Mymodels;
using System.IO;

namespace INI.Controllers.RVC
{
    public class tbNetVirtualGroupsController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: tbNetVirtualGroups
        public ActionResult Index()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            Guid userid =new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            
            //StateUserAceptGroup 0 desactivado 1 mis redes  2 miembro de grupo 3 invitaciones 4 solicitantes

            //1 Mis redes
            var Misrvc = from q in db.tbNetVirtualUserGroup
                         where q.idNetVirtualUser == userid && q.isOwner == true
                         select new RedVirtualGrupoOtherUser
                         {
                             id = q.idNetVirtualGroup,
                             Name = q.tbNetVirtualGroup.name,
                             idUser = q.idNetVirtualUser,
                             State = q.tbNetVirtualGroup.state,
                             StateUserAceptGroup = q.StateUserAceptGroup,
                             IsOwner = q.isOwner,
                             CreateDate = q.tbNetVirtualGroup.createDate.Value
                         };
            //2 Redes a las que colaboras
            var rvcMiembro = (from q in db.tbNetVirtualUserGroup
                              where q.idNetVirtualUser == userid && q.isOwner == false && q.tbNetVirtualGroup.state == true && q.StateUserAceptGroup == 2
                              select new RedVirtualGrupoOtherUser
                              {
                                  id = q.idNetVirtualGroup,
                                  Name = q.tbNetVirtualGroup.name,
                                  State = q.tbNetVirtualGroup.state,
                                  StateUserAceptGroup = q.StateUserAceptGroup,
                                  IsOwner = q.isOwner,
                                  CreateDate = q.tbNetVirtualGroup.createDate.Value
                              }).ToList();
            foreach (var item in rvcMiembro)
            {
                item.idUser = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == item.id && m.isOwner == true).Select(m=>m.idNetVirtualUser).FirstOrDefault();
            }
            //3 Invitaciones

            var rvcInvitaciones = (from q in db.tbNetVirtualUserGroup
                                   where q.idNetVirtualUser == userid && q.isOwner == false && q.tbNetVirtualGroup.state == true && q.StateUserAceptGroup == 3
                                   select new RedVirtualGrupoOtherUser
                                   {
                                       id = q.idNetVirtualGroup,
                                       Name = q.tbNetVirtualGroup.name,
                                       idUser = q.idNetVirtualUser,
                                       State = q.tbNetVirtualGroup.state,
                                       StateUserAceptGroup = q.StateUserAceptGroup,
                                       IsOwner = q.isOwner,
                                       CreateDate = q.tbNetVirtualGroup.createDate.Value
                                   }).ToList();

            foreach (var item in rvcInvitaciones)
            {
                item.idUser = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == item.id && m.isOwner == true).Select(m => m.idNetVirtualUser).FirstOrDefault();
            }
            //4 mis solicitudes

            var rvcRequesting = (from q in db.tbNetVirtualUserGroup
                                 where q.idNetVirtualUser == userid && q.isOwner == false && q.tbNetVirtualGroup.state == true && q.StateUserAceptGroup == 4
                                 select new RedVirtualGrupoOtherUser
                                 {
                                     id = q.idNetVirtualGroup,
                                     Name = q.tbNetVirtualGroup.name,
                                     idUser = q.idNetVirtualUser,
                                     State = q.tbNetVirtualGroup.state,
                                     StateUserAceptGroup = q.StateUserAceptGroup,
                                     IsOwner = q.isOwner,
                                     CreateDate = q.tbNetVirtualGroup.createDate.Value
                                 }).ToList();

            foreach (var item in rvcRequesting)
            {
                item.idUser = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == item.id && m.isOwner == true).Select(m => m.idNetVirtualUser).FirstOrDefault();
            }

            //----------------
            var lstcodemiem = rvcMiembro.Select(m => m.id).ToList();
            var lstcodeinvs = rvcInvitaciones.Select(m => m.id).ToList();
            var lstcodereq = rvcRequesting.Select(m => m.id).ToList();
            var rvcOtros = from q in db.tbNetVirtualUserGroup
                           where q.idNetVirtualUser != userid && q.isOwner == true && q.tbNetVirtualGroup.state == true && !lstcodeinvs.Contains(q.id) && !lstcodereq.Contains(q.id) && !lstcodemiem.Contains(q.id)
                                  select new RedVirtualGrupoOtherUser
                                  {
                                      id = q.idNetVirtualGroup,
                                      Name = q.tbNetVirtualGroup.name,  
                                      idUser=q.idNetVirtualUser,
                                      State = q.tbNetVirtualGroup.state,
                                      StateUserAceptGroup = q.StateUserAceptGroup,
                                      IsOwner = q.isOwner,
                                      CreateDate = q.tbNetVirtualGroup.createDate.Value
                                  };
           



            RVCTotal rvcTotal = new RVCTotal();
            rvcTotal.Misrvc = Misrvc.ToList();
            rvcTotal.rvcMiembro = rvcMiembro;
            rvcTotal.rvcInvitaciones = rvcInvitaciones;
            rvcTotal.rvcSolicitudes = rvcRequesting;
            rvcTotal.rvcOtros = rvcOtros.ToList();
            //rvcTotal.redVirtualGrupoOtherUser = otrosgrupos.ToList();
            return View(rvcTotal);
        }

        // GET: tbNetVirtualGroups/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ControllerName="tbNetVirtualGroups";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualGroup tbNetVirtualGroup = db.tbNetVirtualGroup.Find(id);
            if (tbNetVirtualGroup == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualGroup);
        }

        // GET: tbNetVirtualGroups/Create
        public ActionResult Create()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            return View();
        }

        // POST: tbNetVirtualGroups/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] tbNetVirtualGroup tbNetVirtualGroup)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
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
                        PropertyInfo propInfo = typeof(tbNetVirtualGroup).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualGroup, buffer);                        
                        FileMetaData fmd = new FileMetaData() { FileId = item, CreatedOn = dn, ModifiedOn = dn, ContentType = file.ContentType, Size = length / 1024 };
                        mdfiles.Add(fmd);                       
                    }
                }
                tbNetVirtualGroup.state = false;
                tbNetVirtualGroup.JsonMetadata = JsonConvert.SerializeObject(mdfiles);
                tbNetVirtualGroup.createDate = System.DateTime.Now;
                db.tbNetVirtualGroup.Add(tbNetVirtualGroup);
                //---------------------------------
                Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
                tbNetVirtualUserGroup nvug = new tbNetVirtualUserGroup();
                nvug.idNetVirtualGroup = tbNetVirtualGroup.id;
                nvug.idNetVirtualUser = userid;
                nvug.isOwner = true;
                nvug.StateUserAceptGroup = 0;
                db.tbNetVirtualUserGroup.Add(nvug);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbNetVirtualGroup);
        }

        // GET: tbNetVirtualGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualGroup tbNetVirtualGroup = db.tbNetVirtualGroup.Find(id);

            if (tbNetVirtualGroup == null)
            {
                return HttpNotFound();
            }        
    
            tbNetVirtualGroup.photo=null;
            
            return View(tbNetVirtualGroup);
        }

        // POST: tbNetVirtualGroups/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] tbNetVirtualGroup tbNetVirtualGroup)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            if (ModelState.IsValid)
            {
                var q = (from m in db.tbNetVirtualGroup where m.id == tbNetVirtualGroup.id select new {m.createDate, m.photo, m.JsonMetadata}).FirstOrDefault();
                if (q != null)
                {
                    tbNetVirtualGroup.createDate = q.createDate;
                    tbNetVirtualGroup.photo = q.photo;
                    tbNetVirtualGroup.JsonMetadata = q.JsonMetadata;
                }
                List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualGroup.JsonMetadata);
                DateTime dn = System.DateTime.Now;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    int length = file.ContentLength;
                    if (length > 0 && file != null)
                    {
                        byte[] buffer = new byte[length];
                        file.InputStream.Read(buffer, 0, length);
                        PropertyInfo propInfo = typeof(tbNetVirtualGroup).GetProperty(item);
                        propInfo.SetValue(tbNetVirtualGroup, buffer);                        
                        FileMetaData fmd=mdfiles.Find(m => m.FileId == item);
                        fmd.ModifiedOn = System.DateTime.Now;
                        fmd.ContentType = file.ContentType;
                        fmd.Size = length / 1024;                      
                    }
                }

                tbNetVirtualGroup.JsonMetadata = JsonConvert.SerializeObject(mdfiles);

                db.Entry(tbNetVirtualGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbNetVirtualGroup);
        }

        // GET: tbNetVirtualGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualGroup tbNetVirtualGroup = db.tbNetVirtualGroup.Find(id);
            if (tbNetVirtualGroup == null)
            {
                return HttpNotFound();
            }
            return View(tbNetVirtualGroup);
        }

        // POST: tbNetVirtualGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ControllerName="tbNetVirtualGroups";
            tbNetVirtualGroup tbNetVirtualGroup = db.tbNetVirtualGroup.Find(id);
            db.tbNetVirtualGroup.Remove(tbNetVirtualGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult DowloadFile(int? id, string FileId="")
       {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNetVirtualGroup tbNetVirtualGroup = db.tbNetVirtualGroup.Find(id);
            
            if (tbNetVirtualGroup != null)
            {
                Type t = typeof(tbNetVirtualGroup);
                PropertyInfo pi = t.GetProperty(FileId);
                byte[] file = (byte[]) pi.GetValue(tbNetVirtualGroup);
                if (file != null)
                {
                    List<FileMetaData> mdfiles = JsonConvert.DeserializeObject<List<FileMetaData>>(tbNetVirtualGroup.JsonMetadata);
                    string content_type = mdfiles.Where(m => m.FileId == FileId).Select(m => m.ContentType).First();
                    return File(file, content_type);
                }
                else
                {
                    var dir = Server.MapPath("/Images");
                    var path = Path.Combine(dir, "Personajes.png");
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

