using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.Proyectos
{
    [Authorize]
    public class MetodoProyController : Controller
    {
        private investicEntities db = new investicEntities();


        // GET: MetodoProy/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMetodoProy tblMetodoProy = db.tblMetodoProy.Find(id);
            if (tblMetodoProy == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblCriticoSocial = new SelectList(db.tblCriticoSocial, "tblCriticoSocial_ID", "critSoc_nombre", tblMetodoProy.tblCriticoSocial);
            ViewBag.tblDisenioProy_ID = new SelectList(db.tblDiseniosProy, "tblDiseniosProy_ID", "disProy_nombre", tblMetodoProy.tblDisenioProy_ID);
            ViewBag.tblHistoricoHermeneutico = new SelectList(db.tblHistoricoHermeneutico, "tblHistoricoHermeneutico_ID", "hisHerm_nombre", tblMetodoProy.tblHistoricoHermeneutico);
            ViewBag.tblParadigmaEpistemologico_ID = new SelectList(db.tblParadigmaEpistemologico, "tblParadigmaEpistemologico_ID", "parEpi_nombre", tblMetodoProy.tblParadigmaEpistemologico_ID);
            ViewBag.tblParadigmaMetodologico_ID = new SelectList(db.tblParadigmaMetodologico, "tblParadigmaMetodologico_ID", "parMet_nombre", tblMetodoProy.tblParadigmaMetodologico_ID);
            ViewBag.tblTipoEstudioProy_ID = new SelectList(db.tblTipoEstudioProy, "tblTipoEstudioProy_ID", "tipEst_nombre", tblMetodoProy.tblTipoEstudioProy_ID);
            return View(tblMetodoProy);
        }

        // POST: MetodoProy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblMetodoProy_ID,tblParadigmaMetodologico_ID,metProy_paradigmaMetodologicoProy,tblParadigmaEpistemologico_ID,tblTipoEstudioProy_ID,tblDisenioProy_ID,tblHistoricoHermeneutico,tblCriticoSocial,metProy_paradigmaEpistemologicoProy,metProy_poblacionMuestraProy,metProy_tecnicasInstrumentosProy,metProy_procedimientoProy,metProy_planAnalisisDatosProy")] tblMetodoProy tblMetodoProy)
        {
            if (ModelState.IsValid)
            {
                tblProyectosInvestigacion miProyecto = (from t in db.tblProyectosInvestigacion
                                                        where t.tblMetodoProy_ID == tblMetodoProy.tblMetodoProy_ID
                                                        select t).FirstOrDefault();
                miProyecto.proyInv_fechaUltimaModificacion = DateTime.Now;
                db.Entry(miProyecto).State = EntityState.Modified;
                db.Entry(tblMetodoProy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = tblMetodoProy.tblMetodoProy_ID });
            }
            ViewBag.tblCriticoSocial = new SelectList(db.tblCriticoSocial, "tblCriticoSocial_ID", "critSoc_nombre", tblMetodoProy.tblCriticoSocial);
            ViewBag.tblDisenioProy_ID = new SelectList(db.tblDiseniosProy, "tblDiseniosProy_ID", "disProy_nombre", tblMetodoProy.tblDisenioProy_ID);
            ViewBag.tblHistoricoHermeneutico = new SelectList(db.tblHistoricoHermeneutico, "tblHistoricoHermeneutico_ID", "hisHerm_nombre", tblMetodoProy.tblHistoricoHermeneutico);
            ViewBag.tblParadigmaEpistemologico_ID = new SelectList(db.tblParadigmaEpistemologico, "tblParadigmaEpistemologico_ID", "parEpi_nombre", tblMetodoProy.tblParadigmaEpistemologico_ID);
            ViewBag.tblParadigmaMetodologico_ID = new SelectList(db.tblParadigmaMetodologico, "tblParadigmaMetodologico_ID", "parMet_nombre", tblMetodoProy.tblParadigmaMetodologico_ID);
            ViewBag.tblTipoEstudioProy_ID = new SelectList(db.tblTipoEstudioProy, "tblTipoEstudioProy_ID", "tipEst_nombre", tblMetodoProy.tblTipoEstudioProy_ID);
            return View(tblMetodoProy);
        }

        public JsonResult TipoEstudioList(string tblParadigmaEpistemologico_ID)
        {
            if (tblParadigmaEpistemologico_ID.Equals("0"))
            {
                return null;
            }
            else if (tblParadigmaEpistemologico_ID.Equals("1"))
            {
                var tblTipoEstudioProy = from t in db.tblTipoEstudioProy select t;
                return Json(new SelectList(tblTipoEstudioProy.ToArray(), "tblTipoEstudioProy_ID", "tipEst_nombre"), JsonRequestBehavior.AllowGet);

                //var tblTipoEstudioProy_ID = new SelectList(db.tblTipoEstudioProy, "tblTipoEstudioProy_ID", "tipEst_nombre");
                //return Json(new SelectList(tblTipoEstudioProy_ID.ToArray(), "tblTipoEstudioProy_ID", "tipEst_nombre"), JsonRequestBehavior.AllowGet);
            }
            else if (tblParadigmaEpistemologico_ID.Equals("2"))
            {
                var tblHistoricoHermeneutico = from t in db.tblHistoricoHermeneutico select t;
                return Json(new SelectList(tblHistoricoHermeneutico.ToArray(), "tblHistoricoHermeneutico_ID", "hisHerm_nombre"), JsonRequestBehavior.AllowGet);
            }
            else if (tblParadigmaEpistemologico_ID.Equals("3"))
            {
                var tblCriticoSocial = from t in db.tblCriticoSocial select t;
                return Json(new SelectList(tblCriticoSocial.ToArray(), "tblCriticoSocial_ID", "critSoc_nombre"), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult DisenioList(string tblTipoEstudioProy_ID)
        {
            if (tblTipoEstudioProy_ID.Equals("4"))
            {
                var tblDiseniosProy = from t in db.tblDiseniosProy select t;
                return Json(new SelectList(tblDiseniosProy.ToArray(), "tblDiseniosProy_ID", "disProy_nombre"), JsonRequestBehavior.AllowGet);
            }
            return null;
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
