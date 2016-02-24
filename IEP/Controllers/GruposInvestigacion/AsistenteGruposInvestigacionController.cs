using IEP.Models;
using IEP.Models.DataBase;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using dl = ClassLibrary;
using System.IO;
using System.Data.Entity;
using System.Threading.Tasks;

namespace IEP.Controllers.GruposInvestigacion
{
    [Authorize]
    public class AsistenteGruposInvestigacionController : Controller
    {
        /// <summary>
        /// Entidad de controla el mapeado de la base de datos
        /// </summary>
        private InvesticEntities db = new InvesticEntities();

        /// <summary>
        /// Constructor heredado 
        /// </summary>
        public AsistenteGruposInvestigacionController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        /// <summary>
        /// Constructor con parametro
        /// </summary>
        /// <param name="userManager">UserManager</param>
        public AsistenteGruposInvestigacionController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Propiedad pública UserManager para el registro de usuarios
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; private set; }


        /// <summary>
        /// Inicio del Asistente de Grupos de Investigacion
        /// </summary>
        /// <param name="code">Codigo de Mensaje de error</param>
        /// <returns></returns>
        public ActionResult Index(int code = 0, int id = 0)
        {
            string idUSer = AspNetUsers.GetUserId(User.Identity.Name);            
            tblGrupoInvestigacion tblgrupoinvestigacion = db.tblGrupoInvestigacion.Where(m => m.idUsuario.Equals(idUSer)).SingleOrDefault();
            if(id!=0)
            {
                tblgrupoinvestigacion = db.tblGrupoInvestigacion.Where(m => m.id == id).SingleOrDefault(); 
            }
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);

            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            return View(tblgrupoinvestigacion);
        }

        /// <summary>
        /// Crear Grupos de Investigación
        /// </summary>
        /// <returns></returns>
        public ActionResult Crear()
        {
            tblGrupoInvestigacion tblgrupoinvestigacion = new tblGrupoInvestigacion();
            ViewBag.idLineaInvestigacion = new SelectList(db.tblLineaInvestigacion, "id", "Nombre");
            tblgrupoinvestigacion.FechaCreacion = DateTime.Now;
            tblgrupoinvestigacion.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            tblgrupoinvestigacion.Codigo = dl.Codigos.ResearchGroupCode();
            return View("_CrearGrupo", tblgrupoinvestigacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "id,Codigo,Nombre,FechaCreacion,TipoGrupo,Avatar,idInstitucion,idLineaInvestigacion,idUsuario")] tblGrupoInvestigacion tblgrupoinvestigacion)
        {                       
            /// Datos del grupo de investigación adicionales
            /// que se cargan en tiempo de ejecución no en el modelo
            /// 
            tblgrupoinvestigacion.FechaCreacion = DateTime.Now;
            tblgrupoinvestigacion.Codigo = dl.Codigos.ResearchGroupCode();
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "imagen-no-disponible.jpg";
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0)
                            continue;
                        string folderPath = Server.MapPath("~/Images/Avatars/");
                        Directory.CreateDirectory(folderPath);
                        string ext = Path.GetExtension(hpf.FileName);
                        fileName = string.Format("img-{0}{1}", tblgrupoinvestigacion.Codigo, ext);
                        string savedFileName = Server.MapPath("~/Images/Avatars/" + fileName);
                        hpf.SaveAs(savedFileName);
                        tblgrupoinvestigacion.Avatar = fileName;
                    }
                    db.tblGrupoInvestigacion.Add(tblgrupoinvestigacion);
                    db.SaveChanges();

                    /// Creación de la reflexion
                    /// 
                    tblReflexionProyectoInvestigacion tblreflexion = new tblReflexionProyectoInvestigacion();
                    tblreflexion.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                    tblreflexion.FechaInicio = DateTime.Now;
                    tblreflexion.Proceso = "Proceso";
                    tblreflexion.Motivacion = "Motivación";
                    tblreflexion.Reflexion = "Reflexión";
                    tblreflexion.ConceptoAsesor = "Espacio para el Asesor";
                    db.tblReflexionProyectoInvestigacion.Add(tblreflexion);
                    db.SaveChanges();


                    /// Creación de la pregunta
                    /// 
                    tblPreguntaInvestigacion tblpregunta;

                    if (tblgrupoinvestigacion.TipoGrupo == 2)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            tblpregunta = new tblPreguntaInvestigacion();
                            tblpregunta.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                            tblpregunta.Consecutivo = i;
                            tblpregunta.FechaCreacion = DateTime.Now;
                            if (i == 0)
                            {
                                tblpregunta.Pregunta = "Pregunta seleccionada";
                                tblpregunta.PreguntaPrincipal = true;
                            }
                            else
                            {
                                tblpregunta.Pregunta = string.Format("Pregunta {0}", i.ToString());
                                tblpregunta.PreguntaPrincipal = false;
                            }
                            db.tblPreguntaInvestigacion.Add(tblpregunta);
                            db.SaveChanges();
                        }

                        tblPreguntaProyectoInvestigacion tblpreguntaproyecto = new tblPreguntaProyectoInvestigacion();
                        tblpreguntaproyecto.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                        db.tblPreguntaProyectoInvestigacion.Add(tblpreguntaproyecto);
                        db.SaveChanges();

                        tblProblemaInvestigacion tblproblemainvestigacion = new tblProblemaInvestigacion();

                        tblproblemainvestigacion.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                        db.tblProblemaInvestigacion.Add(tblproblemainvestigacion);
                        db.SaveChanges();

                        tblProblemaProyectoInvestigacion tblproblemaproyectoinvestigacion = new tblProblemaProyectoInvestigacion();

                        tblproblemaproyectoinvestigacion.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                        db.tblProblemaProyectoInvestigacion.Add(tblproblemaproyectoinvestigacion);
                        db.SaveChanges();
                    }
                    else
                    {
                        tblDocumentosSoporte tbldocumentos = new tblDocumentosSoporte();
                        tbldocumentos.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                        db.tblDocumentosSoporte.Add(tbldocumentos);
                        db.SaveChanges();
                    }
                    /// Grupo creado satisfactoriamente
                    return RedirectToAction("Index", new { code = 200 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", new { code = 201 });
                }
            }
            ViewBag.idLineaInvestigacion = new SelectList(db.tblLineaInvestigacion, "id", "Nombre");
            tblgrupoinvestigacion.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(tblgrupoinvestigacion);
        }        

        /// <summary>
        /// Maestros asesores o coinvestigadores
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MaestroCoinvestigador(int id = 0)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);            

            int idGrupoInvestigacion = tblGrupoInvestigacion.ResearchGroupIdByUser(userId);

            if (id == 0)
            {
                id = idGrupoInvestigacion;
            }
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            var tbcoresearchteacher = db.tblMiembroGrupo
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.tblRol.id == 1);
            if (tbcoresearchteacher.ToList().Count == 0)
            {
                var tbgroupmember = new tblMiembroGrupo();
                tbgroupmember.idGrupoInvestigacion = idGrupoInvestigacion;
                tbgroupmember.idUsuario = userId;
                tbgroupmember.idRol = 1;
                tbgroupmember.Grado = "0";
                try
                {
                    db.tblMiembroGrupo.Add(tbgroupmember);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", new { id = 160 });
                }
            }
            else
            {
                tbcoresearchteacher = db.tblMiembroGrupo
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.tblRol.id != 2);
            }
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { id = 999 });
            }
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(id);
            return View(tbcoresearchteacher.ToList());
        }

        /// <summary>
        /// Invitar usuario como Asesor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        public ActionResult InvitarAsesor(string id, int idGroup)
        {
            try
            {
                var tblinvitacion = new tblInvitacionGrupo();
                tblinvitacion.idGrupo = idGroup;
                tblinvitacion.idUsuario = id;
                tblinvitacion.idRol = 3;
                db.tblInvitacionGrupo.Add(tblinvitacion);
                db.SaveChanges();
                return RedirectToAction("MaestroCoinvestigador", new { id = 100 });

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = 150 });
            }
        }

        /// <summary>
        /// Invitar usuarios como Colaborador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        public ActionResult InvitarColaborador(string id, int idGroup)
        {
            try
            {
                var tblinvitacion = new tblInvitacionGrupo();
                tblinvitacion.idGrupo = idGroup;
                tblinvitacion.idUsuario = id;
                tblinvitacion.idRol = 4;
                db.tblInvitacionGrupo.Add(tblinvitacion);
                db.SaveChanges();
                return RedirectToAction("MaestroCoinvestigador", new { id = 100 });

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = 150 });
            }
        }

        /// <summary>
        /// Ingreso de información de la bitacora
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ParaElMaestro(int idGrupoInvestigacion)
        {
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(idGrupoInvestigacion);
            infogrupo.idGrupo = idGrupoInvestigacion;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            var tbresearchprojectreflexion = db.tblReflexionProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion).SingleOrDefault();
            return View(tbresearchprojectreflexion);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ParaElMaestro([Bind(Include = "id,FechaInicio,UltimaModificacion,Proceso,Motivacion,Reflexion,ConceptoAsesor,Revisado,idGrupoInvestigacion")] tblReflexionProyectoInvestigacion tbresearchprojectreflexion)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchprojectreflexion.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbresearchprojectreflexion.idGrupoInvestigacion });
            }
            tbresearchprojectreflexion.UltimaModificacion = DateTime.Now;
            if (ModelState.IsValid)
            {                
                db.Entry(tbresearchprojectreflexion).State = EntityState.Modified;
                db.SaveChanges();                
                return RedirectToAction("Index", new { code = 110 });
            }
            int id = tbresearchprojectreflexion.idGrupoInvestigacion;
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            return View(tbresearchprojectreflexion);
        }

        /// <summary>
        /// Revisión de la Bitacora por el Coinvestigador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ParaElCoinvestigador(int id)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { id = 999 });
            }
            var tbresearchprojectreflexion = db.tblReflexionProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == id).SingleOrDefault();
            return View(tbresearchprojectreflexion);
        }

        /// <summary>
        /// Pregunta para el grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Pregunta(int id)
        {
            ViewBag.ResearchId = id;
            InformacionGrupo infogrupo = new InformacionGrupo();            
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;
            var tbresearchquestion = db.tblPreguntaInvestigacion.Where(m => m.idGrupoInvestigacion == id);
            return View(tbresearchquestion);
        }

        /// <summary>
        /// Modificar pregunta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditarPreguntaInvestigacion(int id = 0)
        {            
            tblPreguntaInvestigacion model = db.tblPreguntaInvestigacion.Find(id);
            return PartialView("_EditarPreguntaInvestigacion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPreguntaInvestigacion([Bind(Include = "id,idGrupoInvestigacion,Consecutivo,Pregunta,FechaCreacion,FechaModificacion,PreguntaPrincipal")] tblPreguntaInvestigacion tbresearchquestion)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchquestion.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbresearchquestion.idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                tbresearchquestion.FechaModificacion = DateTime.Now;
                db.Entry(tbresearchquestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 120 });
            }
            return View(tbresearchquestion);
        }

        /// <summary>
        /// Pregunta para el maestro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PreguntaParaElMaestro(int idGrupoInvestigacion)
        {
            ViewBag.ResearchId = idGrupoInvestigacion;
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(idGrupoInvestigacion);
            infogrupo.idGrupo = idGrupoInvestigacion;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;
            var tbresearchprojectquestion = db.tblPreguntaProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion).SingleOrDefault();
            return View(tbresearchprojectquestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreguntaParaElMaestro([Bind(Include = "id,InformacionUno,FuenteUno,InformacionDos,FuenteDos,InformacionTres,FuenteTres,Reflexion,ConceptoAsesor,Revision,idGrupoInvestigacion")] tblPreguntaProyectoInvestigacion tbresearchprojectquestion)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);

            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchprojectquestion.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbresearchprojectquestion.idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbresearchprojectquestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 120 });
            }
            return View(tbresearchprojectquestion);
        }

        /// <summary>
        /// El problema
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ElProblema(int idGrupoInvestigacion)
        {
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(idGrupoInvestigacion);
            infogrupo.idGrupo = idGrupoInvestigacion;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;
            var tbresearchproblem = db.tblProblemaInvestigacion.Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion).SingleOrDefault();
            return View(tbresearchproblem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ElProblema([Bind(Include = "id,Descripcion,Justificacion,idGrupoInvestigacion")] tblProblemaInvestigacion tbresearchproblem)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);

            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchproblem.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbresearchproblem.idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbresearchproblem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 120 });
            }
            return View(tbresearchproblem);
        }

        /// <summary>
        /// Problema de investigación para el profesor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ElProblemaParaElProfesor(int idGrupoInvestigacion)
        {
            ViewBag.ResearchId = idGrupoInvestigacion;
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(idGrupoInvestigacion);
            infogrupo.idGrupo = idGrupoInvestigacion;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;
            var tbresearchprojectproblem = db.tblProblemaProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == idGrupoInvestigacion).SingleOrDefault();
            return View(tbresearchprojectproblem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ElProblemaParaElProfesor([Bind(Include = "id,Como,Reflexion,ConceptoAsesor,Revision,idGrupoInvestigacion")] tblProblemaProyectoInvestigacion tbresearchprojectproblem)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchprojectproblem.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbresearchprojectproblem.idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbresearchprojectproblem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 120 });
            }
            return View(tbresearchprojectproblem);
        }        

        public ActionResult AgregarPresupuesto(int idGrupoInvestigacion)
        {
            tblPresupuestoProyectoInvestigacion tblpresupuesto = new tblPresupuestoProyectoInvestigacion();
            tblpresupuesto.idGrupoInvestigacion = idGrupoInvestigacion;
            ViewBag.idRubro = new SelectList(db.tblRubro, "id", "Rubro");
            return View(tblpresupuesto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarPresupuesto([Bind(Include = "id,idGrupoinvestigacion,idRubro,DescripcionGasto,ValorRubro,ValorUnitario,Total")] tblPresupuestoProyectoInvestigacion tbpresupuesto)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbpresupuesto.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tbpresupuesto.idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                tbpresupuesto.Total = tbpresupuesto.ValorRubro * tbpresupuesto.ValorUnitario;
                db.tblPresupuestoProyectoInvestigacion.Add(tbpresupuesto);                
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 400, id = tbpresupuesto.idGrupoInvestigacion });
            }
            ViewBag.idRubro = new SelectList(db.tblRubro, "id", "Rubro");
            return View(tbpresupuesto);
        }

        public ActionResult PresupuestoDetallado(int id, int code = 0)
        {
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);
            var presupuesto = db.tblPresupuestoProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == id).ToList();
            if(presupuesto.Count == 0)
            {
                return RedirectToAction("Index", new { code = 411, id = id });
            }
            return View(presupuesto);
        }

        public ActionResult EliminarPresupuesto(int id)
        {            
            tblPresupuestoProyectoInvestigacion tblpresupuesto = db.tblPresupuestoProyectoInvestigacion.Find(id);
            int idGrupo = tblpresupuesto.idGrupoInvestigacion;
            db.tblPresupuestoProyectoInvestigacion.Remove(tblpresupuesto);
            db.SaveChanges();            
            return RedirectToAction("PresupuestoDetallado", new { id = idGrupo, code = 410 });
        }

        public ActionResult AgregarEstadoArte(int idGrupoInvestigacion)
        {
            tblEstadoArteProyectoInvestigacion tblestado = new tblEstadoArteProyectoInvestigacion();
            tblestado.idGrupoInvestigacion = idGrupoInvestigacion;
            return View(tblestado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarEstadoArte([Bind(Include="id,idGrupoInvestigacion,TemaInvestigacion,MapaConceptual")] tblEstadoArteProyectoInvestigacion tblestado)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tblestado.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tblestado.idGrupoInvestigacion });
            }
            try
            {
                foreach (string file in Request.Files)
                {                    
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    string folderPath = Server.MapPath("~/Upload/");
                    Directory.CreateDirectory(folderPath);
                    string ext = Path.GetExtension(hpf.FileName);
                    string fileName = dl.Codigos.CMap(db.tblGrupoInvestigacion.Find(tblestado.idGrupoInvestigacion).Nombre, ext); 
                        tblestado.MapaConceptual = fileName;                    
                    string savedFileName = Server.MapPath("~/Upload/" + fileName);
                    hpf.SaveAs(savedFileName);
                    db.tblEstadoArteProyectoInvestigacion.Add(tblestado);                                    
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 600, id = tblestado.idGrupoInvestigacion });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { code = 130, id = tblestado.idGrupoInvestigacion });
            }
            return View();               
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarConcepto([Bind(Include = "id,idEstadoArte,Autor,Publicacion,Texto")]tblConceptosEstadoArte tblconcepto)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idGrupoInvestigacion = db.tblEstadoArteProyectoInvestigacion.Find(tblconcepto.idEstadoArte).idGrupoInvestigacion;
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = idGrupoInvestigacion });
            }
            if (ModelState.IsValid)
            {
                db.tblConceptosEstadoArte.Add(tblconcepto);
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 400, id = idGrupoInvestigacion });
            }
            return View(tblconcepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarInformacion([Bind(Include="id,idGrupoInvestigacion,idInstrumento,Evidencia,Descripcion")] tblRecoleccionInformacionProyectoInvestigacion tblinformacion)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tblinformacion.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { code = 999, id = tblinformacion.idGrupoInvestigacion });
            }
            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    string folderPath = Server.MapPath("~/Upload/");
                    Directory.CreateDirectory(folderPath);
                    string ext = Path.GetExtension(hpf.FileName);
                    var grupo = db.tblGrupoInvestigacion.Find(tblinformacion.idGrupoInvestigacion);
                    string fileName = dl.Codigos.Evidence(grupo.Nombre, grupo.tblRecoleccionInformacionProyectoInvestigacion.Count(), ext);
                    tblinformacion.Evidencia = fileName;
                    string savedFileName = Server.MapPath("~/Upload/" + fileName);
                    hpf.SaveAs(savedFileName);
                    db.tblRecoleccionInformacionProyectoInvestigacion.Add(tblinformacion);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 601, id = tblinformacion.idGrupoInvestigacion });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { code = 601, id = tblinformacion.idGrupoInvestigacion });
            }
            return View();              
        }

        public ActionResult ForoProyecto(int id, int code = 0)
        {
            var tbforo = db.tblForoProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.idForo == null)
                .Include(m => m.tblForoProyectoInvestigacion1);
            ViewBag.idGrupoinvestigacion = id;
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);

            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            return View(tbforo.ToList());
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarForo([Bind(Include="id,idUser,idGrupoInvestigacion,Titulo,Mensaje,Fecha,Respuestas,idForo,FechaUltimaRespuesta")] tblForoProyectoInvestigacion tblforo)
        {
            tblforo.Fecha = DateTime.Now;            
            if (ModelState.IsValid)
            {                
                db.tblForoProyectoInvestigacion.Add(tblforo);
                db.SaveChanges();
                if (tblforo.idForo.HasValue)
                {
                    tblForoProyectoInvestigacion tblForoPrincipal = db.tblForoProyectoInvestigacion.Find(tblforo.idForo.Value);
                    int respuestas = db.tblForoProyectoInvestigacion.Where(m => m.idForo == tblforo.idForo.Value).Count();
                    tblForoPrincipal.Respuestas = respuestas;
                    db.Entry(tblForoPrincipal).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ForoProyecto", new { id = tblforo.idGrupoInvestigacion });
            }
            return RedirectToAction("ForoPoryecto", new { id = tblforo.idGrupoInvestigacion });
        }

        public ActionResult ComentarioProyecto(int id, int code = 0)
        {
            var tbcomentario = db.tblComentarioGrupo.Where(m => m.idGrupo == id)
                .Include(m => m.tblGrupoInvestigacion);
            ViewBag.idGrupoinvestigacion = id;
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);

            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;

            return View(tbcomentario.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarComentario([Bind(Include="id,idGrupo,UserId,Comentario,Aprobado")] tblComentarioGrupo tblcomentario)
        {
            if (ModelState.IsValid)
            {
                db.tblComentarioGrupo.Add(tblcomentario);
                db.SaveChanges();
                return RedirectToAction("ComentarioProyecto", new { id = tblcomentario.idGrupo });
            }
            return RedirectToAction("ComentarioProyecto", new { id = tblcomentario.idGrupo });
        }

        public ActionResult Comentarios(int id) 
        {
            var listaComentarios = db.tblComentarioGrupo.Where(m => m.idGrupo == id).Where(m => m.Aprobado.Value);
            return PartialView(listaComentarios.ToList());
        }

        public ActionResult AprobarComentario(int id)
        {
            tblComentarioGrupo tblcomentario = db.tblComentarioGrupo.Find(id);
            tblcomentario.Aprobado = true;
            db.Entry(tblcomentario).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ComentarioProyecto", new { id = tblcomentario.idGrupo });
        }

        public ActionResult EliminarComentario(int id)
        {
            tblComentarioGrupo tblcomentario = db.tblComentarioGrupo.Where(m => m.id == id).SingleOrDefault();
            db.tblComentarioGrupo.Remove(tblcomentario);
            db.SaveChanges();
            return RedirectToAction("ComentarioProyecto", new { id = tblcomentario.idGrupo });
        }

        /***************************/

        public ActionResult DocumentosSoporte(int id)
        {
            var tbsupportdocument = db.tblDocumentosSoporte.Where(m => m.idGrupoInvestigacion == id).SingleOrDefault();
            return View(tbsupportdocument);
        }

        public ActionResult UploadFileCommitment(int id)
        {
            var uploadfile = db.tblDocumentosSoporte.Find(id);
            return PartialView("_UploadFile", new UploadFile { Id = uploadfile.id, ResearchGroupId = uploadfile.idGrupoInvestigacion, FileToUpload = string.Empty, Acceptance = false });
        }

        public ActionResult UploadFileAceptance(int id)
        {
            var uploadfile = db.tblDocumentosSoporte.Find(id);
            return PartialView("_UploadFile", new UploadFile { Id = uploadfile.id, ResearchGroupId = uploadfile.idGrupoInvestigacion, FileToUpload = string.Empty, Acceptance = true });
        }

        [HttpPost]
        public ActionResult UploadFile(UploadFile model)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var tbsupportdocuments = db.tblDocumentosSoporte.Find(model.Id);

                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    string folderPath = Server.MapPath("~/Upload/");
                    Directory.CreateDirectory(folderPath);
                    string ext = Path.GetExtension(hpf.FileName);
                    string fileName = "";

                    if (model.Acceptance)
                    {
                        fileName = dl.Codigos.Acceptance(db.tblGrupoInvestigacion.Find(model.ResearchGroupId).Nombre, ext);
                        tbsupportdocuments.CartaAceptacion = fileName;
                    }
                    else
                    {
                        fileName = dl.Codigos.Commitment(db.tblGrupoInvestigacion.Find(model.ResearchGroupId).Nombre, ext);
                        tbsupportdocuments.CartaCompromiso = fileName;
                    }
                    string savedFileName = Server.MapPath("~/Upload/" + fileName);
                    hpf.SaveAs(savedFileName);
                    db.Entry(tbsupportdocuments).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { codeError = 130 });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CargarImagenFeria(tblPropagacionGrupo model)
        {
            int id = model.idGrupoInvestigacion;
            try
            {
                foreach (string file in Request.Files)
                {
                    var grupo = db.tblGrupoInvestigacion.Find(model.idGrupoInvestigacion);
                    int index = db.tblPropagacionGrupo.Where(m => m.idGrupoInvestigacion == id).Count();
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    string folderPath = string.Format("{0}{1}/", Server.MapPath("~/Upload/"), grupo.Nombre.Replace(" ", ""));
                    Directory.CreateDirectory(folderPath);
                    string ext = Path.GetExtension(hpf.FileName);
                    string fileName = "";
                    fileName = dl.Codigos.ImgInstitucional(grupo.Nombre.Replace(" ", ""), ext, index);
                    string savedFileName = Server.MapPath("~/Upload/" + grupo.Nombre.Replace(" ", "") + "/" + fileName);                    
                    hpf.SaveAs(savedFileName);
                    model.Archivo = string.Format("../../Upload/{0}/{1}", grupo.Nombre.Replace(" ", ""), fileName);
                    db.tblPropagacionGrupo.Add(model);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = id, code = 135 });
            }
            return RedirectToAction("Index", new { id = id, code = 130 });
        }

        /// <summary>
        /// Miembros del grupo
        /// </summary>
        /// <returns></returns>
        public ActionResult MiembroGrupo()
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);

            int idRol = tblMiembroGrupo.GetRoleMiembro(userId);
            if (idRol != 1)
            {
                return RedirectToAction("Index", new { id = 999 });
            }
            InformacionGrupo infogrupo = new InformacionGrupo();
            int idGrupo = tblGrupoInvestigacion.ResearchGroupIdByUser(userId);
            var grupo = tblGrupoInvestigacion.Find(idGrupo);
            infogrupo.idGrupo = idGrupo;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == idGrupo)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            ViewBag.InfoGrupo = infogrupo;
            int id = tblGrupoInvestigacion.ResearchGroupIdByUser(userId);
            var tbgroupmember = db.tblMiembroGrupo.Where(m => m.idGrupoInvestigacion == id).Where(m => m.idRol == 2);
            return View(tbgroupmember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarMiembroGrupo(MiembroGrupo model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.User.PersonalID,
                    Name = model.User.Name,
                    SureName = model.User.SureName,
                    PersonalID = model.User.PersonalID,
                    Genre = model.User.Genre,
                    Email = model.User.Mail,
                    PhoneNumber = model.User.Phone,
                    Address = model.User.Address,
                    BirthDay = model.User.BirthDay
                };
                var result = await UserManager.CreateAsync(user, model.User.Password);
                if (result.Succeeded)
                {
                    model.Informacion.idUsuario = user.Id;
                    db.tblMiembroGrupo.Add(model.Informacion);
                    db.SaveChanges();
                    RedirectToAction("MiembroGrupo");
                }
                else
                {
                    AddErrors(result);
                }
            }
            return RedirectToAction("MiembroGrupo");
        }

        /// <summary>
        /// Eliminar Miembro grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteGroupMember(int id)
        {
            tblMiembroGrupo tbgroupmember = db.tblMiembroGrupo.Find(id);
            int idGroup = tbgroupmember.idGrupoInvestigacion;
            db.tblMiembroGrupo.Remove(tbgroupmember);
            db.SaveChanges();
            return RedirectToAction("MiembroGrupo");
        }

        /// <summary>
        /// Ver Invitaciones
        /// </summary>
        /// <returns></returns>
        public ActionResult Invitaciones()
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            var invitaciones = db.tblInvitacionGrupo.Where(m => m.idUsuario.Equals(userId))
                .Include(m => m.tblGrupoInvestigacion);
            return View(invitaciones.ToList());
        }

        /// <summary>
        /// Aceptar invitación
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Aceptar(int id)
        {
            var invitacion = db.tblInvitacionGrupo.Find(id);
            tblMiembroGrupo miembro = new tblMiembroGrupo();
            miembro.Grado = "0";
            miembro.idGrupoInvestigacion = invitacion.idGrupo;
            miembro.idUsuario = invitacion.idUsuario;
            miembro.idRol = invitacion.idRol;
            db.tblMiembroGrupo.Add(miembro);
            db.SaveChanges();
            invitacion.Aceptada = true;
            db.Entry(invitacion).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Invitaciones");
        }

        //Adicionales

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }   
}