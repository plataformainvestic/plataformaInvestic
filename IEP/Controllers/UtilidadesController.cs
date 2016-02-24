using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IEP.Models.DataBase;
using dl = ClassLibrary;

namespace IEP.Controllers
{
    public class UtilidadesController : Controller
    {
        private InvesticEntities db = new InvesticEntities();        

        // JsonResults
        public JsonResult LineaInvestigacion(int id)
        {
            var result = (from m in db.tblLineaInvestigacion
                          where m.Categoria == id
                          select new dl.ResearchLine { id = m.id, Name = m.Nombre }).ToList();
            return Json(new dl.ResearchLineList { totalResultsCount = result.Count(), ResearchLineItems = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteInstitucion(string term)
        {

            var result = (from m in db.tblInstitucion
                          where m.Nombre.ToLower().Contains(term.ToLower())
                          select new dl.Institution { id = m.id, Name = m.Nombre, Municipio = m.tblMunicipios.NombreMunicipio });
            return Json(new dl.InstitunionList { totalResultsCount = result.Count(), Institutions = result.ToList() }, JsonRequestBehavior.AllowGet);

        }
    }
}