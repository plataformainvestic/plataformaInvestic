using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using INI.Models.DataBase;
using INI.Models.DataTableAjaxModels;
using INI.Models.Admin;
using System.Linq.Dynamic;
using INI.Models.DataTableAjaxModels.Binder;
using System.Threading.Tasks;
using INI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using INI.Extensions.Controllers;

namespace INI.Controllers.Administracion
{
    [Authorize]
    public class PanelAdministrativoController : NewtomSofController
    {
        private investicEntities db = new investicEntities();
        public PanelAdministrativoController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public PanelAdministrativoController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        // GET: AspNetUsers
        public ActionResult Index(int id=0)
        {
            ViewBag.Msj = "";
            switch (id)
            {
                case 1:
                        ViewBag.Msj = "Creación satisfactoria";
                    break;
                case 2:
                        ViewBag.Msj = "Edición satisfactoria";
                   break;
                case 3:
                        ViewBag.Msj = "Eliminación satisfactoria";
                   break;
                case 4:
                        ViewBag.Msj = "No se pudo eliminar revise las referencias circulares que tiene este registro." ;
                   break;  

            }
            return View();
        }

       
        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        
        public ActionResult RegistroAdmin()
        {

            if (AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name))
            {
                @ViewBag.RolId = new SelectList(db.AspNetRoles, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistroAdmin(RegisterViewModel model, FormCollection form)
        {
            investicEntities db = new investicEntities();
            model.UserName = model.PersonalID;
            @ViewBag.RolId = new SelectList(db.AspNetRoles, "Id", "Name", form["RolId"].ToString());

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    SureName = model.SureName,
                    PersonalID = model.PersonalID,
                    Genre = model.Genre,
                    Email = model.Mail,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    BirthDay = model.BirthDay
                };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    AspNetUserRoles asignRoleToUser = new AspNetUserRoles();
                    string roleSelected = form["RolId"].ToString();
                    asignRoleToUser.RoleId = roleSelected;
                    asignRoleToUser.UserId = AspNetUsers.GetUserId(model.UserName);
                    db.AspNetUserRoles.Add(asignRoleToUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = 1 });
                }
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

       

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            
            if (aspNetUsers == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            if (AspNetUsersRoles.IsUserInRole("Administrator", User.Identity.Name))
            {
                @ViewBag.RolId = new SelectList(db.AspNetRoles, "Id", "Name", db.AspNetUserRoles.Where(m => m.UserId == id).Select(m=>m.RoleId).FirstOrDefault());
                RegisterViewModelUpdate registerViewModelUpdate = new RegisterViewModelUpdate()
                {
                    Id=aspNetUsers.Id,
                    Address = aspNetUsers.Address,
                    BirthDay = aspNetUsers.BirthDay,
                    ConfirmPassword = "",
                    Genre = aspNetUsers.Genre,
                    Mail = aspNetUsers.Email,
                    Name = aspNetUsers.Name,                    
                    Password = "",
                    PersonalID = aspNetUsers.PersonalID,
                    Phone = aspNetUsers.PhoneNumber,
                    SureName = aspNetUsers.SureName,
                    UserName = aspNetUsers.PersonalID,
                };
 
                return View(registerViewModelUpdate);
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
            
            
        }

        // POST: AspNetUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegisterViewModelUpdate model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers aspNetUsers = db.AspNetUsers.Find(model.Id);
                aspNetUsers.UserName = model.PersonalID;
                aspNetUsers.PersonalID = model.PersonalID;
                aspNetUsers.Name = model.Name;
                aspNetUsers.SureName = model.SureName;
                aspNetUsers.Genre = model.Genre;
                aspNetUsers.PhoneNumber = model.Phone;
                aspNetUsers.Email = model.Mail;
                aspNetUsers.Address = model.Address;
                aspNetUsers.BirthDay = model.BirthDay;
                aspNetUsers.AspNetUserRoles.FirstOrDefault().RoleId = form["RolId"].ToString();
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                if (!String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.NewPassword))
                {
                    var result = await UserManager.ChangePasswordAsync(aspNetUsers.Id, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { id = 2 });
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index", new { id = 2 });
                }
                
                
            }
            return View(model);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            
            
            //Borramos manualmente las excepciones
            //--FK_tblForoProyectoInvestigacion_tblForoProyectoInvestigacion		
            //    --FK_tblGrupoInvestigacion_AspNetUsers
            //    --FK_tblIntegrantesGrupoInv_AspNetUsers
            //    --FK.dbo.actividades_dbo.AspNetUsers		
            //    --FK_tblAsesorZonaMunicipio_tblAsesorZona
            //    --FK_tblProyectosInvestigacion_tblEstado
            //    --FK_tblProyectosInvestigacion_tblProyectosInvestigacion_Rev4
            //    --FK_tblProyectosInvestigacion_tblProyectosInvestigacion_Rev5
            //    --FK_tblProyectosInvestigacion_tblProyectosInvestigacion_Rev6
            //    --FK_tblProyectosInvestigacion_Rev_tblEstado
            //    --FK.dbo.responsabilidade_dbo.AspNetUsers


           
                try
                {
                    db.Database.ExecuteSqlCommand("HabilitarCascada");
                    db.AspNetUsers.Remove(aspNetUsers);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("DesHabilitarCascada");
                    return RedirectToAction("Index", new { id = 3 });
                    
                }
                catch(Exception ex)
                {
                    try
                    {
                        db.AspNetUsers.Remove(aspNetUsers);
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = 3 });
                    }
                    catch(Exception ex2)
                    {
                        return RedirectToAction("Index", new { id = 4});
                    }
                }
                return RedirectToAction("Index");
                
            
            
            
            
            
        }

        [HttpPost]
        public ActionResult getUsers([ModelBinder(typeof(RqDatatableModelBinder))]RequestModel requestModel)
        {
            ResponseModel<UserAdmin> responseModel = new ResponseModel<UserAdmin>();
            Type dat = typeof(UserAdmin);

            //---Parametros de control
            string conditions = "";            
            string orderbyparameters = "";

            var properties = dat.GetProperties();
            var q = db.AspNetUsers.Select(m => new UserAdmin()
                    {
                        DT_RowId=m.Id,
                        SureName = m.SureName,
                        Name = m.Name,
                        Email = m.Email,
                        Address = m.Address,
                        PersonalID = m.PersonalID,
                        BirthDay = m.BirthDay,
                        Tipodocumento=m.TipoDoc.ToString(),
                        Genre = m.Genre == 1 ? "M" : "F",
                        Rol = m.AspNetUserRoles.FirstOrDefault().AspNetRoles.Name
                    }
                    );
            
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

            if (requestModel.Orders.ToList()[0].Column>=0)
            {
                int column=requestModel.Orders.ToList()[0].Column;
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
