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
    public class ProyectosInvestigacion_RevController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: ProyectosInvestigacion_Rev
        public ActionResult MisProyectosRevision()//Con el codigo de usuario revisa sus proyectos de evaluación
        {
            //UserID
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            var misProyectosRevision = (from t in db.tblProyectosInvestigacion_Rev
                                        where
                                        t.tblUsuarioPlataforma_Evaluador_ID == idUsuario &&
                                        t.tblEstado_ID == 4
                                        select t).ToList();

            return View(misProyectosRevision);
        }











        // GET: ProyectosInvestigacion_Rev/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev = db.tblProyectosInvestigacion_Rev.Find(id);
            if (tblProyectosInvestigacion_Rev == null)
            {
                return HttpNotFound();
            }
            return View(tblProyectosInvestigacion_Rev);
        }

        // GET: ProyectosInvestigacion_Rev/Create
        public ActionResult Create()
        {
            ViewBag.tblCaracteristicasProy_Rev_ID = new SelectList(db.tblCaracteristicasProy_Rev, "tblCaracteristicasProy_Rev_ID", "carProy_resultadosEsperadosProy");
            ViewBag.tblCronogramaProy_Rev_ID = new SelectList(db.tblCronogramaProy_Rev, "tblCronogramaProy_Rev_ID", "croProy_revision");
            ViewBag.tblEstado_ID = new SelectList(db.tblEstado, "tblEstado_ID", "est_nombre");
            ViewBag.tblMarcoReferenciaProy_Rev_ID = new SelectList(db.tblMarcoReferenciaProy_Rev, "tblMarcoReferenciaProy_Rev_ID", "marRefProy_marcoTeoricoProy");
            ViewBag.tblMetodoProy_Rev_ID = new SelectList(db.tblMetodoProy_Rev, "tblMetodoProy_Rev_ID", "metProy_paradigmaMetodologicoProy");
            ViewBag.tblPresentacionProyecto_Rev_ID = new SelectList(db.tblPresentacionProyecto_Rev, "tblPresentacionProyecto_Rev_ID", "preProy_tituloProy");
            ViewBag.tblPresupuestoProy_Rev_ID = new SelectList(db.tblPresupuestoProy_Rev, "tblPresupuestoProy_Rev_ID", "preProy_revision");
            ViewBag.tblProblemaInvestigacionProy_Rev_ID = new SelectList(db.tblProblemaInvestigacionProy_Rev, "tblProblemaInvestigacionProy_Rev_ID", "proInvProy_planteamientoProblemaProy");
            ViewBag.tblReferenciasProy_Rev_ID = new SelectList(db.tblReferenciasProy_Rev, "tblReferenciasProy_Rev_ID", "refProy_referencias");
            return View();
        }

        // POST: ProyectosInvestigacion_Rev/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblProyectosInvestigacion_Rev_ID,tblProyectosInvestigacion_ID,tblEstado_ID,tblPresentacionProyecto_Rev_ID,tblProblemaInvestigacionProy_Rev_ID,tblMarcoReferenciaProy_Rev_ID,tblMetodoProy_Rev_ID,tblCaracteristicasProy_Rev_ID,tblCronogramaProy_Rev_ID,tblPresupuestoProy_Rev_ID,tblReferenciasProy_Rev_ID,tblUsuarioPlataforma_Evaluador_ID,proyInvRev_fechaAsignacion,proyInvRev_fechaLimiteEvaluacion,proyInvRev_fechaUltimaEvaluacion")] tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev)
        {
            if (ModelState.IsValid)
            {
                db.tblProyectosInvestigacion_Rev.Add(tblProyectosInvestigacion_Rev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tblCaracteristicasProy_Rev_ID = new SelectList(db.tblCaracteristicasProy_Rev, "tblCaracteristicasProy_Rev_ID", "carProy_resultadosEsperadosProy", tblProyectosInvestigacion_Rev.tblCaracteristicasProy_Rev_ID);
            ViewBag.tblCronogramaProy_Rev_ID = new SelectList(db.tblCronogramaProy_Rev, "tblCronogramaProy_Rev_ID", "croProy_revision", tblProyectosInvestigacion_Rev.tblCronogramaProy_Rev_ID);
            ViewBag.tblEstado_ID = new SelectList(db.tblEstado, "tblEstado_ID", "est_nombre", tblProyectosInvestigacion_Rev.tblEstado_ID);
            ViewBag.tblMarcoReferenciaProy_Rev_ID = new SelectList(db.tblMarcoReferenciaProy_Rev, "tblMarcoReferenciaProy_Rev_ID", "marRefProy_marcoTeoricoProy", tblProyectosInvestigacion_Rev.tblMarcoReferenciaProy_Rev_ID);
            ViewBag.tblMetodoProy_Rev_ID = new SelectList(db.tblMetodoProy_Rev, "tblMetodoProy_Rev_ID", "metProy_paradigmaMetodologicoProy", tblProyectosInvestigacion_Rev.tblMetodoProy_Rev_ID);
            ViewBag.tblPresentacionProyecto_Rev_ID = new SelectList(db.tblPresentacionProyecto_Rev, "tblPresentacionProyecto_Rev_ID", "preProy_tituloProy", tblProyectosInvestigacion_Rev.tblPresentacionProyecto_Rev_ID);
            ViewBag.tblPresupuestoProy_Rev_ID = new SelectList(db.tblPresupuestoProy_Rev, "tblPresupuestoProy_Rev_ID", "preProy_revision", tblProyectosInvestigacion_Rev.tblPresupuestoProy_Rev_ID);
            ViewBag.tblProblemaInvestigacionProy_Rev_ID = new SelectList(db.tblProblemaInvestigacionProy_Rev, "tblProblemaInvestigacionProy_Rev_ID", "proInvProy_planteamientoProblemaProy", tblProyectosInvestigacion_Rev.tblProblemaInvestigacionProy_Rev_ID);
            ViewBag.tblReferenciasProy_Rev_ID = new SelectList(db.tblReferenciasProy_Rev, "tblReferenciasProy_Rev_ID", "refProy_referencias", tblProyectosInvestigacion_Rev.tblReferenciasProy_Rev_ID);
            return View(tblProyectosInvestigacion_Rev);
        }

        // GET: ProyectosInvestigacion_Rev/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev = db.tblProyectosInvestigacion_Rev.Find(id);
            if (tblProyectosInvestigacion_Rev == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblCaracteristicasProy_Rev_ID = new SelectList(db.tblCaracteristicasProy_Rev, "tblCaracteristicasProy_Rev_ID", "carProy_resultadosEsperadosProy", tblProyectosInvestigacion_Rev.tblCaracteristicasProy_Rev_ID);
            ViewBag.tblCronogramaProy_Rev_ID = new SelectList(db.tblCronogramaProy_Rev, "tblCronogramaProy_Rev_ID", "croProy_revision", tblProyectosInvestigacion_Rev.tblCronogramaProy_Rev_ID);
            ViewBag.tblEstado_ID = new SelectList(db.tblEstado, "tblEstado_ID", "est_nombre", tblProyectosInvestigacion_Rev.tblEstado_ID);
            ViewBag.tblMarcoReferenciaProy_Rev_ID = new SelectList(db.tblMarcoReferenciaProy_Rev, "tblMarcoReferenciaProy_Rev_ID", "marRefProy_marcoTeoricoProy", tblProyectosInvestigacion_Rev.tblMarcoReferenciaProy_Rev_ID);
            ViewBag.tblMetodoProy_Rev_ID = new SelectList(db.tblMetodoProy_Rev, "tblMetodoProy_Rev_ID", "metProy_paradigmaMetodologicoProy", tblProyectosInvestigacion_Rev.tblMetodoProy_Rev_ID);
            ViewBag.tblPresentacionProyecto_Rev_ID = new SelectList(db.tblPresentacionProyecto_Rev, "tblPresentacionProyecto_Rev_ID", "preProy_tituloProy", tblProyectosInvestigacion_Rev.tblPresentacionProyecto_Rev_ID);
            ViewBag.tblPresupuestoProy_Rev_ID = new SelectList(db.tblPresupuestoProy_Rev, "tblPresupuestoProy_Rev_ID", "preProy_revision", tblProyectosInvestigacion_Rev.tblPresupuestoProy_Rev_ID);
            ViewBag.tblProblemaInvestigacionProy_Rev_ID = new SelectList(db.tblProblemaInvestigacionProy_Rev, "tblProblemaInvestigacionProy_Rev_ID", "proInvProy_planteamientoProblemaProy", tblProyectosInvestigacion_Rev.tblProblemaInvestigacionProy_Rev_ID);
            ViewBag.tblReferenciasProy_Rev_ID = new SelectList(db.tblReferenciasProy_Rev, "tblReferenciasProy_Rev_ID", "refProy_referencias", tblProyectosInvestigacion_Rev.tblReferenciasProy_Rev_ID);
            return View(tblProyectosInvestigacion_Rev);
        }

        // POST: ProyectosInvestigacion_Rev/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblProyectosInvestigacion_Rev_ID,tblProyectosInvestigacion_ID,tblEstado_ID,tblPresentacionProyecto_Rev_ID,tblProblemaInvestigacionProy_Rev_ID,tblMarcoReferenciaProy_Rev_ID,tblMetodoProy_Rev_ID,tblCaracteristicasProy_Rev_ID,tblCronogramaProy_Rev_ID,tblPresupuestoProy_Rev_ID,tblReferenciasProy_Rev_ID,tblUsuarioPlataforma_Evaluador_ID,proyInvRev_fechaAsignacion,proyInvRev_fechaLimiteEvaluacion,proyInvRev_fechaUltimaEvaluacion")] tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProyectosInvestigacion_Rev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tblCaracteristicasProy_Rev_ID = new SelectList(db.tblCaracteristicasProy_Rev, "tblCaracteristicasProy_Rev_ID", "carProy_resultadosEsperadosProy", tblProyectosInvestigacion_Rev.tblCaracteristicasProy_Rev_ID);
            ViewBag.tblCronogramaProy_Rev_ID = new SelectList(db.tblCronogramaProy_Rev, "tblCronogramaProy_Rev_ID", "croProy_revision", tblProyectosInvestigacion_Rev.tblCronogramaProy_Rev_ID);
            ViewBag.tblEstado_ID = new SelectList(db.tblEstado, "tblEstado_ID", "est_nombre", tblProyectosInvestigacion_Rev.tblEstado_ID);
            ViewBag.tblMarcoReferenciaProy_Rev_ID = new SelectList(db.tblMarcoReferenciaProy_Rev, "tblMarcoReferenciaProy_Rev_ID", "marRefProy_marcoTeoricoProy", tblProyectosInvestigacion_Rev.tblMarcoReferenciaProy_Rev_ID);
            ViewBag.tblMetodoProy_Rev_ID = new SelectList(db.tblMetodoProy_Rev, "tblMetodoProy_Rev_ID", "metProy_paradigmaMetodologicoProy", tblProyectosInvestigacion_Rev.tblMetodoProy_Rev_ID);
            ViewBag.tblPresentacionProyecto_Rev_ID = new SelectList(db.tblPresentacionProyecto_Rev, "tblPresentacionProyecto_Rev_ID", "preProy_tituloProy", tblProyectosInvestigacion_Rev.tblPresentacionProyecto_Rev_ID);
            ViewBag.tblPresupuestoProy_Rev_ID = new SelectList(db.tblPresupuestoProy_Rev, "tblPresupuestoProy_Rev_ID", "preProy_revision", tblProyectosInvestigacion_Rev.tblPresupuestoProy_Rev_ID);
            ViewBag.tblProblemaInvestigacionProy_Rev_ID = new SelectList(db.tblProblemaInvestigacionProy_Rev, "tblProblemaInvestigacionProy_Rev_ID", "proInvProy_planteamientoProblemaProy", tblProyectosInvestigacion_Rev.tblProblemaInvestigacionProy_Rev_ID);
            ViewBag.tblReferenciasProy_Rev_ID = new SelectList(db.tblReferenciasProy_Rev, "tblReferenciasProy_Rev_ID", "refProy_referencias", tblProyectosInvestigacion_Rev.tblReferenciasProy_Rev_ID);
            return View(tblProyectosInvestigacion_Rev);
        }

        // GET: ProyectosInvestigacion_Rev/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev = db.tblProyectosInvestigacion_Rev.Find(id);
            if (tblProyectosInvestigacion_Rev == null)
            {
                return HttpNotFound();
            }
            return View(tblProyectosInvestigacion_Rev);
        }

        // POST: ProyectosInvestigacion_Rev/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tblProyectosInvestigacion_Rev tblProyectosInvestigacion_Rev = db.tblProyectosInvestigacion_Rev.Find(id);
            db.tblProyectosInvestigacion_Rev.Remove(tblProyectosInvestigacion_Rev);
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
