using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INI.Models.Adds;
using INI.Models.DataBase;

namespace INI.Controllers.Aprendizaje
{
    public class JsonController : Controller
    {
        // GET: Json
        public JsonResult AutoCompleteCategoria(string term)
        {
            chamiloEntities db = new chamiloEntities();

            var result = (from c in db.course
                          where c.title.ToLower().Contains(term.ToLower())
                          select new CategoriaCursos { id = c.id, name = c.title, code = c.code });
            return Json(new ListaCategorias { totalResultsCount = result.Count(), Categorias = result.ToList() }, JsonRequestBehavior.AllowGet);

        }
        // GET: Json
        public JsonResult buscacurso(string codcat)
        {
            chamiloEntities db = new chamiloEntities();

            var result = (from c in db.course
                          where c.category_code.Equals(codcat)
                          select new CursosChamilo { title = c.title });
            return Json(new ListaCursos { totalResultsCount = result.Count(), Cursos = result.ToList() }, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult TipoEstudioList(string tblParadigmaEpistemologico_ID)
        //{
        //    if (tblParadigmaEpistemologico_ID.Equals("1"))
        //    {
        //        var tblTipoEstudioProy_ID = new SelectList(db.tblTipoEstudioProy, "tblTipoEstudioProy_ID", "tipEst_nombre");
        //        return Json(new SelectList(tblTipoEstudioProy_ID.ToArray(), "tblTipoEstudioProy_ID", "tipEst_nombre", JsonRequestBehavior.AllowGet));
        //    }
        //    return null;
        //}

     
    }
}