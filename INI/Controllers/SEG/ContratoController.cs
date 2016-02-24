using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace INI.Controllers.SEG
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class ContratoController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: Contrato
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        //GET
        [Authorize]
        public ActionResult Seleccionar()
        {
            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {

                var currentUser = User.Identity.GetUserId();

                int equipo = (from e in db.tblEquiposTrabajo where e.Id_Coordinador.Equals(currentUser) select e.Id_Equipo).SingleOrDefault();

                string idequipo = equipo.ToString();

                List<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();


                ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");

                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }

            

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seleccionar(string Id_Contratista)
        {

            return RedirectToAction("Datos", new { Id_Contratista = Id_Contratista });
         
        }
        [Authorize]
        public ActionResult Datos(string Id_Contratista)
        {
            var usuario = AspNetUsers.Find(Id_Contratista);
            investicEntities db = new investicEntities();

            var currentUser = User.Identity.GetUserId();
            var equip = db.tblEquiposTrabajo.Where(x => x.Id_Coordinador.Equals(currentUser));
            var carge = db.tblCargos.ToList();
            var usuarios = db.AspNetUsers.ToList();

           
            ViewBag.Equipo = new SelectList(equip, "Id_Equipo", "Nombre_Equipo");
            ViewBag.Cargo = new SelectList(carge, "Id_Cargo", "Nombre_Cargo");

            if (AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {
                investicEntities db2 = new investicEntities();

                var currentUser2 = User.Identity.GetUserId();
                var algo = db2.tblEquiposTrabajo.Where(x => x.Id_Coordinador.Equals(currentUser2));
                var carg = db2.tblCargos.ToList();
                var usuarios2 = db2.AspNetUsers.ToList();


                ViewBag.Equipo = new SelectList(algo, "Id_Equipo", "Nombre_Equipo");
                ViewBag.Cargo = new SelectList(carg, "Id_Cargo", "Nombre_Cargo");
            }
            return View(usuario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Datos([Bind(Include = "Id,Name,SureName,PersonalID,Genre,Address,BirthDay,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LastName,Celular,Cargo,Contrato,Cdp,Equipo,Fecha_IniContrato,Fecha_FinContrato,Cedula,Nombres,Apellidos")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Seleccionar");
            }

            return View(aspNetUsers);
        }

        // GET: Contrato/Details/5
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

        // GET: Contrato/Create
        public ActionResult Crear()
        {

            investicEntities db = new investicEntities();

            var currentUser = User.Identity.GetUserId();           
            var equip = db.tblEquiposTrabajo.Where(x => x.Id_Coordinador.Equals(currentUser));
            var carge = db.tblCargos.ToList();
            var usuarios = db.AspNetUsers.ToList();

            ViewBag.Id = new SelectList(usuarios, "Id", "LastName");
            ViewBag.Equipo = new SelectList(equip, "Id_Equipo", "Nombre_Equipo");
            ViewBag.Cargo = new SelectList(carge, "Id_Cargo", "Nombre_Cargo");

            if (AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {
                investicEntities db2 = new investicEntities();

                var currentUser2 = User.Identity.GetUserId();
                var algo = db2.tblEquiposTrabajo.Where(x => x.Id_Coordinador.Equals(currentUser2));
                var carg = db2.tblCargos.ToList();
                var usuarios2 = db2.AspNetUsers.ToList();

                ViewBag.Id = new SelectList(usuarios2, "Id", "LastName");
                ViewBag.Equipo = new SelectList(algo, "Id_Equipo", "Nombre_Equipo");
                ViewBag.Cargo = new SelectList(carg, "Id_Cargo", "Nombre_Cargo");
            }

            return View();

        }

        // POST: Contrato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Id,Name,SureName,PersonalID,Genre,Address,BirthDay,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LastName,Celular,Cargo,Contrato,Cdp,Equipo,Fecha_IniContrato,Fecha_FinContrato,Cedula,Nombres,Apellidos")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
            
                db.SaveChanges();
                return RedirectToAction("Seleccionar");
            }

            return View(aspNetUsers);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Contrato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SureName,PersonalID,Genre,Address,BirthDay,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LastName,Celular,Cargo,Contrato,Cdp,Equipo,Fecha_IniContrato,Fecha_FinContrato,Cedula,Nombres,Apellidos")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsers);
        }

        // GET: Contrato/Delete/5
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

        // POST: Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
