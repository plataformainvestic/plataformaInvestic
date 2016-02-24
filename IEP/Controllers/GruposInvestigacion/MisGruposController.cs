using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IEP.Models.DataBase;
using dl = ClassLibrary;

namespace IEP.Controllers.GruposInvestigacion
{
    [Authorize]
    public class MisGruposController : Controller
    {

        InvesticEntities db = new InvesticEntities();
        // GET: MisGrupos
        public ActionResult Index()
        {            
            List<MisGrupos> misGrupos = new List<MisGrupos>();
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            var listaGrupos = db.tblGrupoInvestigacion.Where(g => g.idUsuario.Equals(idUsuario));
                        
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

            var invitaciones = db.tblInvitacionGrupo.Where(m => m.idUsuario == idUsuario)
                .Where(m => m.Aceptada)
                .Include(m => m.tblGrupoInvestigacion);

            foreach(var item in invitaciones)
            {
                MisGrupos m = new MisGrupos();
                InformacionGrupo i = new InformacionGrupo();
                i.Descripcion = item.tblGrupoInvestigacion.tblProblemaInvestigacion.First().Descripcion;
                i.Institucion = item.tblGrupoInvestigacion.tblInstitucion.Nombre;
                i.Municipio = item.tblGrupoInvestigacion.tblInstitucion.tblMunicipios.NombreMunicipio;
                i.NombreGrupo = item.tblGrupoInvestigacion.Nombre;
                m.Informacion = i;
                m.Avatar = item.tblGrupoInvestigacion.Avatar;
                m.id = item.tblGrupoInvestigacion.id;
                misGrupos.Add(m);
            }

            return View(misGrupos);
        }        

        public ActionResult CrearGrupo()
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);            
            tblMaestroCoInvestigador maestro = db.tblMaestroCoInvestigador.Where(m => m.idUsuario.Equals(idUsuario))                
                .FirstOrDefault();
            if (maestro != null)
            {
                var tblgrupoinvestigacion = db.tblGrupoInvestigacion.Where(m => m.idUsuario.Equals(idUsuario)).FirstOrDefault();
                if (tblgrupoinvestigacion != null)
                {
                    return View("VerGrupo", tblgrupoinvestigacion);
                }
                else
                {
                    return RedirectToAction("Index", "AsistenteGruposInvestigacion");
                }
            }
            else
            {
                return RedirectToAction("CrearMaestro", "Investigador");
            }
        }

        public ActionResult VerGrupo(int id)
        {
            tblGrupoInvestigacion tblgrupoinvestigacion = db.tblGrupoInvestigacion.Find(id);
            ViewBag.idLineaInvestigacion = new SelectList(db.tblLineaInvestigacion, "id", "Nombre");
            tblgrupoinvestigacion.id = 1;
            tblgrupoinvestigacion.Codigo = dl.Codigos.ResearchGroupCode();
            tblgrupoinvestigacion.Nombre = "Grupo los intocables";
            tblgrupoinvestigacion.FechaCreacion = DateTime.Now;
            tblgrupoinvestigacion.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            
            return View(tblgrupoinvestigacion);
        }
        
    }
}