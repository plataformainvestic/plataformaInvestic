using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SEG.Models;
using SEG.Extensions.ActionResults;
using SEG.Models.DataBase;
using reporte = SEG;




namespace SEG.Controllers
{
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
            
            return new PDFResult(reporte.PDFReports.Reporte(repodata), "reporte");

            
        }


        


        


    }
}