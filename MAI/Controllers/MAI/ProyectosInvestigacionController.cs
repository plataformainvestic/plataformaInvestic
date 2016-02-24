using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.Investigacion
{
    [Authorize]
    public class ProyectosInvestigacionController : Controller
    {
        private investicEntities db = new investicEntities();

        public ActionResult Index(long? id)//recibe el id del grupo de investigacion
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGruposInvestigacion miGrupo = db.tblGruposInvestigacion.Find(id);
            if (miGrupo == null)
            {
                return HttpNotFound();
            }
            return View(miGrupo);
        }

        public ActionResult Proyectos(long? id)//recibe el id del grupo de investigacion
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGruposInvestigacion miGrupo = db.tblGruposInvestigacion.Find(id);
            if (miGrupo == null)
            {
                return HttpNotFound();
            }

            ViewBag.tblGruposInvestigacion_ID = id;
            return View(miGrupo);
        }

        public ActionResult CrearProyecto(long? id) //recibe el id del grupo de Investigacion
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGruposInvestigacion miGrupo = db.tblGruposInvestigacion.Find(id);
            if (miGrupo == null)
            {
                return HttpNotFound();
            }

            ViewBag.tblGruposInvestigacion_ID = id;
            return View(miGrupo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearProyecto([Bind(Include = "tblProyectosInvestigacion_ID,tblGruposInvestigacion_ID,proyInv_nombreProyecto,proInv_fechaCreacion,proInv_fechaUltimaModificacion")] tblProyectosInvestigacion tblProyectosInvestigacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!tblProyectosInvestigacion.proyInv_nombreProyecto.Equals(null))
                    {
                        tblProyectosInvestigacion.tblEstado_ID = 1; //Estado Activo
                        tblProyectosInvestigacion.proyInv_fechaCreacion = DateTime.Now;
                        tblProyectosInvestigacion.proyInv_fechaUltimaModificacion = DateTime.Now;
                        //Agregar Tablas al Proyecto
                        addTablasProyecto(tblProyectosInvestigacion);
                        //Agregar Tablas de Revision
                        //addTablasRevProyecto(tblProyectosInvestigacion);
                        //-------------Revision
                        tblProyectosInvestigacion_Rev rev = evaluacionProyecto(tblProyectosInvestigacion);
                        tblProyectosInvestigacion_Rev rev1 = evaluacionProyecto(tblProyectosInvestigacion);
                        tblProyectosInvestigacion_Rev rev2 = evaluacionProyecto(tblProyectosInvestigacion);
                        tblProyectosInvestigacion_Rev rev3 = evaluacionProyecto(tblProyectosInvestigacion);

                        tblProyectosInvestigacion.tblProyectosInvestigacion_Rev = rev;
                        tblProyectosInvestigacion.tblProyectosInvestigacion_Rev1 = rev1;
                        tblProyectosInvestigacion.tblProyectosInvestigacion_Rev2 = rev2;
                        tblProyectosInvestigacion.tblProyectosInvestigacion_Rev3 = rev3;

                        //--------------------- 


                        db.tblProyectosInvestigacion.Add(tblProyectosInvestigacion);
                        tblGruposInvestigacion miGrupoInv = db.tblGruposInvestigacion.Find(tblProyectosInvestigacion.tblGruposInvestigacion_ID);
                        miGrupoInv.gruInv_proyectos = miGrupoInv.gruInv_proyectos + 1;
                        db.Entry(miGrupoInv).State = EntityState.Modified;
                        db.SaveChanges();

                        //Guardar revisions
                        rev.tblProyectosInvestigacion_ID = tblProyectosInvestigacion.tblProyectosInvestigacion_ID;
                        rev1.tblProyectosInvestigacion_ID = tblProyectosInvestigacion.tblProyectosInvestigacion_ID;
                        rev2.tblProyectosInvestigacion_ID = tblProyectosInvestigacion.tblProyectosInvestigacion_ID;
                        rev3.tblProyectosInvestigacion_ID = tblProyectosInvestigacion.tblProyectosInvestigacion_ID;

                        db.Entry(rev).State = EntityState.Modified;
                        db.Entry(rev1).State = EntityState.Modified;
                        db.Entry(rev2).State = EntityState.Modified;
                        db.Entry(rev3).State = EntityState.Modified;
                        db.SaveChanges();
                        
                        return RedirectToAction("Proyectos", new { id = tblProyectosInvestigacion.tblGruposInvestigacion_ID });
                    }
                }
                catch (Exception)
                {
                    //throw;
                    return RedirectToAction("Proyectos", new { id = tblProyectosInvestigacion.tblGruposInvestigacion_ID });
                }
            }
            tblGruposInvestigacion miGrupo = db.tblGruposInvestigacion.Find(tblProyectosInvestigacion.tblGruposInvestigacion_ID);
            return View(miGrupo);
        }

        private void addTablasProyecto(tblProyectosInvestigacion tblProyectosInvestigacion)
        {
            //tabla presentacion proyecto
            tblPresentacionProyecto presentacionProy = new tblPresentacionProyecto();
            presentacionProy.preProy_tituloProy = tblProyectosInvestigacion.proyInv_nombreProyecto;
            db.tblPresentacionProyecto.Add(presentacionProy);
            //tabla problema de investigacion
            tblProblemaInvestigacionProy problemaProy = new tblProblemaInvestigacionProy();
            db.tblProblemaInvestigacionProy.Add(problemaProy);
            //tabla marco de referencia
            tblMarcoReferenciaProy marcoProy = new tblMarcoReferenciaProy();
            db.tblMarcoReferenciaProy.Add(marcoProy);
            //tabla metodo
            tblMetodoProy metodoProy = new tblMetodoProy();
            db.tblMetodoProy.Add(metodoProy);
            //tabla caracteristicas
            tblCaracteristicasProy caractProy = new tblCaracteristicasProy();
            db.tblCaracteristicasProy.Add(caractProy);
            //tabla cronograma
            tblCronogramaProy cronogramaProy = new tblCronogramaProy();
            db.tblCronogramaProy.Add(cronogramaProy);
            //tabla presupuesto
            tblPresupuestoProy presupuestoProy = new tblPresupuestoProy();
            db.tblPresupuestoProy.Add(presupuestoProy);
            //tabla referencias
            tblReferenciasProy referenciasProy = new tblReferenciasProy();
            db.tblReferenciasProy.Add(referenciasProy);

            db.SaveChanges();

            tblProyectosInvestigacion.tblPresentacionProyecto = presentacionProy;
            tblProyectosInvestigacion.tblProblemaInvestigacionProy = problemaProy;
            tblProyectosInvestigacion.tblMarcoReferenciaProy = marcoProy;
            tblProyectosInvestigacion.tblMetodoProy = metodoProy;
            tblProyectosInvestigacion.tblCaracteristicasProy = caractProy;
            tblProyectosInvestigacion.tblCronogramaProy = cronogramaProy;
            tblProyectosInvestigacion.tblPresupuestoProy = presupuestoProy;
            tblProyectosInvestigacion.tblReferenciasProy = referenciasProy;

        }

        private void addTablasRevProyecto(tblProyectosInvestigacion tblProyectosInvestigacion)
        {
            tblProyectosInvestigacion.tblProyectosInvestigacion_Rev = evaluacionProyecto(tblProyectosInvestigacion);
            tblProyectosInvestigacion.tblProyectosInvestigacion_Rev1 = evaluacionProyecto(tblProyectosInvestigacion);
            tblProyectosInvestigacion.tblProyectosInvestigacion_Rev2 = evaluacionProyecto(tblProyectosInvestigacion);
            tblProyectosInvestigacion.tblProyectosInvestigacion_Rev3 = evaluacionProyecto(tblProyectosInvestigacion);

            //tblProyectosInvestigacion.tblEvaluacionProyColciencias_ID = evaluacionProyecto(tblProyectosInvestigacion);
            //tblProyectosInvestigacion.tblEvaluacionProyInvestic_ID = evaluacionProyecto(tblProyectosInvestigacion);
            //tblProyectosInvestigacion.tblEvaluacionProyEvaluador1_ID = evaluacionProyecto(tblProyectosInvestigacion);
            //tblProyectosInvestigacion.tblEvaluacionProyEvaluador2_ID = evaluacionProyecto(tblProyectosInvestigacion);
        }

        private tblProyectosInvestigacion_Rev evaluacionProyecto(tblProyectosInvestigacion tblProyectosInvestigacion)
        {
            //Tablas de Revision
            tblPresentacionProyecto_Rev revPresentacionProyecto = new tblPresentacionProyecto_Rev();
            db.tblPresentacionProyecto_Rev.Add(revPresentacionProyecto);
            tblProblemaInvestigacionProy_Rev revProblemaProy = new tblProblemaInvestigacionProy_Rev();
            db.tblProblemaInvestigacionProy_Rev.Add(revProblemaProy);
            tblMarcoReferenciaProy_Rev revMarcoProy = new tblMarcoReferenciaProy_Rev();
            db.tblMarcoReferenciaProy_Rev.Add(revMarcoProy);
            tblMetodoProy_Rev revMetodoProy = new tblMetodoProy_Rev();
            db.tblMetodoProy_Rev.Add(revMetodoProy);
            tblCaracteristicasProy_Rev revCaracteristicasProy = new tblCaracteristicasProy_Rev();
            db.tblCaracteristicasProy_Rev.Add(revCaracteristicasProy);
            tblCronogramaProy_Rev revCronogramaProy = new tblCronogramaProy_Rev();
            db.tblCronogramaProy_Rev.Add(revCronogramaProy);
            tblPresupuestoProy_Rev revPresupuestoProy = new tblPresupuestoProy_Rev();
            db.tblPresupuestoProy_Rev.Add(revPresupuestoProy);
            tblReferenciasProy_Rev revReferenciasProy = new tblReferenciasProy_Rev();
            db.tblReferenciasProy_Rev.Add(revReferenciasProy);
            //Guardar tablas de revision
            db.SaveChanges();

            //Asignar tablas a revision del proyecto
            tblProyectosInvestigacion_Rev revision = new tblProyectosInvestigacion_Rev();
            revision.tblPresentacionProyecto_Rev = revPresentacionProyecto;
            revision.tblProblemaInvestigacionProy_Rev = revProblemaProy;
            revision.tblMarcoReferenciaProy_Rev = revMarcoProy;
            revision.tblMetodoProy_Rev = revMetodoProy;
            revision.tblCaracteristicasProy_Rev = revCaracteristicasProy;
            revision.tblCronogramaProy_Rev = revCronogramaProy;
            revision.tblPresupuestoProy_Rev = revPresupuestoProy;
            revision.tblReferenciasProy_Rev = revReferenciasProy;
            revision.tblEstado_ID = 6; //Sin asignacion

            db.tblProyectosInvestigacion_Rev.Add(revision);
            db.SaveChanges();

            //return revision.tblProyectosInvestigacion_Rev_ID;
            return revision;
        }

        public ActionResult EditarProyecto(long? id)//Recibe el id del proyecto
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProyectosInvestigacion miProyecto = db.tblProyectosInvestigacion.Find(id);
            if (miProyecto == null)
            {
                return HttpNotFound();
            }
            return View(miProyecto);
        }

        public ActionResult InformeProyecto(long? id)//Recibe el id del proyecto para presentar el informe final
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProyectosInvestigacion miProyecto = db.tblProyectosInvestigacion.Find(id);
            if (miProyecto == null)
            {
                return HttpNotFound();
            }
            return View(miProyecto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InformeProyecto([Bind(Include = "tblProyectoInvestigacion_ID")] tblProyectosInvestigacion tblProyectosInvestigacion)
        {
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion.tblEstado_ID = 7; //Finalizado, Listo para revision
                db.Entry(tblProyectosInvestigacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetallesGrupo", "GruposInvestigacion", new { id = tblProyectosInvestigacion.tblGruposInvestigacion_ID });
            }
            return RedirectToAction("DetallesGrupo", "GruposInvestigacion", new { id = tblProyectosInvestigacion.tblGruposInvestigacion_ID });
        }

        public ActionResult PromoverProyecto(long? id)
        {
            tblProyectosInvestigacion miProyecto = db.tblProyectosInvestigacion.Find(id);
            if (miProyecto == null)
            {
                return HttpNotFound();
            }
            miProyecto.tblEstado_ID = 7; //Finaliado, listo para revision
            db.Entry(miProyecto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DetallesGrupo", "GruposInvestigacion", new { id = miProyecto.tblGruposInvestigacion_ID });
        }


        public JsonResult EliminarProyeto(long? id)
        {
            bool succes;
            try
            {
                tblProyectosInvestigacion tblProyectosInvestigacion = db.tblProyectosInvestigacion.Find(id);
                tblProyectosInvestigacion.tblEstado_ID = 2;
                tblGruposInvestigacion grupo = db.tblGruposInvestigacion.Find(tblProyectosInvestigacion.tblGruposInvestigacion_ID);
                grupo.gruInv_proyectos = grupo.gruInv_proyectos - 1;

                db.Entry(grupo).State = EntityState.Modified;
                db.Entry(tblProyectosInvestigacion).State = EntityState.Modified;
                db.SaveChanges();
                succes = true;
            }
            catch (Exception)
            {
                succes = false;
                throw;
            }

            return Json(succes, JsonRequestBehavior.AllowGet);
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