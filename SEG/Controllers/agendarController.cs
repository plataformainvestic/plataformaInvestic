using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEG.Models.DataBase;
using SEG.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SEG.Controllers
{
    public class agendarController : Controller
    {
        private Entities db = new Entities();
       
        

        // GET: agendar
        public ActionResult Index()
        {
            var programa_tareas = db.programa_tareas.Include(p => p.AspNetUser).Include(p => p.responsabilidade).Include(p => p.tabla_alternativas);
            return View(programa_tareas.ToList());
        }
        //GET
        public ActionResult Seleccionar()
        {
            programa_tareas nueva = new programa_tareas();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());  

            int equipo= (from e in db.equipos where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

            string idequipo = equipo.ToString();
             
            List<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();        


            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");

            return View();

          

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seleccionar(programa_tareas nueva)
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
            programa_tareas programa_tareas = db.programa_tareas.Find(id);
            if (programa_tareas == null)
            {
                return HttpNotFound();
            }
            return View(programa_tareas);
        }

        // GET: agendar/Create;
        public ActionResult tarea()
        {
            
          
            
            string idusuario = TempData["IdContr"].ToString();

            if(idusuario==null)
            {
                return RedirectToAction("Seleccionar", "agendar");

            }
            List<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.Id.Equals(idusuario)).ToList();
            var nombrecontratista = (from n in db.AspNetUsers where n.Id.Equals(idusuario) select n.LastName).SingleOrDefault();

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");
            ViewData["nombrecom"] = nombrecontratista;



            
            //ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName");

            var contractual = db.responsabilidades.Where(y => y.Id_Contratista.Equals(idusuario)).ToList();          
            ViewBag.Responsabilidad = new SelectList(contractual, "Id", "Descripcion");

            ViewBag.Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa");


            TempData["IdContr"] = idusuario;
          
          
        
            return View();
        }

        // POST: agendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult tarea([Bind(Include = "Id,Id_Contratista,Nombre,Descripcion,Alternativa,Responsabilidad,Fecha_Ini,Fecha_Fin")] programa_tareas programa_tareas)
        {
            if (ModelState.IsValid)
            {
                programa_tareas.Estado = 1;
                db.programa_tareas.Add(programa_tareas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.responsabilidades, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // GET: agendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programa_tareas programa_tareas = db.programa_tareas.Find(id);
            if (programa_tareas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.responsabilidades, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // POST: agendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Nombre,Descripcion,Alternativa,Responsabilidad,Fecha_Corte")] programa_tareas programa_tareas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programa_tareas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", programa_tareas.Id_Contratista);
            ViewBag.Responsabilidad = new SelectList(db.responsabilidades, "Id", "Id_Contratista", programa_tareas.Responsabilidad);
            ViewBag.Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa", programa_tareas.Alternativa);
            return View(programa_tareas);
        }

        // GET: agendar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programa_tareas programa_tareas = db.programa_tareas.Find(id);
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
            programa_tareas programa_tareas = db.programa_tareas.Find(id);
            db.programa_tareas.Remove(programa_tareas);
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
