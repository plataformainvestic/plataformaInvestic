using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using INI.Extensions.ActionResults;
using INI.Models;
using INI.Models.DataBase;
using reporte = INI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class ReporteController : Controller
    {
        private investicEntities db = new investicEntities();
        // GET: Reporte
        [HttpGet]
        [Authorize]
        public ActionResult Reporte()
        {
               if (AspNetUsersRoles.IsUserInRole("Contratista", User.Identity.Name) )
               {
                    Reporte repodata = new Reporte();
                    repodata.FechaFinal = DateTime.Now;
                    repodata.Fechainicial = DateTime.Now;
                    repodata.Tipo = 1;
                    repodata.UserId = User.Identity.GetUserId();
                    return View(repodata);
               }
               else{
                    return RedirectToAction("IniciarSesion", "Usuario");
               }
        }

        //POST: Reporte
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reporte(Reporte repodata)
        {
           
            if (ModelState.IsValid)
            {
                var fechafinalcontrato = (from f in db.AspNetUsers where f.Id.Equals(repodata.UserId) select f.Fecha_FinContrato).SingleOrDefault();
                var fechainicontrato = (from f in db.AspNetUsers where f.Id.Equals(repodata.UserId) select f.Fecha_IniContrato).SingleOrDefault();

                if(repodata.FechaFinal> repodata.Fechainicial)
                {
                    if (repodata.Fechainicial >= fechainicontrato && repodata.FechaFinal <= fechafinalcontrato)
                    {
                        return new PDFResult(reporte.seg_PDFReports.Reporte(repodata), "reporte");
                    }
                    else
                    {
                        ViewBag.Message = "No se puede generar reporte, verifique fechas";
                        return View(repodata);
                    }
                }
                else
                { 
                    ViewBag.Message = "No se puede generar reporte, verifique fechas";
                    return View(repodata);
                }
            }
            return View(repodata);
            }
          
        }


    }
