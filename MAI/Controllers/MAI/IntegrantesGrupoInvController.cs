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
    public class IntegrantesGrupoInvController : Controller
    {
        private investicEntities db = new investicEntities();

        public ActionResult Index(long? id)//recibe el id del grupo de investigacion al que se van a vincular los integrantes
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

        public ActionResult Integrantes(long? id) //recibe el id del grupo de investigacion 
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

            //var nuevosUsuarios = from t in db.tblUsuarioPlataforma
            //                     where !(from u in db.tblIntegrantesGrupoInv
            //                             where u.tblGruposInvestigacion_ID == id
            //                             select u.tblUsuarioPlataforma_ID).Contains(t.tblUsuarioPlataforma_ID)
            //                     select t;

            //ViewBag.tblGrupoInvestigacion_ID = id;
            //ViewBag.tblUsuarioPlataforma_ID = new SelectList(nuevosUsuarios, "tblUsuarioPlataforma_ID", "usuPla_identificacion", "usuPla_nombre");
            return View(miGrupo);
        }

        public ActionResult AgregarIntegranteGrupoInv(string idUsuario, long idGrupo)
        {
            tblIntegrantesGrupoInv nuevoIntegrante = new tblIntegrantesGrupoInv();
            nuevoIntegrante.intGruInv_fechaVinculacion = DateTime.Now;
            nuevoIntegrante.tblGruposInvestigacion_ID = idGrupo;
            nuevoIntegrante.tblUsuarioPlataforma_ID = idUsuario;
            db.tblIntegrantesGrupoInv.Add(nuevoIntegrante);
            db.SaveChanges();
            return RedirectToAction("Integrantes", new { id = idGrupo });
        }


        public ActionResult AgregarIntegrantes(long? id) //recibe el id del grupo de investigacion al que se van a vincular
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
            IQueryable<AspNetUsers> usuarios = from t in db.AspNetUsers
                                                        where !(from u in db.tblIntegrantesGrupoInv
                                                                where u.tblGruposInvestigacion_ID == miGrupo.tblGruposInvestigacion_ID
                                                                select u.tblUsuarioPlataforma_ID).Contains(t.Id)
                                                        select t;


            //IQueryable<tblUsuarioPlataforma> usuarios = from t in db.tblUsuarioPlataforma
            //                                            where !(from u in db.tblIntegrantesGrupoInv
            //                                                    where u.tblGruposInvestigacion_ID == miGrupo.tblGruposInvestigacion_ID
            //                                                    select u.tblUsuarioPlataforma_ID).Contains(t.tblUsuarioPlataforma_ID)
            //                                            select t;
            ViewBag.tblGruposInvestigacion_ID = id;
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarIntegrantes([Bind(Include = "tblIntegrantesGrupoInv_ID,tblGruposInvestigacion_ID,tblUsuarioPlataforma_ID")] tblIntegrantesGrupoInv tblIntegrantesGrupoInv)
        {
            if (ModelState.IsValid)
            {
                tblIntegrantesGrupoInv.intGruInv_fechaVinculacion = DateTime.Now;
                db.tblIntegrantesGrupoInv.Add(tblIntegrantesGrupoInv);
                db.SaveChanges();
                tblGruposInvestigacion g = db.tblGruposInvestigacion.Find(tblIntegrantesGrupoInv.tblGruposInvestigacion_ID);
                return RedirectToAction("Integrantes", new { id = g.tblGruposInvestigacion_ID });
                //return RedirectToAction("Integrantes", new { id = g.tblGruposInvestigacion_ID });
            }

            ViewBag.tblGruposInvestigacion_ID = new SelectList(db.tblGruposInvestigacion, "tblGruposInvestigacion_ID", "gruInv_nombreGrupo", tblIntegrantesGrupoInv.tblGruposInvestigacion_ID);
            return View(tblIntegrantesGrupoInv);
        }


        // GET: tblIntegrantesGrupoInvs/Delete/5
        public ActionResult Delete(long? id)
        {
            tblIntegrantesGrupoInv tblIntegrantesGrupoInv = db.tblIntegrantesGrupoInv.Find(id);
            var idGrupo = tblIntegrantesGrupoInv.tblGruposInvestigacion_ID;
            db.tblIntegrantesGrupoInv.Remove(tblIntegrantesGrupoInv);
            db.SaveChanges();
            return RedirectToAction("Integrantes", "IntegrantesGrupoInv", new { id = idGrupo });
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
