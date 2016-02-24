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
using SEG.Models;
using SEG.Models.DataBase;
using System.Data.Entity.Validation;
namespace SEG.Controllers
{
    public class actividadController : Controller
    {
        private Entities db = new Entities();

        // GET: actividad
        public ActionResult Index()
        {
           
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<actividade> actividades =
                db.actividades.Include(a => a.AspNetUser)
                    .Include(a => a.tabla_alternativas)
                    .Include(a => a.tabla_estados)
                    .Include(a => a.responsabilidade)
                    .Where(z => z.Id_Contratista.Equals(currentUser.Id));

            return View(actividades.ToList());
        }
        //GET
        public ActionResult SeleccionarTarea()
        {
           var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId()); 
 
            var tareas = db.programa_tareas.Where(p=>p.Id_Contratista.Equals(currentUser.Id)).Where(z=>z.Estado==1);

            

            if(tareas.Count()==0)
            {
                ViewBag.Message = "No hay tareas pendientes en el momento";
                ViewBag.Id = new SelectList(tareas, "Id", "Nombre");
            }
            else
            {
               
                    
            ViewBag.Id = new SelectList(tareas, "Id", "Nombre");
            }
            

            return View();

        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarTarea(programa_tareas ts)
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
            actividade actividade = db.actividades.Find(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // GET: actividad/Create
        public ActionResult Create(int id=0)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");

            if(TempData.Count!=0)
            { 
            //seleccion de alternativa de acuerdo a la tarea asignada
            string idtarea = TempData["IdTarea"].ToString();
            int idt = Convert.ToInt32(idtarea);
            var tasktodo = (from t in db.programa_tareas where t.Id == idt select t.Nombre).SingleOrDefault();
            var taskdesc = (from t in db.programa_tareas where t.Id == idt select t.Descripcion).SingleOrDefault();
            var fechac = (from t in db.programa_tareas where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
            DateTime fechanow = DateTime.Now;
            TimeSpan diference = fechac - fechanow;
            int days = (int)diference.TotalDays +1;
            

            int salternativa = (from s in db.programa_tareas where s.Id==idt select s.tabla_alternativas.Id_Alternativa).SingleOrDefault();
            var alts = db.tabla_alternativas.Where(q => q.Id_Alternativa.Equals(salternativa)).ToList();          
            ViewBag.Id_Alternativa = new SelectList(alts,"Id_Alternativa","Des_Alternativa");

            ViewData["tarea"] = tasktodo;
            ViewData["descrip"] = taskdesc;
            ViewData["fechacorte"] = fechac.ToString("dd/MM/yyyy");
            ViewData["dias"] = days;



            //seleccion de responsabilidad de acuerdo a la tarea asignada
            int sresponsa = (from r in db.programa_tareas where r.Id == idt select r.responsabilidade.Id).SingleOrDefault();
            var reps = db.responsabilidades.Where(h => h.Id.Equals(sresponsa)).ToList();
            ViewBag.Des_Resp_Contratista = new SelectList(reps, "Id", "Descripcion");
            TempData["IdTarea"] = idt;
            }
            else
            {
                ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa");

                List<responsabilidade> responsab = db.responsabilidades.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                ViewBag.Des_Resp_Contratista = new SelectList(responsab, "Id", "Descripcion");
            }
            

            

            
            
            
            ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado");


            actividade ac = new actividade();
            

            ac.Fecha_Ini = DateTime.Now;
            ac.Fecha_Fin = DateTime.Now;



            
            if (id != 0)
            {
                ac = db.actividades.Find(id);


            }
            return View(ac);
        }

        // POST: actividad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Actividad,Fecha_Ini,Fecha_Fin,Id_Contratista,Id_Alternativa,Des_Resp_Contratista,Des_Actividad,Id_Estado,Des_Observaciones")] actividade actividade)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            int status;
            
        
            if (ModelState.IsValid)
            {
                
                    string idtarea = TempData["IdTarea"].ToString();
                    int idt = Convert.ToInt32(idtarea);

           
                
               

                DateTime fechaInicial = (from f in db.fechasreportes
                                         where f.Activa == 1
                                         select f.Fecha_Ini).SingleOrDefault();

                DateTime fechaFinal = (from f in db.fechasreportes
                                       where f.Activa == 1
                                       select f.Fecha_Fin).SingleOrDefault();

                DateTime fechainicialactividad = (from f in db.programa_tareas where f.Id == idt select f.Fecha_Ini).SingleOrDefault();

                DateTime fechafinalactividad = (from f in db.programa_tareas where f.Id==idt select f.Fecha_Fin).SingleOrDefault();


                DateTime fechacorte = actividade.Fecha_Fin;
                
                


                DateTime fechaactual = DateTime.Now;

                DateTime tesgasds = fechaactual.AddDays(4);

                int diaactual = (int) fechaactual.DayOfWeek;
                int diaactuaal = (int)tesgasds.DayOfWeek;

                DateTime RFI = fechaactual;
                DateTime RFF= RFI.AddDays(6);

              


                int periodo1 = WeekMonth.GetWeekOfMonth(RFI);

                

    
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

                int periodo2 = WeekMonth.GetWeekNumberOfMonth(RFF);

                if ((fechaactual >= fechainicialactividad) && (fechaactual<= fechafinalactividad))
                {
                    if ((fechacorte>=fechainicialactividad) && (fechacorte<=fechafinalactividad))
                    {

                        try
                        {
                            actividade.Id_Contratista = currentUser.Id;
                            actividade.Periodo_Reporte = periodo1;
                            
                            status = actividade.Id_Estado;
                            db.actividades.Add(actividade);
                            db.SaveChanges();

                            var qry = from e in db.programa_tareas where e.Id.Equals(idt) select e;
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
                    ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa",
                        actividade.Id_Alternativa);
                    ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado", actividade.Id_Estado);
                    List<responsabilidade> responsab = db.responsabilidades.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                    ViewBag.Des_Resp_Contratista = new SelectList(responsab, "Id", "Descripcion");
                    TempData["IdTarea"] = idt;
                    //seleccion de alternativa de acuerdo a la tarea asignada
                    idtarea = TempData["IdTarea"].ToString();
                    idt = Convert.ToInt32(idtarea);
                    var tasktodo = (from t in db.programa_tareas where t.Id == idt select t.Nombre).SingleOrDefault();
                    var taskdesc = (from t in db.programa_tareas where t.Id == idt select t.Descripcion).SingleOrDefault();
                    var fechac = (from t in db.programa_tareas where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
                    DateTime fechanow = DateTime.Now;
                    TimeSpan diference = fechac - fechanow;
                    int days = (int)diference.TotalDays + 1;


                    int salternativa = (from s in db.programa_tareas where s.Id == idt select s.tabla_alternativas.Id_Alternativa).SingleOrDefault();
                    var alts = db.tabla_alternativas.Where(q => q.Id_Alternativa.Equals(salternativa)).ToList();
                    ViewBag.Id_Alternativa = new SelectList(alts, "Id_Alternativa", "Des_Alternativa");

                    ViewData["tarea"] = tasktodo;
                    ViewData["descrip"] = taskdesc;
                    ViewData["fechacorte"] = fechac.ToString("dd/MM/yyyy");
                    ViewData["dias"] = days;



                    //seleccion de responsabilidad de acuerdo a la tarea asignada
                    int sresponsa = (from r in db.programa_tareas where r.Id == idt select r.responsabilidade.Id).SingleOrDefault();
                    var reps = db.responsabilidades.Where(h => h.Id.Equals(sresponsa)).ToList();
                    ViewBag.Des_Resp_Contratista = new SelectList(reps, "Id", "Descripcion");
                    TempData["IdTarea"] = idt;

                    return View(actividade);
                }
                ViewBag.Message = "Periodo de Reporte Invalido!!";
                ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
                ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa",
                    actividade.Id_Alternativa);
                ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado", actividade.Id_Estado);
                List<responsabilidade> responsabi = db.responsabilidades.Where(t => t.Id_Contratista == currentUser.Id).ToList();
                ViewBag.Des_Resp_Contratista = new SelectList(responsabi, "Id", "Descripcion");
                TempData["IdTarea"] = idt;

                //seleccion de alternativa de acuerdo a la tarea asignada
                idtarea = TempData["IdTarea"].ToString();
                idt = Convert.ToInt32(idtarea);
                var tasktodob = (from t in db.programa_tareas where t.Id == idt select t.Nombre).SingleOrDefault();
                var taskdescb = (from t in db.programa_tareas where t.Id == idt select t.Descripcion).SingleOrDefault();
                var fechacb = (from t in db.programa_tareas where t.Id == idt select t.Fecha_Fin).SingleOrDefault();
                DateTime fechanowb = DateTime.Now;
                TimeSpan diferenceb = fechacb - fechanowb;
                int daysb = (int)diferenceb.TotalDays + 1;


                int salternativab = (from s in db.programa_tareas where s.Id == idt select s.tabla_alternativas.Id_Alternativa).SingleOrDefault();
                var altsb = db.tabla_alternativas.Where(q => q.Id_Alternativa.Equals(salternativab)).ToList();
                ViewBag.Id_Alternativa = new SelectList(altsb, "Id_Alternativa", "Des_Alternativa");

                ViewData["tarea"] = tasktodob;
                ViewData["descrip"] = taskdescb;
                ViewData["fechacorte"] = fechacb.ToString("dd/MM/yyyy");
                ViewData["dias"] = daysb;



                //seleccion de responsabilidad de acuerdo a la tarea asignada
                int sresponsab = (from r in db.programa_tareas where r.Id == idt select r.responsabilidade.Id).SingleOrDefault();
                var repsb = db.responsabilidades.Where(h => h.Id.Equals(sresponsab)).ToList();
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
            actividade actividade = db.actividades.Find(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
            ViewBag.Des_Resp_Contratista = new SelectList(db.responsabilidades, "Id", "Id_Contratista", actividade.Des_Resp_Contratista);
            ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa", actividade.Id_Alternativa);
            ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado", actividade.Id_Estado);
            return View(actividade);
        }

        // POST: actividad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Actividad,Fecha_Ini,Fecha_Fin,Id_Contratista,Id_Alternativa,Des_Resp_Contratista,Des_Actividad,Id_Estado,Des_Observaciones")] actividade actividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", actividade.Id_Contratista);
            ViewBag.Des_Resp_Contratista = new SelectList(db.responsabilidades, "Id", "Id_Contratista", actividade.Des_Resp_Contratista);
            ViewBag.Id_Alternativa = new SelectList(db.tabla_alternativas, "Id_Alternativa", "Des_Alternativa", actividade.Id_Alternativa);
            ViewBag.Id_Estado = new SelectList(db.tabla_estados, "Id_Estado", "Des_Estado", actividade.Id_Estado);
            return View(actividade);
        }

        // GET: actividad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividade actividade = db.actividades.Find(id);
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
            actividade actividade = db.actividades.Find(id);
            db.actividades.Remove(actividade);
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
            producto producto = db.productos.Find(id);
            
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
            
            producto producto = db.productos.Find(id);
            int idActividad = producto.Id_Actividad;
            db.productos.Remove(producto);
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
            evidencia evidencia = db.evidencias.Find(id);
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
            evidencia evidencia = db.evidencias.Find(id);
            int idActividad = evidencia.Id_Actividad;
            db.evidencias.Remove(evidencia);
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
