using INI.Models.DataBase;
using INI.Models.Mymodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INI.Controllers.RVC
{
    //StateUserAceptGroup 0 desactivado 1 mis redes  2 miembro de grupo 3 invitaciones 4 solicitantes
    public class RVCController : Controller
    {
        // GET: RVC
        public ActionResult Index()
        {
            
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) ||  AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            investicEntities db = new investicEntities();
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            int n = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualUser == userid && (m.StateUserAceptGroup == 1 || m.StateUserAceptGroup == 2)).Count();
            
            if (n==0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "tbNetVirtualGroups");
            }
        }

        public ActionResult AttachmentUser()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            investicEntities db = new investicEntities();
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            int nu = db.tbNetVirtualUser.Where(m => m.id == userid).Count();
            if (nu == 0)
            {                
                db.tbNetVirtualUser.Add(new tbNetVirtualUser() { id = userid, state = true });
                db.SaveChanges();
            }
            return RedirectToAction("Index", "tbNetVirtualGroups");
        }

        public ActionResult Aprobar()
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            investicEntities db = new investicEntities();
            var Peticionrvc = from q in db.tbNetVirtualUserGroup
                                where q.isOwner==true && q.tbNetVirtualGroup.state==false
                                select new RedVirtualGrupoOwner
                                {
                                    id=q.idNetVirtualGroup,
                                    Name=q.tbNetVirtualGroup.name,                                
                                    State=q.tbNetVirtualGroup.state,
                                    IsOwner=q.isOwner,
                                    CreateDate=q.tbNetVirtualGroup.createDate.Value
                                };

            return View(Peticionrvc);
        }


        //JSON REQUEST

        

        //User acept your Request to RVK(id) for invitation (id is the group)
        public ActionResult AceptRequest(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            investicEntities db = new investicEntities();
            try
            {
                var rvc = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualUser == userid && m.idNetVirtualGroup == id).FirstOrDefault();
                rvc.StateUserAceptGroup = 2;
                db.Entry(rvc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { rta = "Ahora perteneces a la RVC" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { rta = "Ha ocurrido un error inesperado" }, JsonRequestBehavior.AllowGet);
            }
           
            
        }

        //User like to be in your RVK (id) 
        public ActionResult RequestBelong(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
                investicEntities db = new investicEntities();
               
                tbNetVirtualUserGroup tbnetVirtualUserGroup = new tbNetVirtualUserGroup();
                tbnetVirtualUserGroup.idNetVirtualUser = userid;
                tbnetVirtualUserGroup.idNetVirtualGroup = id;
                tbnetVirtualUserGroup.isOwner = false;
                tbnetVirtualUserGroup.StateUserAceptGroup = 4;
                db.tbNetVirtualUserGroup.Add(tbnetVirtualUserGroup);
                db.SaveChanges();
                return Json(new { rta = "Su solicitud ha sido enviada al creador de esta red." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { rta = "Wrong" }, JsonRequestBehavior.AllowGet);
            }           
            
        }

        //Editor aprove your RVK(id) request
        public ActionResult ChangeState(int id=0)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Editor", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);            
            investicEntities db = new investicEntities();
            
            try
            {
                var rvc = db.tbNetVirtualGroup.Find(id);
                if (rvc == null) return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
                rvc.state = true;
                var rvcug = rvc.tbNetVirtualUserGroup.Where(m => m.isOwner = true).FirstOrDefault();
                if (rvcug == null) return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
                rvcug.StateUserAceptGroup = 1;
                db.Entry(rvc).State = System.Data.Entity.EntityState.Modified;
                db.Entry(rvcug).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { rta = "La red ha sido aprobada." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
            }
           
            
        }

        

        public ActionResult ChangeStateAplicant(int id = 0)
        {
            investicEntities db = new investicEntities();
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            Guid idOwner = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == id && m.isOwner == true).Select(m => m.idNetVirtualUser).FirstOrDefault();
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || (AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) && idOwner==userid))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            
            try
            {
                var rvc = db.tbNetVirtualGroup.Find(id);
                if (rvc == null) return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
                rvc.state = true;
                var rvcug = rvc.tbNetVirtualUserGroup.Where(m => m.isOwner = true).FirstOrDefault();
                if (rvcug == null) return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
                rvcug.StateUserAceptGroup = 2;
                db.Entry(rvc).State = System.Data.Entity.EntityState.Modified;
                db.Entry(rvcug).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { rta = "La red ha sido aprobada." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}