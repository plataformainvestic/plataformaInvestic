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
    public class InvestigadorController : Controller
    {

        private InvesticEntities db = new InvesticEntities();
        // GET: Investigador
        public ActionResult Index(int code = 0)
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);
            tblMaestroCoInvestigador maestro = db.tblMaestroCoInvestigador.Where(m => m.idUsuario.Equals(idUsuario))
                .Include(m => m.tblAreaConocimiento)
                .FirstOrDefault();
            if (maestro != null)
            {
                return View(maestro);
            }
            else
            {
                return RedirectToAction("CrearMaestro");  
            }
        }

        public ActionResult CrearMaestro()
        {
            tblMaestroCoInvestigador maestro = new tblMaestroCoInvestigador();
            ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre");
            maestro.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(maestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearMaestro([Bind(Include = "id,idInstitucion,idUsuario,TiempoOndas,Pregrado,Postgrado,Otro,idAreaConocimiento,ExperienciaAreaConocimiento")] tblMaestroCoInvestigador tblmaestrocoinvvestigador)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    db.tblMaestroCoInvestigador.Add(tblmaestrocoinvvestigador);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 100 });
                }
                catch (Exception)
                {
                    ;
                }
            }
            ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre");
            return View("CrearMaestro", tblmaestrocoinvvestigador);
        }

        public ActionResult ModificarMaestro()
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            tblMaestroCoInvestigador maestro = db.tblMaestroCoInvestigador.Where(m => m.idUsuario.Equals(idUsuario))
                .Include(m => m.tblAreaConocimiento)
                .FirstOrDefault();
            ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre", maestro.idAreaConocimiento);
            maestro.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(maestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarMaestro([Bind(Include = "id,idUsuario,TiempoOndas,Pregrado,Postgrado,Otro,idAreaConocimiento,ExperienciaAreaConocimiento")] tblMaestroCoInvestigador tblmaestrocoinvvestigador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tblmaestrocoinvvestigador).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 200 });
                }
                catch (Exception)
                {
                    ;
                }
            }
            ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre");
            return View("ModificarMaestro", tblmaestrocoinvvestigador);
        }

        public ActionResult Pruebas()
        {
            return View(db.tblPropagacionGrupo.Where(m => m.idGrupoInvestigacion == 22).ToList());
        }

    }
}