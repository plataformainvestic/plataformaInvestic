using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;

namespace INI.Controllers.GruposInvestigacion
{
    //[Authorize(Roles = "Administrator,Estudiante,Maestro")]

    public class InvestigacionGruposController : Controller
    {

        public investicEntities db = new investicEntities();
        // GET: GruposInvestigacion
        public ActionResult Index()
        {
            List<MisGrupos> misGrupos = new List<MisGrupos>();
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            var listaGrupos = db.tblGrupoInvestigacion;

            foreach (var item in listaGrupos)
            {
                MisGrupos m = new MisGrupos();
                InformacionGrupo i = new InformacionGrupo();
                i.Descripcion = "No disponible";
                if (item.tblProblemaInvestigacion.FirstOrDefault() != null)
                {
                    i.Descripcion = item.tblProblemaInvestigacion.First().Descripcion;
                }
                i.Institucion = item.tblInstitucion.Nombre;
                i.Municipio = item.tblInstitucion.tblMunicipios.NombreMunicipio;
                i.NombreGrupo = item.Nombre;
                m.Informacion = i;
                m.Avatar = item.Avatar;
                m.id = item.id;
                misGrupos.Add(m);
            }        

            return View(misGrupos);
        }
    }
}