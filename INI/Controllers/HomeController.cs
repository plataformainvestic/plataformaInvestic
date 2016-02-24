using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using INI.Models;
using INI.Models.DataBase;
using INI.Models.Database.Adds;
using INI.Models.Adds;
using INI.Models.BusquedaInvestigacion;
using INI.ChamiloWS;
using INI.Extensions.ActionResults;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using INI.Models.Mymodels;


namespace INI.Controllers
{

    public class HomeController : Controller
    {

        private investicEntities db = new investicEntities();
        private chamiloEntities dc = new chamiloEntities();


        // GET: Home
        public ActionResult Index()
        {           
            return View();
        }

        [Authorize]
        public ActionResult Chat()
        {
            ViewBag.Name =AspNetUsers.GetName(User.Identity.Name);            
            return View();
        }

        public ActionResult AsesoresZona()
        {
            return View("AsesoresZona");
        }
 

        public ActionResult Chamilo(String url)
        {
            string direction = "";
            switch (url)
            {
                case "cr":
                    direction = "http://investic.udenar.edu.co/chamilolms/main/create_course/add_course.php";
                    break;
                case "mc":
                    direction = "http://investic.udenar.edu.co/chamilolms/user_portal.php?&origin=";
                    break;
                case "cc":
                    direction = "http://investic.udenar.edu.co/chamilolms/main/auth/courses.php";
                    break;
                default:
                    direction = "";
                    break;
            }
            ViewBag.Direction = direction;
            return View();
        }

        public ActionResult Tutores()
        {
            //  return View(db.tblTutorZona.Include(m=>m.tblInstitucionEducativa).Include(n=>n.tblTutorZonaSedeEducativa).ToList());
            return PartialView("_tutores");

        }
        //INICIO DEL CONOCENOS
        public ActionResult Conocenos()
        {
            return View();
        }
        public ActionResult General()
        {
            return View();
        }
        public ActionResult EquiposTrabajo()
        {
            return View();
        }
        public ActionResult Alternativas()
        {
            return View();
        }


        public ActionResult mapaasesores()
        {
            return View();
        }
        public ActionResult mapatutores()
        {
            return View();
        }
       //FINAL DEL CONOCENOS
        public ActionResult VerTutores(int id)
        {
            var mmodel = db.tblTutorZona.Where(m => m.tblInstitucion.tblMunicipios.tblRegion_ID == id && m.estaActivo==true).ToList();
            return PartialView("_VerTutores", mmodel);
        }
        public ActionResult VerAsesores(int id) {

            var modeltutores = db.tblAsesorZona.Where(m => m.tblMunicipios.tblRegion_ID == id && m.estaActivo==true).ToList();
            return PartialView("_VerAsesores", modeltutores);
        }

        public ActionResult AsesoresXZona(int id)
        {
            return View("AsesoresZona", db.tblAsesorZona.Where(m => m.tblEquipo_ID == id).ToList());
        }
        // GET: tblTutorZona

        //public ActionResult TutoresTIC()
        //{
        //    //var listatutor= db.tblTutorZona.Include(m =>m.tblTutorZonaSedeEducativa).Include(m=>m.tblTutorZonaSedeEducativa.tbl)
        //    //return View(db.tblTutorZona.ToList());
        //    var tutor = new Tutores();
        //    tutor. = db.tblEquipo.ToList();
        //    tutor.tutZon_nombre = db.tblTutorZona.First().tutZon_nombre;
        //    return View(tutor);
        //}

        public ActionResult IntegrantesEquipos()
        {
            return PartialView("_Integrantes", db.tblIntegrante.Include(m => m.tblEquipo).ToList());
        }
        //SECCIÓN INVESTIGA 
        public ActionResult Aprendizaje()
        {
            ViewBag.chCategorias = new SelectList(dc.course_category, "code", "name");
            return View();
        }
        public ActionResult Investigacion()
        {
            return View();
        }

        public ActionResult BusquedaEstudiantesInvestigando()
        {
            ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //public ActionResult BusquedaEstudiantesInvestigando(SearchProyect busqueda)
        //{
        //    List<tblProyectosInvestigacion> misProyectos = new List<tblProyectosInvestigacion>();
        //    if (busqueda.EsBusquedaAvanzada == 1)
        //    {
        //        //Busqueda simple
        //        if (busqueda.EsProyectoInvestigacion == 1)
        //        {
        //            misProyectos = db.tblProyectosInvestigacion.Where(t => t.proyInv_nombreProyecto.Contains(busqueda.Titulo)).ToList();
        //            if (busqueda.PalabrasClave != null)
        //            {
        //                BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
        //            }
        //        }
        //        else
        //        {
        //            misProyectos = db.tblProyectosInvestigacion.Where(t => t.tblGruposInvestigacion.gruInv_nombreGrupo.Contains(busqueda.Titulo)).ToList();
        //            if (busqueda.PalabrasClave != null)
        //            {
        //                BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
        //            }
        //        }
        //    }
        //    //Busqueda avanzada
        //    else
        //    {
        //        if (busqueda.EsProyectoInvestigacion == 1)
        //        {
        //            misProyectos = db.tblProyectosInvestigacion.Where(
        //                t => t.proyInv_nombreProyecto.Contains(busqueda.Titulo)
        //                || t.tblPresentacionProyecto.tblEjeInvestigacion_ID == busqueda.tblEjeInvestigacion_ID
        //                || (t.proyInv_fechaCreacion >= busqueda.FechaDesde && t.proyInv_fechaCreacion <= busqueda.FechaHasta)
        //                || t.tblGruposInvestigacion.AspNetUsers.Nombres.Contains(busqueda.Autor)
        //                || t.tblGruposInvestigacion.AspNetUsers.Apellidos.Contains(busqueda.Autor)
        //                ).ToList();
        //            if (busqueda.PalabrasClave != null)
        //            {
        //                BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
        //            }
        //        }
        //        else
        //        {
        //            misProyectos = db.tblProyectosInvestigacion.Where(
        //                t => t.tblGruposInvestigacion.gruInv_nombreGrupo.Contains(busqueda.Titulo)
        //                || t.tblPresentacionProyecto.tblEjeInvestigacion_ID == busqueda.tblEjeInvestigacion_ID
        //                || (t.proyInv_fechaCreacion >= busqueda.FechaDesde && t.proyInv_fechaCreacion <= busqueda.FechaHasta)
        //                || t.tblGruposInvestigacion.AspNetUsers.Nombres.Contains(busqueda.Autor)
        //                || t.tblGruposInvestigacion.AspNetUsers.Apellidos.Contains(busqueda.Autor)
        //                ).ToList();
        //            if (busqueda.PalabrasClave != null)
        //            {
        //                BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
        //            }
        //        }
        //    }

        //    if (misProyectos != null)
        //    {
        //        return View("resultadoBusquedaEstudiantesInvestigando", misProyectos);
        //    }
            
        //    ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre");
        //    return View();
        //}
        public ActionResult BusquedaEstudiantesInvestigando(SearchProyect busqueda)
        {
            List<MisGrupos> misGrupos = new List<MisGrupos>();

            String term = busqueda.Titulo;

            var listaGrupos = db.tblGrupoInvestigacion.Where(m => m.Nombre.Contains(term));

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
                if (item.tblPreguntaInvestigacion.Count > 0 && item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First() != null)
                {
                    i.Pregunta = item.tblPreguntaInvestigacion.Where(g => g.PreguntaPrincipal).First().Pregunta;
                }
                m.Informacion = i;
                m.Avatar = item.Avatar;
                m.id = item.id;
                misGrupos.Add(m);
            }

            return View("resultadoBusquedaEstudiantesInvestigando", misGrupos);
        }

        public ActionResult resultadoBusquedaGruposEstudiante(string term = "")
        {
            List<MisGrupos> misGrupos = new List<MisGrupos>();

            var listaGrupos = db.tblGrupoInvestigacion.Where(m => m.Nombre.Contains(term));

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

            return View(misGrupos);
        }



        public ActionResult VisualizarGrupoEstudiantes(int? idGrupoInvestigacion)
        {
            if (idGrupoInvestigacion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblgrpinv = db.tblGrupoInvestigacion.Where(t => t.id == idGrupoInvestigacion).OrderByDescending(m=>m.id).First();

            if (tblgrpinv == null)
            {
                return HttpNotFound();
            }

            return View(tblgrpinv);
        }

        public ActionResult VisualizarGrupoEstudiantes2(int id=0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblgrpinv = db.tblGrupoInvestigacion.Where(t => t.id == id).OrderByDescending(m => m.id).First();

            if (tblgrpinv == null)
            {
                return HttpNotFound();
            }

            return View(tblgrpinv);
        }

        //Busqueda Proyectos Investigación 
        public ActionResult BusquedaDocenteInvestigando()
        {
            ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult BusquedaDocenteInvestigando(SearchProyect busqueda)
        {
            List<tblProyectosInvestigacion> misProyectos = new List<tblProyectosInvestigacion>();
            if (busqueda.EsBusquedaAvanzada == 1)
            {
                //Busqueda simple
                if (busqueda.EsProyectoInvestigacion == 1)
                {
                    misProyectos = db.tblProyectosInvestigacion.Where(t => t.proyInv_nombreProyecto.Contains(busqueda.Titulo)).ToList();
                    if (busqueda.PalabrasClave != null)
                    {
                        BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
                    }
                }
                else
                {
                    misProyectos = db.tblProyectosInvestigacion.Where(t => t.tblGruposInvestigacion.gruInv_nombreGrupo.Contains(busqueda.Titulo)).ToList();
                    if (busqueda.PalabrasClave != null)
                    {
                        BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
                    }
                }
            }
            //Busqueda avanzada
            else
            {
                if (busqueda.EsProyectoInvestigacion == 1)
                {
                    misProyectos = db.tblProyectosInvestigacion.Where(
                        t => t.proyInv_nombreProyecto.Contains(busqueda.Titulo)
                        || t.tblPresentacionProyecto.tblEjeInvestigacion_ID == busqueda.tblEjeInvestigacion_ID
                        || (t.proyInv_fechaCreacion >= busqueda.FechaDesde && t.proyInv_fechaCreacion <= busqueda.FechaHasta)
                        || t.tblGruposInvestigacion.AspNetUsers.Nombres.Contains(busqueda.Autor)
                        || t.tblGruposInvestigacion.AspNetUsers.Apellidos.Contains(busqueda.Autor)
                        ).ToList();
                    if (busqueda.PalabrasClave != null)
                    {
                        BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
                    }
                }
                else
                {
                    misProyectos = db.tblProyectosInvestigacion.Where(
                        t => t.tblGruposInvestigacion.gruInv_nombreGrupo.Contains(busqueda.Titulo)
                        || t.tblPresentacionProyecto.tblEjeInvestigacion_ID == busqueda.tblEjeInvestigacion_ID
                        || (t.proyInv_fechaCreacion >= busqueda.FechaDesde && t.proyInv_fechaCreacion <= busqueda.FechaHasta)
                        || t.tblGruposInvestigacion.AspNetUsers.Nombres.Contains(busqueda.Autor)
                        || t.tblGruposInvestigacion.AspNetUsers.Apellidos.Contains(busqueda.Autor)
                        ).ToList();
                    if (busqueda.PalabrasClave != null)
                    {
                        BusquedaPalabrasClaves(misProyectos, busqueda.PalabrasClave);
                    }
                }
            }

            if (misProyectos != null)
            {
                return View("resultadoBusquedaProyectosInvestigacion", misProyectos);
            }

            ViewBag.tblEjeInvestigacion_ID = new SelectList(db.tblEjeInvestigacion, "tblEjeInvestigacion_ID", "ejeInv_nombre");
            return View();
        }

        private void BusquedaPalabrasClaves(List<tblProyectosInvestigacion> misProyectos, string PalabrasClave)
        {
            List<tblProyectosInvestigacion> filtro = new List<tblProyectosInvestigacion>();
            string[] palabras = PalabrasClave.Split(' ');
            foreach (var item in palabras)
            {
                var miBusquedaPalabrasClaves = misProyectos.Where(t => t.tblPresentacionProyecto.preProy_palabrasClavesProy.Contains(item));
                foreach (var item2 in miBusquedaPalabrasClaves)
                {
                    if (!filtro.Contains(item2))
                    {
                        filtro.Add(item2);
                    }
                }
            }
            misProyectos = filtro;
        }
        
        public ActionResult resultadoBusquedaProyectosInvestigacion(List<tblProyectosInvestigacion> misProyectos)
        {
            //var misProyectos = TempData["resultadoBusquedaProyectos"] as List<tblProyectosInvestigacion>;
            return View(misProyectos);
        }

        public ActionResult VisualizaProyectoInvestigacion(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var miProyectoVisualizado = db.tblProyectosInvestigacion.Where(t => t.tblProyectosInvestigacion_ID == id).FirstOrDefault();
            if (miProyectoVisualizado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id.Value;
            return View(miProyectoVisualizado);
        }
        public ActionResult ReporteProyectoInvestigacion(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var miProyectoVisualizado = db.tblProyectosInvestigacion.Where(t => t.tblProyectosInvestigacion_ID == id).FirstOrDefault();
            if (miProyectoVisualizado == null)
            {
                return HttpNotFound();
            }
            var totalInvestic = 0;
            var totalOtraFuente = 0;
            
            MemoryStream ms = new MemoryStream();

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Proyecto Investigacion");
            doc.AddCreator("Hector Mora");

            // Abrimos el archivo
            doc.Open();
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/") + "logo_investic.png");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            float percentage = 0.0f;
            percentage = 150 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(imagen);

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            #region Titulo Reporte
            doc.Add(new Paragraph("Docentes Investigando"));
            #endregion

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            #region Titulo Proyecto
            if (miProyectoVisualizado.tblPresentacionProyecto!=null && miProyectoVisualizado.tblPresentacionProyecto.preProy_tituloProy != "" && miProyectoVisualizado.tblPresentacionProyecto.preProy_tituloProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblPresentacionProyecto.preProy_tituloProy));            
            #endregion
            doc.Add(Chunk.NEWLINE);

            #region PRESENTACIÓN DEL PROYECTO
            //Aqui estilo de color
            doc.Add(new Paragraph("PRESENTACIÓN DEL PROYECTO"));
            
            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("Resumen"));
            if (miProyectoVisualizado.tblPresentacionProyecto != null && miProyectoVisualizado.tblPresentacionProyecto.preProy_resumenProy != "" && miProyectoVisualizado.tblPresentacionProyecto.preProy_resumenProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblPresentacionProyecto.preProy_resumenProy));


            doc.Add(new Paragraph("Palabras Claves"));

            if (miProyectoVisualizado.tblPresentacionProyecto != null && miProyectoVisualizado.tblPresentacionProyecto.preProy_palabrasClavesProy != "" && miProyectoVisualizado.tblPresentacionProyecto.preProy_palabrasClavesProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblPresentacionProyecto.preProy_palabrasClavesProy));

            doc.Add(new Paragraph("Eje de Investigacion"));

            if (miProyectoVisualizado.tblPresentacionProyecto != null && miProyectoVisualizado.tblPresentacionProyecto.tblEjeInvestigacion.ejeInv_nombre != "" && miProyectoVisualizado.tblPresentacionProyecto.tblEjeInvestigacion.ejeInv_nombre != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblPresentacionProyecto.tblEjeInvestigacion.ejeInv_nombre));
            if (miProyectoVisualizado.tblPresentacionProyecto != null && miProyectoVisualizado.tblPresentacionProyecto.preProy_ejeInvestigacionProy != "" && miProyectoVisualizado.tblPresentacionProyecto.preProy_ejeInvestigacionProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblPresentacionProyecto.preProy_ejeInvestigacionProy));


            #endregion

            #region PROBLEMA DE INVESTIGACIÓN
            //Aqui estilo de color
            doc.Add(new Paragraph("PROBLEMA DE INVESTIGACIÓN"));

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("Planteamiento del Problema"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy!=null &&  miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_planteamientoProblemaProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_planteamientoProblemaProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_planteamientoProblemaProy));

            doc.Add(new Paragraph("Pregunta de Investigación"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy != null && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_preguntaInvestigacionProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_preguntaInvestigacionProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_preguntaInvestigacionProy));

            doc.Add(new Paragraph("Subpregunta de Investigación"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy != null && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_subpreguntaInvestigacionProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_subpreguntaInvestigacionProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_subpreguntaInvestigacionProy));

            doc.Add(new Paragraph("Justificación"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy != null && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_justificacionProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_justificacionProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_justificacionProy));

            doc.Add(new Paragraph("Objetivo General"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy != null && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivoGeneralProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivoGeneralProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivoGeneralProy));

            doc.Add(new Paragraph("Objetivos Específicos"));
            if (miProyectoVisualizado.tblProblemaInvestigacionProy != null && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivosEspecificosProy != "" && miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivosEspecificosProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblProblemaInvestigacionProy.proInvProy_objetivosEspecificosProy));

            #endregion

            #region MARCO DE REFERENCIA
            //Aqui estilo de color
            doc.Add(new Paragraph("MARCO DE REFERENCIA"));

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("Marco Teórico"));
            if (miProyectoVisualizado.tblMarcoReferenciaProy!=null && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoTeoricoProy != "" && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoTeoricoProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoTeoricoProy));

            doc.Add(new Paragraph("Marco de Antecedentes"));
            if (miProyectoVisualizado.tblMarcoReferenciaProy != null && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoAntecedentesProy != "" && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoAntecedentesProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoAntecedentesProy));

            doc.Add(new Paragraph("Marco Contextual"));
            if (miProyectoVisualizado.tblMarcoReferenciaProy != null && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoConceptualProy != "" && miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoConceptualProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMarcoReferenciaProy.marRefProy_marcoConceptualProy));
           

            #endregion

            #region MÉTODO
            //Aqui estilo de color
            doc.Add(new Paragraph("MÉTODO"));

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("Paradigma Metodológico"));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico.parMet_nombre != "" && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico.parMet_nombre != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico.parMet_nombre));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.metProy_paradigmaMetodologicoProy != "" && miProyectoVisualizado.tblMetodoProy.metProy_paradigmaMetodologicoProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.metProy_paradigmaMetodologicoProy));

            doc.Add(new Paragraph("Paradigma Epistemológico"));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.tblParadigmaEpistemologico.parEpi_nombre != "" && miProyectoVisualizado.tblMetodoProy.tblParadigmaEpistemologico.parEpi_nombre != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.tblParadigmaEpistemologico.parEpi_nombre));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.tblTipoEstudioProy.tipEst_nombre != "" && miProyectoVisualizado.tblMetodoProy.tblTipoEstudioProy.tipEst_nombre != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.tblTipoEstudioProy.tipEst_nombre));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.tblDiseniosProy.disProy_nombre != "" && miProyectoVisualizado.tblMetodoProy.tblDiseniosProy.disProy_nombre != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.tblDiseniosProy.disProy_nombre));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.metProy_paradigmaEpistemologicoProy != "" && miProyectoVisualizado.tblMetodoProy.metProy_paradigmaEpistemologicoProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.metProy_paradigmaEpistemologicoProy));

            doc.Add(new Paragraph("Técnicas e Instrumentos de Recolección de Información"));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.metProy_tecnicasInstrumentosProy != "" && miProyectoVisualizado.tblMetodoProy.metProy_tecnicasInstrumentosProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.metProy_tecnicasInstrumentosProy));

            doc.Add(new Paragraph("Procedimientos"));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.metProy_procedimientoProy != "" && miProyectoVisualizado.tblMetodoProy.metProy_procedimientoProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.metProy_procedimientoProy));

            doc.Add(new Paragraph("Plan de Análisis de Datos e Información"));
            if (miProyectoVisualizado.tblMetodoProy != null && miProyectoVisualizado.tblMetodoProy.tblParadigmaMetodologico!=null && miProyectoVisualizado.tblMetodoProy.metProy_planAnalisisDatosProy != "" && miProyectoVisualizado.tblMetodoProy.metProy_planAnalisisDatosProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblMetodoProy.metProy_planAnalisisDatosProy));            

            #endregion

            #region CARACTERISTICAS
            //Aqui estilo de color
            doc.Add(new Paragraph("CARACTERISTICAS"));

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("Resultados Esperados"));
            if (miProyectoVisualizado.tblCaracteristicasProy!=null && miProyectoVisualizado.tblCaracteristicasProy.carProy_resultadosEsperadosProy != "" && miProyectoVisualizado.tblCaracteristicasProy.carProy_resultadosEsperadosProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblCaracteristicasProy.carProy_resultadosEsperadosProy));

            doc.Add(new Paragraph("Caracterización de la Investigación"));
            if (miProyectoVisualizado.tblCaracteristicasProy!=null && miProyectoVisualizado.tblCaracteristicasProy.carProy_caracterizacionProy != "" && miProyectoVisualizado.tblCaracteristicasProy.carProy_caracterizacionProy != null) 
                doc.Add(new Paragraph(miProyectoVisualizado.tblCaracteristicasProy.carProy_caracterizacionProy));
            #endregion

            #region CRONOGRAMA
            //Aqui estilo de color
            doc.Add(new Paragraph("CRONOGRAMA"));

            doc.Add(Chunk.NEWLINE);

            PdfPTable tblCrono = new PdfPTable(4);

            PdfPCell clActividad = new PdfPCell(new Phrase("Actividad", _standardFont));
            clActividad.BorderWidth = 1;
            PdfPCell clFStart = new PdfPCell(new Phrase("Fecha Inicio", _standardFont));
            clFStart.BorderWidth = 1;
            PdfPCell clFEnd = new PdfPCell(new Phrase("Fecha Final", _standardFont));
            clFEnd.BorderWidth = 1;
            PdfPCell clIndicador = new PdfPCell(new Phrase("Indicador", _standardFont));
            clFEnd.BorderWidth = 1;

            tblCrono.AddCell(clActividad);
            tblCrono.AddCell(clFStart);
            tblCrono.AddCell(clFEnd);
            tblCrono.AddCell(clIndicador);

            foreach (var item in miProyectoVisualizado.tblCronogramaProy.tblFechaCronograma)
            {
                
                tblCrono.AddCell(new PdfPCell(new Phrase(item.cro_Actividad,_standardFont)));
                tblCrono.AddCell(new PdfPCell(new Phrase(item.cro_FechaInicio.ToString(),_standardFont)));
                tblCrono.AddCell(new PdfPCell(new Phrase(item.cro_FechaFin.ToString(),_standardFont)));
                tblCrono.AddCell(new PdfPCell(new Phrase(item.cro_Indicador, _standardFont)));                   
                
            }

            doc.Add(tblCrono);
            #endregion

            #region PRESUPUESTO
            //Aqui estilo de color
            doc.Add(new Paragraph("PRESUPUESTO"));

            doc.Add(Chunk.NEWLINE);

            PdfPTable tblPresu = new PdfPTable(4);

            PdfPCell clRubro = new PdfPCell(new Phrase("Rubro", _standardFont));
            clActividad.BorderWidth = 1;
            PdfPCell clFuente = new PdfPCell(new Phrase("Fuente", _standardFont));
            clFStart.BorderWidth = 1;
            PdfPCell clValor = new PdfPCell(new Phrase("Valor", _standardFont));
            clFEnd.BorderWidth = 1;
            PdfPCell clJustificacion = new PdfPCell(new Phrase("Justificación", _standardFont));
            clFEnd.BorderWidth = 1;

            tblPresu.AddCell(clRubro);
            tblPresu.AddCell(clFuente);
            tblPresu.AddCell(clValor);
            tblPresu.AddCell(clJustificacion);

            foreach (var item in miProyectoVisualizado.tblPresupuestoProy.tblRubroPresupuesto)
            {
                if (string.Format("{0}", item.rubPre_fuente).ToUpper().Equals("INVESTIC"))
                {
                    totalInvestic += Int32.Parse(item.rubPre_valor.ToString());
                }
                else
                {
                    totalOtraFuente += Int32.Parse(item.rubPre_valor.ToString());
                }
                tblPresu.AddCell(new PdfPCell(new Phrase(item.tblRubro.Rubro, _standardFont)));
                tblPresu.AddCell(new PdfPCell(new Phrase(item.rubPre_fuente, _standardFont)));
                tblPresu.AddCell(new PdfPCell(new Phrase(item.rubPre_valor.ToString(), _standardFont)));
                tblPresu.AddCell(new PdfPCell(new Phrase(item.rubPre_justificacion, _standardFont)));

            }

            doc.Add(tblPresu);
            doc.Add(new Paragraph(String.Format("Total Aporte Investic: {0}", totalInvestic)));
            doc.Add(new Paragraph(String.Format("Total Otras Fuentes: {0}", totalOtraFuente)));
            doc.Add(new Paragraph(String.Format("Total Presupuesto: {0}", totalInvestic + totalOtraFuente)));

            #endregion


            #region REFERENCIAS
            //Aqui estilo de color
            doc.Add(new Paragraph("REFERENCIAS"));

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("REFERENCIAS"));
            if (miProyectoVisualizado.tblReferenciasProy.refProy_referencias != "" && miProyectoVisualizado.tblReferenciasProy.refProy_referencias!=null)
                doc.Add(new Paragraph(miProyectoVisualizado.tblReferenciasProy.refProy_referencias));          


            #endregion

            doc.Close();
            writer.Close();
            return new PDFResult(ms, "Reporte");
            
        }

        public ActionResult CursosChamilopcat(int idcat)
        {
            List<VerinfoCursos> vercursos = new List<VerinfoCursos>();
            var nombre = "";
            var municipio = "";
            var lstcursos = dc.course.Where(m=>m.category_code.Equals(idcat.ToString())).ToList();
            if(idcat==0)lstcursos=dc.course.ToList();
            foreach (var item in lstcursos)
            {
                IchamiloClient ch = new IchamiloClient();
                chamiloDescriptionCatalog cdesccat = ch.getTutorCource(item.code);
                var description = "";
                try
                {
                    var userId = AspNetUsers.GetUserId(cdesccat.username);
                    nombre = tblMaestroCoInvestigador.GetNameInstitucion(userId);
                    municipio = tblMaestroCoInvestigador.GetMunicipalityInstitucion(userId);
                }
                catch
                {
                    nombre = "";
                    municipio = "";
                }
                finally
                {
                    VerinfoCursos vic = new VerinfoCursos() { idCurso = item.id, NombreCurso = item.title, Profesor = cdesccat.name_complete, SedeEducativa = nombre, municipio = municipio, Fcreacion = cdesccat.creation_date, DescripcionCurso = description };
                    vercursos.Add(vic);
                }
            }
            return PartialView("_Cursosporcat", vercursos);
        }
        public ActionResult CursosTitle(string term)
        {
            List<VerinfoCursos> vercursos = new List<VerinfoCursos>();
            var nombre = "";
            var municipio = "";
            foreach (var item in dc.course.Where(m => m.title.Contains(term.Trim())).ToList())
            {
                IchamiloClient ch = new IchamiloClient();
                chamiloDescriptionCatalog cdesccat = ch.getTutorCource(item.code);
                var description = "";
                try
                {
                    var userId = AspNetUsers.GetUserId(cdesccat.username);
                    nombre = tblMaestroCoInvestigador.GetNameInstitucion(userId);
                    municipio = tblMaestroCoInvestigador.GetMunicipalityInstitucion(userId);
                }
                catch
                {
                    nombre = "";
                    municipio = "";
                }
                finally
                {
                    VerinfoCursos vic = new VerinfoCursos() { idCurso = item.id, NombreCurso = item.title, Profesor = cdesccat.name_complete, SedeEducativa = nombre, municipio = municipio, Fcreacion = cdesccat.creation_date, DescripcionCurso = description };
                    vercursos.Add(vic);
                }
            }
            return PartialView("_CursosTitle", vercursos);
        }
        public ActionResult  CursosChamilo()
        {
            List<VerinfoCursos> vercursos = new List<VerinfoCursos>();
            var nombre = "";
            var municipio = ""; 
            foreach (var item in dc.course.ToList())
            {
                IchamiloClient ch = new IchamiloClient();
                chamiloDescriptionCatalog cdesccat = ch.getTutorCource(item.code);
                var description = "";
                try { 
                    var userId = AspNetUsers.GetUserId(cdesccat.username);
                    nombre = tblMaestroCoInvestigador.GetNameInstitucion(userId);
                    municipio = tblMaestroCoInvestigador.GetMunicipalityInstitucion(userId);                   
                }
                catch
                {
                    nombre = "";
                    municipio = "";
                }
                finally
                {
                    VerinfoCursos vic = new VerinfoCursos() { idCurso=item.id, NombreCurso = item.title, Profesor = cdesccat.name_complete, SedeEducativa = nombre, municipio = municipio, Fcreacion = cdesccat.creation_date, DescripcionCurso = description };
                    vercursos.Add(vic);
                }
            }
            return PartialView("_Cursos", vercursos);
        }

        public ActionResult getDetail(int id)
        {
            var nombre = "";
            var municipio = "";
            var item = dc.course.Where(m => m.id == id).FirstOrDefault();
            VerinfoCursos vic = null;
            if (item != null)
            {
                IchamiloClient ch = new IchamiloClient();
                chamiloDescriptionCatalog cdesccat = ch.getTutorCource(item.code);
                var description = getDescriptionCourse(item.id);
                try
                {
                    var userId = AspNetUsers.GetUserId(cdesccat.username);
                    nombre = tblMaestroCoInvestigador.GetNameInstitucion(userId);
                    municipio = tblMaestroCoInvestigador.GetMunicipalityInstitucion(userId);
                }
                catch
                {
                    nombre = "";
                    municipio = "";
                }
                finally
                {
                    vic = new VerinfoCursos() { idCurso = item.id, NombreCurso = item.title, Profesor = cdesccat.name_complete, SedeEducativa = nombre, municipio = municipio, Fcreacion = cdesccat.creation_date, DescripcionCurso = description };


                }

            }
            return PartialView("getDetail", vic);
        }
        public ActionResult VisualizaProyectoInvestigacionEstudiante(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var miProyectoVisualizado = db.tblProyectosInvestigacion.Where(t => t.tblProyectosInvestigacion_ID == id).FirstOrDefault();
            if (miProyectoVisualizado == null)
            {
                return HttpNotFound();
            }
            return View(miProyectoVisualizado);
        }

       public String getDescriptionCourse(int id)
        {
            var r = from m in dc.c_course_description where m.c_id == id select new { title = m.title, content = m.content };
            String result="";
            foreach (var item in r)
            {
                result += "<strong>"+item.title + "</strong></br>";
                result += "</br>"+item.content+"</br>";
            }
            return result;
        }
        public ActionResult descripcioncursos(int? id)
        {
            return View(dc.course.Where(m => m.id == id).First());
        }
        //public ActionResult Test()
        //{
        //    var tutor = new Tutores();
        //    tutor.equipos = db.tblEquipo.First();
        //    tutor.tutZon_nombre = db.tblTutorZona.First().tutZon_nombre;
        //    return View(tutor);
        //}

        //METODO PARA CONTAR EN CATEGORIAS DE CURSOS

        public ActionResult cuentacat() { 
        
            CuentaCursos contarcursos = new CuentaCursos();
            //tutoresAsesores.nasesores = db.tblAsesorZona.Count(m => m.tblMunicipios.tblRegion_ID == id);
            contarcursos.Notecinf = dc.course.Count(m => m.category_code.Equals("1"));
            contarcursos.NoCNat = dc.course.Count(m => m.category_code.Equals("2"));
            contarcursos.NoMat = dc.course.Count(m => m.category_code.Equals("3"));
            contarcursos.CSoc = dc.course.Count(m => m.category_code.Equals("4"));
            contarcursos.NoLeng = dc.course.Count(m => m.category_code.Equals("5"));
            contarcursos.NoIngles = dc.course.Count(m => m.category_code.Equals("6"));
            contarcursos.NoProyTrans = dc.course.Count(m => m.category_code.Equals("7"));
            contarcursos.Nootros = dc.course.Count(m => m.category_code.Equals("8") || m.category_code.Equals(""));
            return PartialView("_micuentacursos", contarcursos);
        }
        public ActionResult Investiga()
        {
            return View();
        }
        public ActionResult Contactenos()
        {
            return View();
        }
        public ActionResult Recuerdos()
        {
            return View();
        }
        public ActionResult comovamos()
        {
            return View();
        }

        public ActionResult cuentacomovamos(int id = 0)
        {

            if (id == 0)
            {
                TutoresAsesores tutoresAsesores = new TutoresAsesores();

                tutoresAsesores.nasesores = db.tblAsesorZona.Where(m=>m.estaActivo==true).Count();
                tutoresAsesores.ntutores = db.tblTutorZona.Where(m => m.estaActivo == true).Count();
                tutoresAsesores.Gruposinv = db.tblMeta.Sum(m => m.met_grupoInvestigacionEstudiantil);
                tutoresAsesores.estinvestigando = db.tblMeta.Sum(m => m.met_estudianteInvestigando);
                tutoresAsesores.Gruposinvdoc = db.tblMeta.Sum(m => m.met_grupoInvestigacionDocente);
                tutoresAsesores.docenteinvestigando = db.tblMeta.Sum(m => m.met_docenteInvestigando);
                tutoresAsesores.esteducativos = db.tblMeta.Sum(m => m.met_establecimientosEducativos);
                tutoresAsesores.padresformados40 = db.tblMeta.Sum(m => m.met_padresFormados40);
                tutoresAsesores.estudiantesFormados40 = db.tblMeta.Sum(m => m.met_estudiantesFormados40);
                tutoresAsesores.docentesFormados120 = db.tblMeta.Sum(m => m.met_docentesFormados120);
                tutoresAsesores.estudiantesFormados180 = db.tblMeta.Sum(m => m.met_estudiantesFormados180);
                tutoresAsesores.docentesFormados180 = db.tblMeta.Sum(m => m.met_docentesFormados180);
                //tutoresAsesores.ncomputadores = db.tblcompentregados.Count(n=>n.tblInstitucionEducativa.tblMunicipio.tblRegion_ID==id);
                return PartialView("_cuentacomovamos", tutoresAsesores);
            }
            else
            {
                TutoresAsesores tutoresAsesores = new TutoresAsesores();
                tutoresAsesores.nomregion = db.tblRegion.Where(m => m.tblRegion_ID == id).FirstOrDefault().reg_nombre;
                tutoresAsesores.nasesores = db.tblAsesorZona.Count(m => m.tblMunicipios.tblRegion_ID == id);
                tutoresAsesores.ntutores = db.tblTutorZona.Count(n => n.tblInstitucion.tblMunicipios.tblRegion_ID == id);
                tutoresAsesores.Gruposinv = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_grupoInvestigacionEstudiantil);
                tutoresAsesores.estinvestigando = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_estudianteInvestigando);
                tutoresAsesores.Gruposinvdoc = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_grupoInvestigacionDocente);
                tutoresAsesores.docenteinvestigando = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_docenteInvestigando);
                tutoresAsesores.esteducativos = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_establecimientosEducativos);
                tutoresAsesores.padresformados40 = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_padresFormados40);
                tutoresAsesores.estudiantesFormados40 = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_estudiantesFormados40);
                tutoresAsesores.docentesFormados120 = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_docentesFormados120);
                tutoresAsesores.estudiantesFormados180 = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_estudiantesFormados180);
                tutoresAsesores.docentesFormados180 = db.tblMeta.Where(n => n.tblMunicipios.tblRegion_ID == id).Sum(m => m.met_docentesFormados180);
                //tutoresAsesores.ncomputadores = db.tblcompentregados.Count(n=>n.tblInstitucionEducativa.tblMunicipio.tblRegion_ID==id);
                return PartialView("_cuentacomovamos", tutoresAsesores);
            }
        }

        public ActionResult cuentamunicipios(string idmun)
        {

            idmun = idmun.Trim();
            TutoresAsesores tutoresAsesores = new TutoresAsesores();
            tutoresAsesores.nomregion = db.tblMunicipios.Where(m => m.idMunicipio == idmun).FirstOrDefault().NombreMunicipio;
            tutoresAsesores.nasesores = db.tblAsesorZona.Where(m=>m.estaActivo==true).Count(m => m.tblMunicipios.idMunicipio.Equals(idmun));
            tutoresAsesores.ntutores = db.tblTutorZona.Where(m=>m.estaActivo==true).Count(n => n.tblInstitucion.tblMunicipios.idMunicipio.Equals(idmun));
            tutoresAsesores.Gruposinv = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_grupoInvestigacionEstudiantil);
            tutoresAsesores.estinvestigando = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_estudianteInvestigando);
            tutoresAsesores.Gruposinvdoc = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_grupoInvestigacionDocente);
            tutoresAsesores.docenteinvestigando = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_docenteInvestigando);
            tutoresAsesores.esteducativos = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_establecimientosEducativos);
            tutoresAsesores.padresformados40 = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_padresFormados40);
            tutoresAsesores.estudiantesFormados40 = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_estudiantesFormados40);
            tutoresAsesores.docentesFormados120 = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_estudiantesFormados40);
            tutoresAsesores.estudiantesFormados180 = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_estudiantesFormados40);
            tutoresAsesores.docentesFormados180 = db.tblMeta.Where(n => n.tblMunicipios.idMunicipio.Equals(idmun)).Sum(m => m.met_estudiantesFormados40);
            return PartialView("_cuentamunicipios", tutoresAsesores);


        }
        public ActionResult Mapa()
        {
            return View();
        }
        public ActionResult _Subregion(int id)
        {
            return PartialView("_Subregion", id);
        }

        public string SubregionName(int id)
        {
            switch (id)
            {
                case 1:
                    return "SANQUIANGA";
                case 2:
                    return "PACIFICO SUR";
                case 3:
                    return "TELEMBÍ";
                case 4:
                    return "PIE DE MONTE COSTERO";
                case 5:
                    return "EXPROVINCIA DE OBANDO";
                case 6:
                    return "SABANA";
                case 7:
                    return "ABADES";
                case 8:
                    return "OCCIDENTE";
                case 9:
                    return "CORDILLERA";
                case 10:
                    return "CENTRO";
                case 11:
                    return "JUANAMBU";
                case 12:
                    return "RIO MAYO";
                case 13:
                    return "GUAMBUYACO";
                default:
                    return "Nombre No disponible";
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Contactenos(FormCollection form)
        {
            var m = new MailMessage(
                       new MailAddress("investicudenar@gmail.com", "Contactenos"),
                       new MailAddress("comunicacionesinvestic@gmail.com"));
            m.Subject = "Mensaje enviado por Contactenos";
            m.Body =
                string.Format(
                    "Nombres: " + form["nombres"] + "<br />" +
                    "Apellidos: " + form["apellidos"] + "<br />" +
                    "Correo Electrónico: " + form["email"] + "<br />" +
                    "No. Celular: " + form["celular"] + "<br />" +
                    "<br />" + form["mensaje"]);

            m.IsBodyHtml = true;
            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("investicudenar@gmail.com", "Investic666");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("EmailEnviado", "Usuario");
        }

        //Revista digital
        public ActionResult CategoriasRevista()
        {
            return View(db.CategoriaRevista);
        }
        public ActionResult SubCategorias(int id=0)
        {
            var q=db.SubCategoriaRevista.OrderBy(m=>m.nombre).Where(m => m.id_categoria == id);
            ViewBag.Categoria = db.CategoriaRevista.Find(id);
            return View(q);
        }
        public ActionResult Revista(int id=0) {
            try
            {
                var q = db.Revista.OrderBy(m=>m.title).Where(m => m.id_SubCategoria == id);
                var qsc = db.SubCategoriaRevista.Find(id);
                ViewBag.Id = qsc.CategoriaRevista.id;
                return View(q);
            }
            catch
            {
                return View(new Revista());
            }
        }

        public ActionResult RevistaFlip(int id=0)
        {
            var q = db.Revista.Where(m => m.id == id).First();
            String path = q.urlfront;
            String pathPdf = q.urlPdf;
            
            path = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + "images"+Path.DirectorySeparatorChar;

            
            String spath = Server.MapPath("~"+path);
            String sdirectory = Path.GetDirectoryName(spath);
            String directory = Path.GetDirectoryName(path);
            string file = Path.GetFileName(path);
            List<String> lst_img = new List<string>();
            foreach (var item in Directory.GetFiles(sdirectory).Where(m=>m.Contains(".png")))
            {
                string img = "../.."+directory+Path.DirectorySeparatorChar+ Path.GetFileName(item);
                img = img.Replace(@"\", "/");
                img = img.Replace(" ", "%20");                
                lst_img.Add(img);
            }
            lst_img.Sort(new DinoComparer());
            string rootdirectory = "../.."+directory;
            rootdirectory = rootdirectory.Replace(@"\", "/");
            rootdirectory = rootdirectory.Replace(" ", "%20");
            string prefix = Path.GetFileName(lst_img[0]).Replace("1.png","");
            DigitalMagazine dm = new DigitalMagazine() { Directory = rootdirectory, lstFiles = lst_img, Prefix = prefix, UrlPdf=pathPdf };
            
            return View(dm);
        }
        //Repositorio Digital
        public ActionResult CategoriasRepositorio()
        {
            return View(db.CategoriaRepositorio);
        }
        public ActionResult SubCategoriasRepositorio(int id = 0)
        {
            var q = db.SubCategoriaRepositorio.Where(m => m.id_categoria == id);
            ViewBag.Categoria = db.CategoriaRepositorio.Find(id);
            return View(q);
        }
        public ActionResult Repositorio(int id = 0)
        {
            try
            {
                var q = db.Repositorio.Where(m => m.id_SubCategoria == id);
                var qsc = db.SubCategoriaRepositorio.Find(id);
                ViewBag.Id = qsc.CategoriaRepositorio.id;
                return View(q);
            }
            catch
            {
                return View(new Repositorio());
            }

        }

       


        public class DinoComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        // If x is null and y is null, they're
                        // equal. 
                        return 0;
                    }
                    else
                    {
                        // If x is null and y is not null, y
                        // is greater. 
                        return -1;
                    }
                }
                else
                {
                    // If x is not null...
                    //
                    if (y == null)
                    // ...and y is null, x is greater.
                    {
                        return 1;
                    }
                    else
                    {
                        // ...and y is not null, compare the 
                        // lengths of the two strings.
                        //
                        int retval = x.Length.CompareTo(y.Length);

                        if (retval != 0)
                        {
                            // If the strings are not of equal length,
                            // the longer string is greater.
                            //
                            return retval;
                        }
                        else
                        {
                            // If the strings are of equal length,
                            // sort them with ordinary string comparison.
                            //
                            return x.CompareTo(y);
                        }
                    }
                }
            }
        }
        
    }
}