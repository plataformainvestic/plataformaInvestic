using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.Investigacion
{
    [Authorize]
    public class GruposInvestigacionController : Controller
    {
        private investicEntities db = new investicEntities();

        // GET: GruposInvestigacions
        public ActionResult MisGrupos()
        {
            //UserID
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            var misGrupos = (from t in db.tblGruposInvestigacion
                             where
                             t.tblUsuarioPlataforma_ID == idUsuario &&
                             t.tblEstado_ID == 1
                             select t).ToList();
            ViewBag.tblUsuarioPlataforma_ID = idUsuario;
            return View(misGrupos);
        }

        public ActionResult CrearGrupoInv() 
        {
            //UserID
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            AspNetUsers usuario = db.AspNetUsers.Find(idUsuario);
            ViewBag.tblUsuarioPlataforma_ID = idUsuario;
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearGrupoInv([Bind(Include = "tblGruposInvestigacion_ID,tblUsuarioPlataforma_ID,gruInv_nombreGrupo,gruInv_emblema,tblEstado_ID")] tblGruposInvestigacion tblGruposInvestigacion)
        {
            if (ModelState.IsValid)
            {
                tblGruposInvestigacion.tblEstado_ID = 1;
                tblGruposInvestigacion.gruInv_fechaCreacion = DateTime.Now;

                try
                {
                    //var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
                    var idUsuario = tblGruposInvestigacion.tblUsuarioPlataforma_ID;
                    string fileName = "imagen-no-disponible.jpg";
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0)
                            continue;
                        string folderPath = Server.MapPath("~/images/logo_Grupo/");
                        Directory.CreateDirectory(folderPath);
                        string ext = Path.GetExtension(hpf.FileName);
                        fileName = string.Format("img-{0}-{1}{2}", idUsuario, DateTime.Now.ToString("ddMMyyyyHHmmss"), ext);
                        string savedFileName = folderPath + fileName;
                        hpf.SaveAs(savedFileName);
                        //tblGruposInvestigacion.gruInv_emblema = fileName;
                    }
                    tblGruposInvestigacion.gruInv_emblema = fileName;
                    db.tblGruposInvestigacion.Add(tblGruposInvestigacion);
                    db.SaveChanges();
                    return RedirectToAction("MisGrupos", new { id = tblGruposInvestigacion.tblUsuarioPlataforma_ID });
                }
                catch (Exception)
                {
                    return RedirectToAction("MisGrupos", new { id = tblGruposInvestigacion.tblUsuarioPlataforma_ID });
                }

            }
            AspNetUsers usuario = db.AspNetUsers.Find(tblGruposInvestigacion.tblUsuarioPlataforma_ID);
            ViewBag.tblUsuarioPlataforma_ID = tblGruposInvestigacion.tblUsuarioPlataforma_ID;
            return View(usuario);
        }

        public ActionResult DetallesGrupo(long? id)//Id del grupo de investigacion
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGruposInvestigacion grupo = db.tblGruposInvestigacion.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            //List<tblUsuarioPlataforma> futurosIntegrantes = new List<tblUsuarioPlataforma>();
            //foreach (var item in db.tblUsuarioPlataforma)
            //{
            //    bool esIntegrante = false;
            //    foreach (var item2 in grupo.tblIntegrantesGrupoInv)
            //    {
            //        if (item2.tblUsuarioPlataforma_ID == item.tblUsuarioPlataforma_ID)
            //        {
            //            esIntegrante = true;
            //            break;
            //        }
            //    }
            //    if (!esIntegrante) futurosIntegrantes.Add(item);
            //}
            ViewBag.tblUsuarioPlataforma_ID = grupo.tblUsuarioPlataforma_ID;
            //futurosIntegrantes.Remove(db.tblUsuarioPlataforma.Find(grupo.tblUsuarioPlataforma_ID));
            //ViewBag.tblUsuarioPlataforma_ID = new SelectList(futurosIntegrantes, "tblUsuarioPlataforma_ID", "usuPla_identificacion");
            //ViewBag.tblUsuarioPlataforma = db.tblUsuarioPlataforma.ToList();
            return View(grupo);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DetallesGrupo([Bind(Include = "tblIntegrantesGrupoInv_ID,tblGruposInvestigacion_ID,tblUsuarioPlataforma_ID")] tblIntegrantesGrupoInv tblIntegrantesGrupoInv)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        tblIntegrantesGrupoInv.intGruInv_fechaVinculacion = DateTime.Now;
        //        db.tblIntegrantesGrupoInv.Add(tblIntegrantesGrupoInv);
        //        db.SaveChanges();
        //        return RedirectToAction("DetallesGrupo", new { id = tblIntegrantesGrupoInv.tblGruposInvestigacion_ID });
        //    }

        //    return View("DetallesGrupo", new { id = tblIntegrantesGrupoInv.tblGruposInvestigacion_ID });
        //}


        //public ActionResult EliminarIntegrante(long? id, long? grupo)//Id en tabla de integrante de Grupo
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EliminarIntegrante(long id, long? grupo)
        //{
        //    tblIntegrantesGrupoInv integrante = db.tblIntegrantesGrupoInv.Find(id);
        //    db.tblIntegrantesGrupoInv.Remove(integrante);
        //    db.SaveChanges();
        //    return RedirectToAction("DetallesGrupo", )
        //}









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
