using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using SEG.Extensions.ActionResults;
using SEG.Models;
using SEG.Models.DataBase;
using reporte = SEG;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SEG.Controllers
{
    public class ReporteController : Controller
    {

        private Entities db = new Entities();
        // GET: Reporte
        [HttpGet]
        public ActionResult Reporte()
        {
                       
            Reporte repodata = new Reporte();
            repodata.FechaFinal = DateTime.Now;
            repodata.Fechainicial = DateTime.Now;
            repodata.Tipo = 1;
            repodata.UserId = User.Identity.GetUserId();
            return View(repodata);
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

                if(repodata.Fechainicial>=fechainicontrato && repodata.FechaFinal<= fechafinalcontrato)
                {
                    return new PDFResult(reporte.PDFReports.Reporte(repodata), "reporte");

                }
                else
                {
                    ViewBag.Message = "No se puede generar reporte, verifique fechas";
                    return View(repodata);

                }
             


                
            }
            else
            {
                return View(repodata);
            }
        }


    }
}