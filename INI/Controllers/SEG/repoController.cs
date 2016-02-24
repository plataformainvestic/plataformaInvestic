using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using INI.Models;
using INI.Extensions.ActionResults;
using INI.Models.DataBase;
using reporte = INI;




namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class repoController : Controller
    {
        // GET: repo
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Reporte()
        {
            return View();
        }

        public ActionResult pdf()
        {
            Reporte repodata = new Reporte();
            
            return new PDFResult(reporte.seg_PDFReports.Reporte(repodata), "reporte");
           
            
        }


        


        


    }
}