using INI.Models;
using INI.Models.DataBase;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using dl = ClassLibrary;

namespace INI.Controllers.IEP
{
    [Authorize]
    public class IEPController : Controller
    {
        investicEntities db = new investicEntities();

         /// <summary>
        /// Constructor heredado 
        /// </summary>
        public IEPController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        /// <summary>
        /// Constructor con parametro
        /// </summary>
        /// <param name="userManager">UserManager</param>
        public IEPController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Propiedad pública UserManager para el registro de usuarios
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: IEP
        public ActionResult Index(int code = 0)
        {
            if (AspNetUsersRoles.IsUserInRole("Estudiante", User.Identity.Name))
            {
                return RedirectToAction("Estudiante");
            }            
            
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            if(!tblMaestroCoInvestigador.Exist(idUsuario))
            {
                return RedirectToAction("CrearMaestro");
            }

            List<MisGrupos> misGrupos = new List<MisGrupos>();

            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);
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
                i.Pregunta = "Pregunta no disponible";
                if (item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).FirstOrDefault() != null)
                {
                    i.Pregunta = item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }                
                m.Informacion = i;
                m.Avatar = item.Avatar;
                m.id = item.id;
                misGrupos.Add(m);
            }

            var invitaciones = db.tblInvitacionGrupo.Where(m => m.idUsuario == idUsuario)
                .Where(m => m.Aceptada)
                .Include(m => m.tblGrupoInvestigacion);

            foreach (var item in invitaciones)
            {
                MisGrupos m = new MisGrupos();
                InformacionGrupo i = new InformacionGrupo();                
                i.Descripcion = "No disponible";
                if (item.tblGrupoInvestigacion.tblProblemaInvestigacion.First() != null)
                {
                    i.Descripcion = item.tblGrupoInvestigacion.tblProblemaInvestigacion.First().Descripcion;
                }
                i.Institucion = item.tblGrupoInvestigacion.tblInstitucion.Nombre;
                i.Municipio = item.tblGrupoInvestigacion.tblInstitucion.tblMunicipios.NombreMunicipio;
                i.NombreGrupo = item.tblGrupoInvestigacion.Nombre;
                i.Pregunta = "Pregunta no disponible";
                if (item.tblGrupoInvestigacion.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    i.Pregunta = item.tblGrupoInvestigacion.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }                
                m.Informacion = i;
                m.Avatar = item.tblGrupoInvestigacion.Avatar;
                m.id = item.tblGrupoInvestigacion.id;
                misGrupos.Add(m);
            }

            return View(misGrupos);
        }

        public ActionResult Estudiante(int code = 0)
        {
            List<MisGrupos> misGrupos = new List<MisGrupos>();
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);
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
                i.Pregunta = "Pregunta no disponible";
                if (item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    i.Pregunta = item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                m.Informacion = i;
                m.Avatar = item.Avatar;
                m.id = item.id;
                misGrupos.Add(m);
            }

            var invitaciones = db.tblInvitacionGrupo.Where(m => m.idUsuario == idUsuario)
                .Where(m => m.Aceptada)
                .Include(m => m.tblGrupoInvestigacion);

            foreach (var item in invitaciones)
            {
                MisGrupos m = new MisGrupos();
                InformacionGrupo i = new InformacionGrupo();
                i.Descripcion = "No disponible";
                if (item.tblGrupoInvestigacion.tblProblemaInvestigacion.First() != null)
                {
                    i.Descripcion = item.tblGrupoInvestigacion.tblProblemaInvestigacion.First().Descripcion;
                }
                i.Institucion = item.tblGrupoInvestigacion.tblInstitucion.Nombre;
                i.Municipio = item.tblGrupoInvestigacion.tblInstitucion.tblMunicipios.NombreMunicipio;
                i.NombreGrupo = item.tblGrupoInvestigacion.Nombre;
                i.Pregunta = "Pregunta no disponible";
                if (item.tblGrupoInvestigacion.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    i.Pregunta = item.tblGrupoInvestigacion.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                m.Informacion = i;
                m.Avatar = item.tblGrupoInvestigacion.Avatar;
                m.id = item.tblGrupoInvestigacion.id;
                misGrupos.Add(m);
            }

            return View(misGrupos);
        }

        #region Hoja de Vida Maestro Coinvestigador
        public ActionResult HojaDeVida(int code = 0)
        {
            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);
            tblMaestroCoInvestigador maestro = new tblMaestroCoInvestigador();
            ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre");
            maestro.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(maestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HojaDeVida([Bind(Include = "id,idInstitucion,idUsuario,TiempoOndas,Pregrado,Postgrado,Otro,idAreaConocimiento,ExperienciaAreaConocimiento")] tblMaestroCoInvestigador tblmaestrocoinvvestigador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tblMaestroCoInvestigador.Add(tblmaestrocoinvvestigador);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 10 });
                }
                ViewBag.idAreaConocimiento = new SelectList(db.tblAreaConocimiento, "id", "Nombre");
                return View("HojaDeVida", tblmaestrocoinvvestigador);
            }
            catch (Exception)
            {
                return RedirectToAction("HojaDeVida", new { code = 20 });
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
        #endregion

        #region Registrar y Modificar Grupo de Investigacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarGrupo([Bind(Include = "id,Codigo,Nombre,FechaCreacion,TipoGrupo,Avatar,idInstitucion,idLineaInvestigacion,idUsuario")] tblGrupoInvestigacion tblgrupoinvestigacion)
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
                    //--------------------------------
                    //tblPreguntaInvestigacion pregunta = new tblPreguntaInvestigacion();
                    //pregunta.FechaCreacion = System.DateTime.Now;
                    //pregunta.FechaModificacion = System.DateTime.Now;
                    //pregunta.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                    //pregunta.Pregunta = "";
                    //pregunta.PreguntaPrincipal = true;
                    //pregunta.Consecutivo = 0;
                    //db.tblPreguntaInvestigacion.Add(pregunta);
                    //--------------------
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

                    if (tblgrupoinvestigacion.TipoGrupo == 2)// abierto
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            tblpregunta = new tblPreguntaInvestigacion();
                            tblpregunta.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                            tblpregunta.Consecutivo = i;
                            tblpregunta.FechaCreacion = DateTime.Now;
                            if (i == 0)
                            {
                                tblpregunta.Pregunta = "Escriba la Pregunta seleccionada";
                                tblpregunta.PreguntaPrincipal = true;
                            }
                            else
                            {
                                tblpregunta.Pregunta = string.Format("Escriba la Pregunta {0}", i.ToString());
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
                    else // pre-estructurado
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            tblpregunta = new tblPreguntaInvestigacion();
                            tblpregunta.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                            tblpregunta.Consecutivo = i;
                            tblpregunta.FechaCreacion = DateTime.Now;
                            if (i == 0)
                            {
                                tblpregunta.Pregunta = "Escriba la Pregunta seleccionada";
                                tblpregunta.PreguntaPrincipal = true;
                            }
                            else
                            {
                                tblpregunta.Pregunta = string.Format("Escriba la Pregunta {0}", i.ToString());
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

                        tblDocumentosSoporte tbldocumentos = new tblDocumentosSoporte();
                        tbldocumentos.idGrupoInvestigacion = tblgrupoinvestigacion.id;
                        db.tblDocumentosSoporte.Add(tbldocumentos);
                        db.SaveChanges();
                    }
                    /// Grupo creado satisfactoriamente
                    return RedirectToAction("Index", new { code = 10 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", new { code = 20 });
                }
            }
            ViewBag.idLineaInvestigacion = new SelectList(db.tblLineaInvestigacion, "id", "Nombre");
            tblgrupoinvestigacion.idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            return View(tblgrupoinvestigacion);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarGrupo([Bind(Include = "id,Codigo,Nombre,FechaCreacion,TipoGrupo,Avatar,idInstitucion,idLineaInvestigacion,idUsuario")] tblGrupoInvestigacion tblgrupoinvestigacion)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = tblGrupoInvestigacion.Find(tblgrupoinvestigacion.id).Avatar;
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
                    }
                    tblgrupoinvestigacion.Avatar = fileName;
                    db.Entry(tblgrupoinvestigacion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 15 });
                }
                return RedirectToAction("Index", new { code = 20 });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { code = 20 });
            }
        }
        #endregion

        #region Invitaciones Grupos de Investigacion
        public ActionResult Invitaciones()
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            var invitaciones = db.tblInvitacionGrupo.Where(m => m.idUsuario.Equals(userId))
                .Include(m => m.tblGrupoInvestigacion);
            return View(invitaciones.ToList());
        }

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
        #endregion        

        public ActionResult IrAlProyecto(int id)
        {
            tblGrupoInvestigacion tblgrupo = db.tblGrupoInvestigacion.Find(id);
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            InformacionGrupo infogrupo = new InformacionGrupo();            
            infogrupo.idGrupo = id;

            if (tblgrupo.tblPreguntaInvestigacion.Count>0)
            {
                infogrupo.Descripcion = tblgrupo.tblPreguntaInvestigacion
                    .Where(m => m.idGrupoInvestigacion == id)
                    .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
                infogrupo.Institucion = tblgrupo.tblInstitucion.Nombre;
                infogrupo.Municipio = tblgrupo.tblInstitucion.tblMunicipios.NombreMunicipio;
                infogrupo.NombreGrupo = tblgrupo.Nombre;
            }

            if (tblgrupo.idUsuario.Equals(idUsuario))
            {
                infogrupo.EsCreador = true;
                ViewBag.InfoGrupo = infogrupo;
                return View("MiGrupo", tblgrupo);
            }
            else
            {                
                infogrupo.EsCreador = false;
                ViewBag.InfoGrupo = infogrupo;                                
                return View("Grupo", tblgrupo);
            }            
        }


        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult OffLineMetodo(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id.ToString()))
                {

                    investicEntities db = new investicEntities();
                    var cadenaxml = db.GeneradorConsultaSQLAXML(id.ToString()).ToList();
                    var arvhivo = cadenaxml[0].ToString();
                    byte[] fileBytes = Encoding.ASCII.GetBytes(arvhivo);
                    string fileName = "ArchivoXML.xml";
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                return Json(new { d_rta = "Archivo generado correctamente." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { d_rta = "Error! al generar el archivo xml:" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult IrABitacoras(int id, int code = 0)
        {
            tblGrupoInvestigacion tblgrupo = db.tblGrupoInvestigacion.Find(id);
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);

            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);

            InformacionGrupo infogrupo = new InformacionGrupo();
            infogrupo.idGrupo = id;
            if (tblgrupo.tblPreguntaInvestigacion.Count > 0)
            {
                infogrupo.Descripcion = tblgrupo.tblPreguntaInvestigacion
                    .Where(m => m.idGrupoInvestigacion == id)
                    .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
                infogrupo.Institucion = tblgrupo.tblInstitucion.Nombre;
                infogrupo.Municipio = tblgrupo.tblInstitucion.tblMunicipios.NombreMunicipio;
                infogrupo.NombreGrupo = tblgrupo.Nombre;
            }

            if (tblgrupo.idUsuario.Equals(idUsuario))
            {
                infogrupo.EsCreador = true;
                ViewBag.InfoGrupo = infogrupo;
                return View("BitacoraCreador", tblgrupo);
            }
            else
            {
                infogrupo.EsCreador = false;
                ViewBag.InfoGrupo = infogrupo;                
                return View("BitacoraColaborador", tblgrupo);
            }            
        }

        #region Foros del proyecto
        public ActionResult ForoProyecto(int id, int code = 0)
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
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
            infogrupo.EsCreador = false;
            if (tblGrupoInvestigacion.Find(id).idUsuario.Equals(idUsuario))
            {
                infogrupo.EsCreador = true;
            }
            ViewBag.InfoGrupo = infogrupo;

            return View(tbforo.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarForo([Bind(Include = "id,idUser,idGrupoInvestigacion,Titulo,Mensaje,Fecha,Respuestas,idForo,FechaUltimaRespuesta")] tblForoProyectoInvestigacion tblforo)
        {
            tblforo.Fecha = DateTime.Now;
            
            int id = tblforo.idGrupoInvestigacion;
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
        #endregion

        #region Comentarios del Proyecto
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
        public ActionResult AgregarComentario([Bind(Include = "id,idGrupo,UserId,Comentario,Aprobado")] tblComentarioGrupo tblcomentario)
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
        #endregion

        #region Bitacora 1 Estar en la onda
        /// <summary>
        /// Maestros asesores o coinvestigadores
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MaestroCoinvestigador(int id, int code = 0)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);           

            ViewBag.Message = dl.ErrorCodes.ErrorCodeToString(code);

            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = "No disponible";
            if (grupo.tblProblemaInvestigacion.First() != null)
            {
                infogrupo.Descripcion = grupo.tblProblemaInvestigacion.First().Descripcion;
            }
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            infogrupo.Pregunta = "Pregunta no disponible";
            if (grupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
            {
                infogrupo.Pregunta = grupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
            }
            infogrupo.EsCreador = grupo.idUsuario.Equals(userId);
            ViewBag.InfoGrupo = infogrupo;

            var tbcoresearchteacher = db.tblMiembroGrupo
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.tblRol.id == 1);
            if (tbcoresearchteacher.ToList().Count == 0)
            {
                if (infogrupo.EsCreador)
                {
                    var tbgroupmember = new tblMiembroGrupo();
                    tbgroupmember.idGrupoInvestigacion = id;
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
                        return RedirectToAction("IrABitacoras", new { id = id, code = 160 });
                    }
                }
            }
            else
            {
                tbcoresearchteacher = db.tblMiembroGrupo
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.tblRol.id != 2);
            }           
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

                tblGrupoInvestigacion tblgrupo = db.tblGrupoInvestigacion.Find(idGroup);
                InformacionGrupo infogrupo = new InformacionGrupo();
                infogrupo.idGrupo = idGroup;
                infogrupo.Descripcion = "No disponible";
                if (tblgrupo.tblProblemaInvestigacion.First() != null)
                {
                    infogrupo.Descripcion = tblgrupo.tblProblemaInvestigacion.First().Descripcion;
                }
                infogrupo.Institucion = tblgrupo.tblInstitucion.Nombre;
                infogrupo.Municipio = tblgrupo.tblInstitucion.tblMunicipios.NombreMunicipio;
                infogrupo.NombreGrupo = tblgrupo.Nombre;
                infogrupo.Pregunta = "Pregunta no disponible";
                if (tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    infogrupo.Pregunta = tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                infogrupo.EsCreador = true;

                return RedirectToAction("MaestroCoinvestigador", new { id = idGroup, code = 100 });

            }
            catch (Exception)
            {
                return RedirectToAction("IrABitacoras", new { id = idGroup });
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

                tblGrupoInvestigacion tblgrupo = db.tblGrupoInvestigacion.Find(idGroup);
                InformacionGrupo infogrupo = new InformacionGrupo();
                infogrupo.idGrupo = idGroup;
                infogrupo.Descripcion = "No disponible";
                if (tblgrupo.tblProblemaInvestigacion.First() != null)
                {
                    infogrupo.Descripcion = tblgrupo.tblProblemaInvestigacion.First().Descripcion;
                }
                infogrupo.Institucion = tblgrupo.tblInstitucion.Nombre;
                infogrupo.Municipio = tblgrupo.tblInstitucion.tblMunicipios.NombreMunicipio;
                infogrupo.NombreGrupo = tblgrupo.Nombre;
                infogrupo.Pregunta = "Pregunta no disponible";
                if (tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    infogrupo.Pregunta = tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                infogrupo.EsCreador = true;

                return RedirectToAction("MaestroCoinvestigador", new { id = idGroup, code = 100 });

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = idGroup });
            }
        }

        public ActionResult DocumentosSoporte(int id)
        {
            ViewBag.Id = id;
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

        /// <summary>
        /// Ingreso de información de la bitacora
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ParaElMaestro(int idGrupo)
        {
            int id = idGrupo;
            InformacionGrupo infogrupo = new InformacionGrupo();
            var grupo = tblGrupoInvestigacion.Find(id);
            infogrupo.idGrupo = id;
            infogrupo.Descripcion = grupo.tblPreguntaInvestigacion
                .Where(m => m.idGrupoInvestigacion == id)
                .Where(m => m.PreguntaPrincipal).Select(m => m.Pregunta).First();
            infogrupo.Institucion = grupo.tblInstitucion.Nombre;
            infogrupo.Municipio = grupo.tblInstitucion.tblMunicipios.NombreMunicipio;
            infogrupo.NombreGrupo = grupo.Nombre;
            infogrupo.EsCreador = true;
            ViewBag.InfoGrupo = infogrupo;

            //var tbresearchprojectreflexion = db.tblReflexionProyectoInvestigacion.Where(m => m.idGrupoInvestigacion == id).SingleOrDefault();
            return View(grupo.tblReflexionProyectoInvestigacion.OrderByDescending(m=>m.id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ParaElMaestro([Bind(Include = "id,FechaInicio,UltimaModificacion,Proceso,Motivacion,Reflexion,ConceptoAsesor,Revisado,idGrupoInvestigacion")] tblReflexionProyectoInvestigacion tbresearchprojectreflexion)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);

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
            infogrupo.EsCreador = true;
            ViewBag.InfoGrupo = infogrupo;

            int idRol = tblMiembroGrupo.GetRoleMiembro(userId, tbresearchprojectreflexion.idGrupoInvestigacion);
            if (idRol != 1)
            {
                return RedirectToAction("IrABitacoras", new { id = tbresearchprojectreflexion.idGrupoInvestigacion, code = 999 });
            }

            tbresearchprojectreflexion.UltimaModificacion = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.Entry(tbresearchprojectreflexion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IrABitacoras", new { id = tbresearchprojectreflexion.idGrupoInvestigacion, code = 110 });
            }

            return View(tbresearchprojectreflexion);
        }


        /// <summary>
        /// Miembros del grupo
        /// </summary>
        /// <returns></returns>
        public ActionResult MiembroGrupo(int id = 0, int code = 0)
        {
            string userId = AspNetUsers.GetUserId(User.Identity.Name);
            if (id == 0)
            {
                id = tblGrupoInvestigacion.ResearchGroupIdByUser(userId);
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
            infogrupo.EsCreador = grupo.idUsuario.Equals(userId);
            ViewBag.InfoGrupo = infogrupo;
            
            var tbgroupmember = db.tblMiembroGrupo.Where(m => m.idGrupoInvestigacion == id).Where(m => m.idRol == 2);
            ViewBag.idGrupoInvestigacion = id;
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
        /// Invitar usuarios como Colaborador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        public ActionResult InvitarEstudiante(string id, int idGroup)
        {
            try
            {
                var tblinvitacion = new tblInvitacionGrupo();
                tblinvitacion.idGrupo = idGroup;
                tblinvitacion.idUsuario = id;
                tblinvitacion.idRol = 2;
                db.tblInvitacionGrupo.Add(tblinvitacion);
                db.SaveChanges();

                tblGrupoInvestigacion tblgrupo = db.tblGrupoInvestigacion.Find(idGroup);
                InformacionGrupo infogrupo = new InformacionGrupo();
                infogrupo.idGrupo = idGroup;
                infogrupo.Descripcion = "No disponible";
                if (tblgrupo.tblProblemaInvestigacion.First() != null)
                {
                    infogrupo.Descripcion = tblgrupo.tblProblemaInvestigacion.First().Descripcion;
                }
                infogrupo.Institucion = tblgrupo.tblInstitucion.Nombre;
                infogrupo.Municipio = tblgrupo.tblInstitucion.tblMunicipios.NombreMunicipio;
                infogrupo.NombreGrupo = tblgrupo.Nombre;
                infogrupo.Pregunta = "Pregunta no disponible";
                if (tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    infogrupo.Pregunta = tblgrupo.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                infogrupo.EsCreador = true;

                return RedirectToAction("MiembroGrupo", new { id = idGroup, code = 100 });

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = idGroup });
            }
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
        
        #endregion

        #region Bitacora 2 Perturbacion de las Ondas
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
                return RedirectToAction("IrABitacoras", new { id = tbresearchquestion.idGrupoInvestigacion, code = 120 });
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
                return RedirectToAction("IrAbitacoras", new { id = tbresearchprojectquestion.idGrupoInvestigacion, code = 120 });
            }
            return View(tbresearchprojectquestion);
        }

        #endregion

        #region Bitacora 3 Superposición de las ondas
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
                return RedirectToAction("IrAbitacoras", new { id = tbresearchproblem.idGrupoInvestigacion, code = 120 });
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
                return RedirectToAction("IrAbitacoras", new { id = tbresearchprojectproblem.idGrupoInvestigacion, code = 120 }); 
            }
            return View(tbresearchprojectproblem);
        }        

        #endregion

        #region Bitacora 4 Presupuesto
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
                return RedirectToAction("IrAbitacoras", new { id = tbpresupuesto.idGrupoInvestigacion, code = 400 });                
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
            if (presupuesto.Count == 0)
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
        #endregion

        #region Bitacora 5 Recorrido
        public ActionResult AgregarEstadoArte(int idGrupoInvestigacion)
        {
            tblEstadoArteProyectoInvestigacion tblestado = new tblEstadoArteProyectoInvestigacion();
            tblestado.idGrupoInvestigacion = idGrupoInvestigacion;
            return View(tblestado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarEstadoArte([Bind(Include = "id,idGrupoInvestigacion,TemaInvestigacion,MapaConceptual")] tblEstadoArteProyectoInvestigacion tblestado)
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
        public ActionResult AgregarInformacion([Bind(Include = "id,idGrupoInvestigacion,idInstrumento,Evidencia,Descripcion")] tblRecoleccionInformacionProyectoInvestigacion tblinformacion)
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
        #endregion

        #region Bitacora 6 Recorrido

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarReflexionDeLaOnda(tblReflexionOnda reflexionDeLaOnda)
        {
            reflexionDeLaOnda.FechaInicio = System.DateTime.Now;
            reflexionDeLaOnda.FechaFin = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {                    
                    db.tblReflexionOnda.Add(reflexionDeLaOnda);
                    db.SaveChanges();                    
                    return RedirectToAction("Index", new { code = 601, id = reflexionDeLaOnda.id });
                }
                catch
                {
                    return RedirectToAction("Index", new { code = 601, id = reflexionDeLaOnda.id });
                }
                

            }
            return View(reflexionDeLaOnda);
            
        }

            
        #endregion

        #region Bitacora 7 Propagacion de la onda        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaPropagacion([Bind(Include = "id,idGrupoInvestigacion,idTipoFeria,Archivo,Descripcion")] tblPropagacionGrupo model)
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
        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}