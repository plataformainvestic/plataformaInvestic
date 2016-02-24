using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INI.Extensions.ActionResults;
using reporte = INI;

namespace INI.Controllers
{
    public class PDFBitacoraController : Controller
    {
        // GET: PDFBitacora
        public ActionResult Index()
        {
            return new PDFResult(reporte.BitacoraReport.Reporte(1027), "reporteBitacora");

          
        }
    }
}