using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using INI.Models;
using INI.Models.DataBase;
using System.Data.Entity.Validation;
namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class actividadController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: actividad
        public ActionResult Index()
        {
           
            tblResponsabContratista rx = new tblResponsabContratista();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<tblActividadContratista> actividades =
                db.tblActividadContratista.Include(a => a.AspNetUsers)
                    .Include(a => a.tblAlternativas)
                    .Include(a => a.tblEstadoTarea)
                    .Include(a => a.tblResponsabContratista)
                    .Where(z => z.Id_Contratista.Equals(currentUser.Id));

            return View(actividades.ToList());
        }
        //GET
        [Authorize]
        public ActionResult SeleccionarTarea(int id=0)
        {
           if (AspNetUsersRoles.IsUserInRole("Contratista", User.Identity.Name))
           {

               var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
               ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

               var tareas = db.tblProgramaTareasContratista.Where(p => p.Id_Contratista.Equals(currentUser.Id)).Where(z => z.Estado == 1);



               if (tareas.Count() == 0)
               {
                   ViewBag.Message = "No hay tareas pendientes en el momento";
                   ViewBag.Id = new SelectList(tareas, "Id", "Nombre");
               }
               else
               {
                   ViewBag.Id = new SelectList(tareas, "Id", "Nombre");
                   return RedirectToAction("Seleccionar Tarea",new{id=id});
               }


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
        public ActionResult SeleccionarTarea(tblProgramaTareasContratista ts)
        {
            TempData["IdTarea"] = ts.Id;
            return RedirectToAction("Create", "actividad");
        }

        // GET: actividad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblActividadContratista actividade = db.tblActividadContratista.Find(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // GET: actividad/Create
        [Authorize]
        public ActionResult Create(int id=0)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");

            if(TempData.Count!=0)
            { 
            //seleccion de alternativa de acuerdo a la tarea asignada
            string idtarea = TempData["IdTarea"].ToString();
            int idt = Convert.ToInt32(idtarea);
            var tasktodo = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Nombre).SingleOrDefault();
            var taskdesc = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Descripcion).SingleOrDefault();
            var fechac = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
            DateTime fechanow = DateTime.Now;
            TimeSpan diference = fechac - fechanow;
            int days = (int)diference.TotalDays +1;


            int salternativa = (from s in db.tblProgramaTareasContratista where s.Id == idt select s.tblAlternativas.Id_Alternativa).SingleOrDefault();
            var alts = db.tblAlternativas.Where(q => q.Id_Alternativa.Equals(salternativa)).ToList();          
            ViewBag.Id_Alternativa = new SelectList(alts,"Id_Alternativa","Des_Alternativa");

            //ViewData["tarea"] = tasktodo;
            //ViewData["descrip"] = taskdesc;
            //ViewData["fechacorte"] = fechac.ToString("dd/MM/yyyy");
            //ViewData["dias"] = days;

       



            





            //seleccion de responsabilidad de acuerdo a la tarea asignada
            int sresponsa = (from r in db.tblProgramaTareasContratista where r.Id == idt select r.tblResponsabContratista.Id).SingleOrDefault();
            var reps = db.tblResponsabContratista.Where(h => h.Id.Equals(sresponsa)).ToList();
            ViewBag.Des_Resp_Contratista = new SelectList(reps, "Id", "Descripcion");
            TempData["IdTarea"] = idt;
            }
            else
            {
                ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa");

                List<tblResponsabContratista> responsab = db.tblResponsabContratista.Where(t => t.Id_Contratista == currentUser.Id).ToList();
             

                ViewBag.Des_Resp_Contratista = new SelectList(responsab, "Id", "Descripcion");
            }
            

            

            
            
            
            ViewBag.Id_Estado = new SelectList(db.tblEstadoTarea, "Id_Estado", "Des_Estado");


            tblActividadContratista ac = new tblActividadContratista();
            

            ac.Fecha_Ini = DateTime.Now;
            ac.Fecha_Fin = DateTime.Now;



            
            if (id != 0)
            {
                ac = db.tblActividadContratista.Find(id);
                ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa");

                List<tblResponsabContratista> responsab = db.tblResponsabContratista.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                ViewBag.Des_Resp_Contratista = new SelectList(responsab, "Id", "Descripcion");


                var tasktodo = (from t in db.tblProgramaTareasContratista where t.Id == id select t.Nombre).SingleOrDefault();
                var taskdesc = (from t in db.tblProgramaTareasContratista where t.Id == id select t.Descripcion).SingleOrDefault();
                var fechac = (from t in db.tblProgramaTareasContratista where t.Id == id select t.Fecha_Fin).SingleOrDefault();
                DateTime fechanow = DateTime.Now;
                TimeSpan diference = fechac - fechanow;
                int days = (int)diference.TotalDays + 1;


                ViewBag.tarea = tasktodo;
                ViewBag.descrip = taskdesc;
                ViewBag.fechacorte = fechac.ToString("dd/MM/yyyy");
                ViewBag.dias = days;



            }
            return View(ac);
        }

        // POST: actividad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Actividad,Fecha_Ini,Fecha_Fin,Id_Contratista,Id_Alternativa,Des_Resp_Contratista,Des_Actividad,Id_Estado,Des_Observaciones")] tblActividadContratista actividade)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            int status;
            int sw=0;
            
        
            if (ModelState.IsValid)
            {
                
                    string idtarea = TempData["IdTarea"].ToString();
                    int idt = Convert.ToInt32(idtarea);

           
                
               

                //DateTime fechaInicial = (from f in db.fechasreportes
                //                         where f.Activa == 1
                //                         select f.Fecha_Ini).SingleOrDefault();

                //DateTime fechaFinal = (from f in db.fechasreportes
                //                       where f.Activa == 1
                //                       select f.Fecha_Fin).SingleOrDefault();

                DateTime fechainicialactividad = (from f in db.tblProgramaTareasContratista where f.Id == idt select f.Fecha_Ini).SingleOrDefault();

                DateTime fechafinalactividad = (from f in db.tblProgramaTareasContratista where f.Id==idt select f.Fecha_Fin).SingleOrDefault();

                DateTime fechacorte = actividade.Fecha_Fin;
          
                

                if(fechacorte != DateTime.Parse("01/01/0001"))
                {
                    sw = 1;
                }

                DateTime fechaactual = DateTime.Now;

                DateTime tesgasds = fechaactual.AddDays(4);

                int diaactual = (int) fechaactual.DayOfWeek;
                int diaactuaal = (int)tesgasds.DayOfWeek;

                DateTime RFI = fechaactual;
                DateTime RFF= RFI.AddDays(6);

              


                int periodo1 = seg_WeekMonth.GetWeekOfMonth(RFI);

                

    
                //Domingo
                if (diaactual == 0)  
                {
                    RFF = fechaactual.AddDays(5);
                    RFI = fechaactual.AddDays(-1);
                    
                }
                //Lunes
                if (diaactual == 1)
                {
                    RFF = fechaactual.AddDays(4);
                    RFI = fechaactual.AddDays(-2);

                }
                //Martes
                if (diaactual == 2)
                {
                    RFF = fechaactual.AddDays(3);
                    RFI = fechaactual.AddDays(-3);

                }
                //Miercoles
                if (diaactual == 3)
                {
                    RFF = fechaactual.AddDays(2);
                    RFI = fechaactual.AddDays(-4);

                }
                //Jueves
                if (diaactual == 4)
                {
                    RFF = fechaactual.AddDays(1);
                    RFI = fechaactual.AddDays(-5);

                }
                //Viernes
                if (diaactual == 5)
                {
                    RFF = fechaactual.AddDays(0);
                    RFI = fechaactual.AddDays(-6);

                }
                //Sabado
                if (diaactual == 6)
                {
                    RFF = fechaactual.AddDays(6);
                    RFI = fechaactual.AddDays(0);

                }

                int periodo2 = seg_WeekMonth.GetWeekNumberOfMonth(RFF);






                if (sw == 1)
                {

                    if ((fechaactual >= fechainicialactividad) && (fechaactual <= fechafinalactividad))
                    {
                        if ((fechacorte >= fechainicialactividad) && (fechacorte <= fechafinalactividad))
                        {

                            try
                            {
                                actividade.Id_Contratista = currentUser.Id;
                                actividade.Periodo_Reporte = periodo1;

                                status = actividade.Id_Estado;
                                db.tblActividadContratista.Add(actividade);
                                db.SaveChanges();

                                var qry = from e in db.tblProgramaTareasContratista where e.Id.Equals(idt) select e;
                                var item = qry.Single();
                                item.Estado = status;
                                db.SaveChanges();

                                TempData["IdTarea"] = idt;

                                //


                                return RedirectToAction("Create", new { id = actividade.Id_Actividad });
                            }
                            catch (DbEntityValidationException e)
                            {

                                foreach (var eve in e.EntityValidationErrors)
                                {
                                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                            ve.PropertyName, ve.ErrorMessage);
                                    }
                                }
                                throw;
                            }

                        }
                        ViewBag.Message = "Periodo de Reporte Invalido!!";
                        ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
                        ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa",
                            actividade.Id_Alternativa);
                        ViewBag.Id_Estado = new SelectList(db.tblEstadoTarea, "Id_Estado", "Des_Estado", actividade.Id_Estado);
                        List<tblResponsabContratista> responsab = db.tblResponsabContratista.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                        ViewBag.Des_Resp_Contratista = new SelectList(responsab, "Id", "Descripcion");
                        TempData["IdTarea"] = idt;
                        //seleccion de alternativa de acuerdo a la tarea asignada
                        idtarea = TempData["IdTarea"].ToString();
                        idt = Convert.ToInt32(idtarea);
                        var tasktodo = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Nombre).SingleOrDefault();
                        var taskdesc = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Descripcion).SingleOrDefault();
                        var fechac = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
                        DateTime fechanow = DateTime.Now;
                        TimeSpan diference = fechac - fechanow;
                        int days = (int)diference.TotalDays + 1;


                        int salternativa = (from s in db.tblProgramaTareasContratista where s.Id == idt select s.tblAlternativas.Id_Alternativa).SingleOrDefault();
                        var alts = db.tblAlternativas.Where(q => q.Id_Alternativa.Equals(salternativa)).ToList();
                        ViewBag.Id_Alternativa = new SelectList(alts, "Id_Alternativa", "Des_Alternativa");

                        ViewData["tarea"] = tasktodo;
                        ViewData["descrip"] = taskdesc;
                        ViewData["fechacorte"] = fechac.ToString("dd/MM/yyyy");
                        ViewData["dias"] = days;



                        //seleccion de responsabilidad de acuerdo a la tarea asignada
                        int sresponsa = (from r in db.tblProgramaTareasContratista where r.Id == idt select r.tblResponsabContratista.Id).SingleOrDefault();
                        var reps = db.tblResponsabContratista.Where(h => h.Id.Equals(sresponsa)).ToList();
                        ViewBag.Des_Resp_Contratista = new SelectList(reps, "Id", "Descripcion");
                        TempData["IdTarea"] = idt;

                        return View(actividade);
                    }
                }
                
                ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
                ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa",
                    actividade.Id_Alternativa);
                ViewBag.Id_Estado = new SelectList(db.tblEstadoTarea, "Id_Estado", "Des_Estado", actividade.Id_Estado);
                List<tblResponsabContratista> responsabi = db.tblResponsabContratista.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                ViewBag.Des_Resp_Contratista = new SelectList(responsabi, "Id", "Descripcion");
                TempData["IdTarea"] = idt;

                //seleccion de alternativa de acuerdo a la tarea asignada
                idtarea = TempData["IdTarea"].ToString();
                idt = Convert.ToInt32(idtarea);
                var tasktodob = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Nombre).SingleOrDefault();
                var taskdescb = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Descripcion).SingleOrDefault();
                var fechacb = (from t in db.tblProgramaTareasContratista where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
                DateTime fechanowb = DateTime.Now;
                TimeSpan diferenceb = fechacb - fechanowb;
                int daysb = (int)diferenceb.TotalDays + 1;


                int salternativab = (from s in db.tblProgramaTareasContratista where s.Id == idt select s.tblAlternativas.Id_Alternativa).SingleOrDefault();
                var altsb = db.tblAlternativas.Where(q => q.Id_Alternativa.Equals(salternativab)).ToList();
                ViewBag.Id_Alternativa = new SelectList(altsb, "Id_Alternativa", "Des_Alternativa");

                ViewData["tarea"] = tasktodob;
                ViewData["descrip"] = taskdescb;
                ViewData["fechacorte"] = fechacb.ToString("dd/MM/yyyy");
                ViewData["dias"] = daysb;



                //seleccion de responsabilidad de acuerdo a la tarea asignada
                int sresponsab = (from r in db.tblProgramaTareasContratista where r.Id == idt select r.tblResponsabContratista.Id).SingleOrDefault();
                var repsb = db.tblResponsabContratista.Where(h => h.Id.Equals(sresponsab)).ToList();
                ViewBag.Des_Resp_Contratista = new SelectList(repsb, "Id", "Descripcion");
                TempData["IdTarea"] = idt;

                return View(actividade);
            }


      //      ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
        //    ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa",
          //      actividade.Id_Alternativa);
            //ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado", actividade.Id_Estado);
            //List<responsabilidade> res =
              //  db.responsabilidades.Where(t => t.Id_Contratista == currentUser.Id).ToList();
            //ViewBag.Des_Resp_Contratista = new SelectList(res, "Id", "Descripcion");
            return View(actividade);
        }

        // GET: actividad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblActividadContratista actividade = db.tblActividadContratista.Find(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
            ViewBag.Des_Resp_Contratista = new SelectList(db.tblResponsabContratista, "Id", "Id_Contratista", actividade.Des_Resp_Contratista);
            ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa", actividade.Id_Alternativa);
            ViewBag.Id_Estado = new SelectList(db.tblEstadoTarea, "Id_Estado", "Des_Estado", actividade.Id_Estado);
            return View(actividade);
        }

        // POST: actividad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Actividad,Fecha_Ini,Fecha_Fin,Id_Contratista,Id_Alternativa,Des_Resp_Contratista,Des_Actividad,Id_Estado,Des_Observaciones")] tblActividadContratista actividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
            ViewBag.Des_Resp_Contratista = new SelectList(db.tblResponsabContratista, "Id", "Id_Contratista", actividade.Des_Resp_Contratista);
            ViewBag.Id_Alternativa = new SelectList(db.tblAlternativas, "Id_Alternativa", "Des_Alternativa", actividade.Id_Alternativa);
            ViewBag.Id_Estado = new SelectList(db.tblEstadoTarea, "Id_Estado", "Des_Estado", actividade.Id_Estado);
            return View(actividade);
        }

        // GET: actividad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblActividadContratista actividade = db.tblActividadContratista.Find(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // POST: actividad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblActividadContratista actividade = db.tblActividadContratista.Find(id);
            db.tblActividadContratista.Remove(actividade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: produto/Delete/5
        public ActionResult BorrarProducto(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductosContratista producto = db.tblProductosContratista.Find(id);
            
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: produto/Delete/5
        [HttpPost, ActionName("BorrarProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarProductoConfirmed(int id)
        {
            
            tblProductosContratista producto = db.tblProductosContratista.Find(id);
            int idActividad = producto.Id_Actividad;
            db.tblProductosContratista.Remove(producto);
            db.SaveChanges();             
            return RedirectToAction("Create","actividad", new {id=idActividad});

        }

        // GET: evidencias/Delete/5
        public ActionResult BorrarEvidencia(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvidenciasContratista evidencia = db.tblEvidenciasContratista.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            return View(evidencia);
        }

        // POST: evidencias/Delete/5
        [HttpPost, ActionName("BorrarEvidencia")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarEvidenciaConfirmed(int id)
        {
            tblEvidenciasContratista evidencia = db.tblEvidenciasContratista.Find(id);
            int idActividad = evidencia.Id_Actividad;
            db.tblEvidenciasContratista.Remove(evidencia);
            db.SaveChanges();
            return RedirectToAction("Create", "actividad", new { id = idActividad });
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
