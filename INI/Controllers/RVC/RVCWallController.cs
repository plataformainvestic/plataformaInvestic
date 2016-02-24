using INI.Models.Admin;
using INI.Models.DataBase;
using INI.Models.DataTableAjaxModels;
using INI.Models.DataTableAjaxModels.Binder;
using INI.Models.Mymodels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace INI.Controllers.RVC
{
    //StateUserAceptGroup 0 desactivado 1 mis redes  2 miembro de grupo 3 invitaciones 4 solicitantes
    public class RVCWallController : Controller
    {
        // GET: RVCWall
        private investicEntities db = new investicEntities();
        //---id = idgrupo
        public ActionResult Index(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Guid userid = new Guid(AspNetUsers.GetUserId(User.Identity.Name));
            Guid idOwner= db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == id && m.isOwner == true).Select(m => m.idNetVirtualUser).FirstOrDefault();
            //Solicitantes
            var rvcSolicitante = (from q in db.tbNetVirtualUserGroup
                                 where q.idNetVirtualGroup == id  && q.isOwner == false && q.tbNetVirtualGroup.state == true && q.StateUserAceptGroup == 4
                                 select new RedVirtualRequesting
                                 {
                                     id = q.idNetVirtualGroup,
                                     Name = q.tbNetVirtualGroup.name,                                     
                                     idAplicant = q.idNetVirtualUser,
                                     State = q.tbNetVirtualGroup.state,
                                     StateUserAceptGroup = q.StateUserAceptGroup,
                                     IsOwner = q.isOwner,
                                     CreateDate = q.tbNetVirtualGroup.createDate.Value
                                 }).ToList();
            foreach (var item in rvcSolicitante)
            {
                item.NameUser=AspNetUsers.GetNameById(item.idAplicant);
                item.idUser = idOwner;
            }
            //Mensajes
            var msj = (from q in db.tbNetVirtualWall
                      where q.tbNetVirtualUserGroup.idNetVirtualGroup == id
                      orderby q.dateSend descending
                      select new RedVirtualWallMessage
                      {                          
                          idUser=q.tbNetVirtualUserGroup.idNetVirtualUser,                          
                          Message=q.message,
                          DataSend=q.dateSend.Value
                      }).ToList();
            foreach (var item in msj)
            {
                item.IsOwner = item.idUser == idOwner;
                item.IsMine = item.idUser == userid;
            }
            RVCWMessages RVCWmessages = new RVCWMessages()
            {
                idNVCGrpUser =db.tbNetVirtualUserGroup.Where(m=>m.idNetVirtualGroup==id && m.idNetVirtualUser==userid).Select(m=>m.id).FirstOrDefault(),
                RedvirtualWallMessage = msj
            };
            //Perfiles
            var profiles = db.tbNetVirtualUserGroup.Where(m => m.idNetVirtualGroup == id).Select(m => new { m.idNetVirtualUser,m.isOwner, m.tbNetVirtualUser.state });
            List<UserRVC> usersRVC = new List<UserRVC>();
            foreach (var item in profiles)
            {
                string uid=item.idNetVirtualUser.ToString();
                var urvc = (from m in db.AspNetUsers
                            where m.Id == uid
                            select new UserRVC()
                            {  
                                
                                Genre=m.Genre,                          
                                Email = m.Email,
                            }).FirstOrDefault();

                if (urvc != null)
                {
                    urvc.Id = item.idNetVirtualUser;
                    urvc.IsOwner = item.isOwner;
                    urvc.State = item.state;
                    urvc.Name = AspNetUsers.GetNameById(item.idNetVirtualUser);
                    usersRVC.Add(urvc);
                }
                
            }

            

            RVCWall rvm = new RVCWall()
            {
                Messages=RVCWmessages,
                Aplicant=rvcSolicitante,
                UsersRVC=usersRVC,
                id=id,
                NameNVC=db.tbNetVirtualGroup.Where(m=>m.id==id).First().name
            };
            return View(rvm);
        }
        public ActionResult ChatRVC()
        {
            return RedirectToAction("Chat","Home");
        }

        public ActionResult InvitarUsuario(int id)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name) )) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.Id = id;
            return View();
        }
        public ActionResult getUsers([ModelBinder(typeof(RqDatatableModelBinder))]RequestModel requestModel)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ResponseModel<UserAdmin> responseModel = new ResponseModel<UserAdmin>();
            Type dat = typeof(UserAdmin);

            //---Parametros de control
            string conditions = "";
            string orderbyparameters = "";

            var properties = dat.GetProperties();
            var q = db.AspNetUsers.Select(m => new UserAdmin()
            {
                DT_RowId = m.Id,
                SureName = m.SureName,                
                Name = m.Name,
                Email = m.Email,
                Address = m.Address,
                PersonalID = m.PersonalID,
                BirthDay = m.BirthDay,
                Genre = m.Genre == 1 ? "M" : "F",
                Rol = m.AspNetUserRoles.FirstOrDefault().AspNetRoles.Name
            }
            );
            q = q.Where(m => m.Rol == "Maestro" || m.Rol == "Estudiante");
            if (!String.IsNullOrEmpty(requestModel.Search.Value))
            {
                string searchPhrase = requestModel.Search.Value;
                int i = 0;
                int n = properties.Count();

                foreach (var property in properties)
                {
                    if (property.Name == "DT_RowId")
                    {
                        i++;
                        continue;
                    }
                    if (property.Name == "Tipodocumento")
                    {
                        i++;
                        continue;
                    }
                    if (property.PropertyType.Name == "DateTime")
                    {
                        i++;
                        continue;
                    }
                    if (property.PropertyType.Name == "String") conditions += string.Format("{0}.Contains(\"{1}\")", property.Name, searchPhrase);
                    if (i < n - 3) conditions += " || ";
                    i++;
                }
            }

            if (requestModel.Orders.ToList()[0].Column >= 0)
            {
                int column = requestModel.Orders.ToList()[0].Column;
                string key = requestModel.Columns.ToList()[column].Data;
                string val = requestModel.Orders.ToList()[0].Dir;
                string direction = val == "desc" ? "descending" : "";
                orderbyparameters = string.Format("{0} {1}", key, direction);
            }

            // Build Response 

            responseModel.recordsTotal = db.AspNetUsers.Count();
            responseModel.draw = requestModel.Draw;

            if (conditions != "" && orderbyparameters != "")
            {
                //var q = db.AspNetUsers.Where(conditions).OrderBy(orderbyparameters).Skip(requestModel.Start).Take(requestModel.Length).
                var q2 = q.Where(string.Format(conditions, requestModel.Search.Value)).OrderBy(orderbyparameters);
                responseModel.recordsFiltered = q2.Count();
                responseModel.data = q2.Skip(requestModel.Start).Take(requestModel.Length).ToList();
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            else if (conditions == "" && orderbyparameters != "")
            {
                var q2 = q.OrderBy(orderbyparameters).Skip(requestModel.Start).Take(requestModel.Length);
                responseModel.recordsFiltered = q.Count();
                responseModel.data = q2.ToList();
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                responseModel.recordsFiltered = responseModel.recordsTotal;
                responseModel.data = q.OrderBy("SureName").Skip(requestModel.Start).Take(requestModel.Length).ToList();

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

        }
        //JSON

        //@id is usergrp
        public ActionResult SendMessage(int id, string msj)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name)|| AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Validar si el usuario pertenece a ese grupo
            try
            {
                tbNetVirtualWall tbNVW = new tbNetVirtualWall()
                {                    
                    dateSend=System.DateTime.Now,
                    idNetVirtualUserGroup=id,
                    message=DumpHRefs(msj),
                    
                };
                db.tbNetVirtualWall.Add(tbNVW);
                db.SaveChanges();
                return Json(new { rta = "Mensaje almacendao con exito" },JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception)
            {

                return Json(new { rta = "Ocurrio un error inesperado" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult SendInvitation(Guid id, int idgrp)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                int nu = db.tbNetVirtualUser.Where(m => m.id == id).Count();
                if (nu == 0)
                {
                    db.tbNetVirtualUser.Add(new tbNetVirtualUser() { id = id, state = true });
                    db.SaveChanges();
                }
                tbNetVirtualUserGroup tvug = new tbNetVirtualUserGroup()
                {
                    idNetVirtualGroup = idgrp,
                    idNetVirtualUser = id,
                    StateUserAceptGroup = 3,
                    isOwner = false
                };
                db.tbNetVirtualUserGroup.Add(tvug);
                db.SaveChanges();
                return Json(new { rta = "Solicitud enviada" });      
            }
            catch (Exception)
            {

                return Json(new { rta = "Ocurrio un error inesperado" });    
            }
                  
            
        }

        //Aprove Request from user externo
        public ActionResult AproveRequest(Guid id, int idgrp)
        {
            if (!(AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Maestro", User.Identity.Name))) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                var tvug = (from q in db.tbNetVirtualUserGroup
                           where q.idNetVirtualGroup == idgrp && q.idNetVirtualUser == id
                           select q).First();

                tvug.StateUserAceptGroup = 2;
                db.Entry(tvug).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { rta = string.Format("El usuario {0} pertenece a la red {1}",AspNetUsers.GetNameById(id),tvug.tbNetVirtualGroup.name) },JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { rta = "Ocurrio un error inesperado" });
            }

        }

        private  string DumpHRefs(string inputString)
        {
            Match m;

            string HRefPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

            try
            {
                m = Regex.Match(inputString, HRefPattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));
                while (m.Success)
                {
                    string value = m.Groups[0].ToString();
                    string ns = string.Format("<a href='{0}' target='_blank'>{1}</a>", value, value);
                    inputString = inputString.Replace(value, ns);
                    m = m.NextMatch();
                }
                return inputString;
            }
            catch (RegexMatchTimeoutException)
            {
                return "error";
            }
        }

        #region Foros del proyecto
        public ActionResult ForoRVC(int id, int code = 0)
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            var tbforo = db.tbNetVirtualForo.Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.idForo == null)
                .Include(m => m.tbNetVirtualForo1);
            ViewBag.idGrupoinvestigacion = id;
            

           

            return View(tbforo.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarForo([Bind(Include = "id,idUser,idGrupoInvestigacion,Titulo,Mensaje,Fecha,Respuestas,idForo,FechaUltimaRespuesta")] tbNetVirtualForo tblforo)
        {
            tblforo.Fecha = DateTime.Now;

           

            if (ModelState.IsValid)
            {
                db.tbNetVirtualForo.Add(tblforo);
                db.SaveChanges();
                if (tblforo.idForo.HasValue)
                {
                    tbNetVirtualForo tblForoPrincipal = db.tbNetVirtualForo.Find(tblforo.idForo.Value);
                    int respuestas = db.tbNetVirtualForo.Where(m => m.idForo == tblforo.idForo.Value).Count();
                    tblForoPrincipal.Respuestas = respuestas;
                    db.Entry(tblForoPrincipal).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ForoRVC", new { id = tblforo.idGrupoInvestigacion });
            }
            return RedirectToAction("ForoRVC", new { id = tblforo.idGrupoInvestigacion });
        }
        #endregion
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