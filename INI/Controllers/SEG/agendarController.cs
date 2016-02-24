using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using INI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class agendarController : Controller
    {
        private investicEntities db = new investicEntities();


        [Authorize]
        // GET: agendar
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var programa_tareas = db.tblProgramaTareasContratista.Where(p=>p.AspNetUsers.Id.Equals(user)).Include(p => p.AspNetUsers).Include(p => p.tblResponsabContratista).Include(p => p.tblAlternativas);
            return View(programa_tareas.ToList());
                    
            

           
        }
        //GET
        [Authorize]
        public ActionResult Seleccionar()
        {

            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {

                tblProgramaTareasContratista nueva = new tblProgramaTareasContratista();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                int equipo = (from e in db.tblEquiposTrabajo where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seleccionar(tblProgramaTareasContratista nueva)
        {
           

            TempData["IdContr"] = nueva.Id_Contratista;
            return RedirectToAction("tarea", "agendar");
            
        }
    


        // GET: agendar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProgramaTareasContratista programa_tareas = db.tblProgramaTareasContratista.Find(id);
            if (programa_tareas == null)
            {
                return HttpNotFound();
            }
            return View(programa_tareas);
        }

        // GET: agendar/Create;
        [Authorize]
        public ActionResult tarea()
        {                                  
            string idusuario = TempData["IdContr"].ToString();

            if(idusuario==null)
            {
                return RedirectToAction("Seleccionar", "agendar");

            }
            List<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.Id.Equals(idusuario)).ToList();
            var nombrecontratista = (from n in db.AspNetUsers where n.Id.Equals(idusuario) select n.LastName).SingleOrDefault();

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");
            ViewData["nombrecom"] = nombrecontratista;



            
            //ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName");

            var contractual = db.tblResponsabContratista.Where(y => y.Id_Contratista.Equals(idusuario)).ToList();          
            ViewBag.Responsabilidad = new SelectList(contractual, "Id", "Descripcion");

            ViewBag.Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa");


            TempData["IdContr"] = idusuario;
          
          
        
            return View();
        }

        // POST: agendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult tarea([Bind(Include = "Id,Id_Contratista,Nombre,Descripcion,Alternativa,Responsabilidad,Fecha_Ini,Fecha_Fin")] tblProgramaTareasContratista programa_tareas)
        {
            if (ModelState.IsValid)
            {
                programa_tareas.Estado = 1;
                db.tblProgramaTareasContratista.Add(programa_tareas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.tblResponsabContratista, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // GET: agendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProgramaTareasContratista programa_tareas = db.tblProgramaTareasContratista.Find(id);
            if (programa_tareas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.tblResponsabContratista, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // POST: agendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Nombre,Descripcion,Alternativa,Responsabilidad,Fecha_Corte")] tblProgramaTareasContratista programa_tareas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programa_tareas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.tblResponsabContratista, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // GET: agendar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProgramaTareasContratista programa_tareas = db.tblProgramaTareasContratista.Find(id);
            if (programa_tareas == null)
            {
                return HttpNotFound();
            }
            return View(programa_tareas);
        }

        // POST: agendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProgramaTareasContratista programa_tareas = db.tblProgramaTareasContratista.Find(id);
            db.tblProgramaTareasContratista.Remove(programa_tareas);
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
