//using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MAI.Models.DataBase;

namespace MAI.Controllers.MAI
{
    [Authorize]
    public class MAIController : Controller
    {
        private investicEntities db = new investicEntities();

        //------Acciones de la Vista Formacion y Experiencia------
        // GET: Obtiene hoja de vida y publica la información completa
        [AllowAnonymous]
        public ActionResult FormacionYExperiencia()
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            tblHojaVida hojaVida = (from q in db.tblHojaVida where q.tblUsuarioPlataforma_ID == idUsuario select q).FirstOrDefault();
            if (hojaVida == null)
            {
                hojaVida = new tblHojaVida();
                hojaVida.tblUsuarioPlataforma_ID = idUsuario;
                db.tblHojaVida.Add(hojaVida);
                db.SaveChanges();
            }
            return View(hojaVida);
        }

        //--Acciones control de Hoja de Vida
        // GET: HojasVida/Edit/5
        public ActionResult EditarHojaVida(long? id) //Recibe id de la hoja de vida
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHojaVida tblhojavida = db.tblHojaVida.Find(id);
            if (tblhojavida == null)
            {
                return HttpNotFound();
            }
            return View(tblhojavida);
        }
        // POST: HojasVida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarHojaVida([Bind(Include = "tblHojaVida_ID,hojVid_anoGradoSecundaria,hojVid_tituloSecundaria,tblUsuarioPlataforma_ID")] tblHojaVida tblHojaVida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHojaVida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.tblUsuarioPlataforma_ID = AspNetUsers.GetUserId(User.Identity.Name);
            return View(tblHojaVida);
        }


        //--Acciones control de Titulos
        //GET: Adicionar Titulo
        public ActionResult AdicionarTitulo(long? id) //Recibe id de hoja de Vida
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = id;
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblNivelAcademicoEducacionSuperior_ID = new SelectList(db.tblNivelAcademicoEducacionSuperior, "tblNivelAcademicoEducacionSuperior_ID", "nivAcaEduSup_nombre");
            return View();
        }
        //POST: Adicionar Titulo
        // POST: TituloEducacionSuperiors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarTitulo([Bind(Include = "tblTituloEducacionSuperior_ID,titEduSup_nombre,titEduSup_anoGraduacion,tblHojaVida_ID,tblNivelAcademicoEducacionSuperior_ID")] tblTituloEducacionSuperior tblTituloEducacionSuperior)
        {
            if (ModelState.IsValid)
            {
                db.tblTituloEducacionSuperior.Add(tblTituloEducacionSuperior);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblTituloEducacionSuperior.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }

            ViewBag.tblHojaVida_ID = new SelectList(db.tblHojaVida, "tblHojaVida_ID", "hojVid_tituloSecundaria", tblTituloEducacionSuperior.tblHojaVida_ID);
            ViewBag.tblNivelAcademicoEducacionSuperior_ID = new SelectList(db.tblNivelAcademicoEducacionSuperior, "tblNivelAcademicoEducacionSuperior_ID", "nivAcaEduSup_nombre", tblTituloEducacionSuperior.tblNivelAcademicoEducacionSuperior_ID);
            return View(tblTituloEducacionSuperior);
        }
        //GET: Editar Titulo
        public ActionResult ModificarTitulo(long? id) //Recibe id del titulo
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTituloEducacionSuperior tblTituloEducacionSuperior = db.tblTituloEducacionSuperior.Find(id);
            if (tblTituloEducacionSuperior == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = tblTituloEducacionSuperior.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblTituloEducacionSuperior.tblHojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblNivelAcademicoEducacionSuperior_ID = new SelectList(db.tblNivelAcademicoEducacionSuperior, "tblNivelAcademicoEducacionSuperior_ID", "nivAcaEduSup_nombre", tblTituloEducacionSuperior.tblNivelAcademicoEducacionSuperior_ID);
            return View(tblTituloEducacionSuperior);
        }
        // POST: TituloEducacionSuperiors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarTitulo([Bind(Include = "tblTituloEducacionSuperior_ID,titEduSup_nombre,titEduSup_anoGraduacion,tblHojaVida_ID,tblNivelAcademicoEducacionSuperior_ID")] tblTituloEducacionSuperior tblTituloEducacionSuperior)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTituloEducacionSuperior).State = EntityState.Modified;
                db.SaveChanges();

                tblHojaVida hojaVida = (from t in db.tblTituloEducacionSuperior where t.tblTituloEducacionSuperior_ID == tblTituloEducacionSuperior.tblTituloEducacionSuperior_ID select t.tblHojaVida).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.idHojaVida = tblTituloEducacionSuperior.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblTituloEducacionSuperior.tblHojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblNivelAcademicoEducacionSuperior_ID = new SelectList(db.tblNivelAcademicoEducacionSuperior, "tblNivelAcademicoEducacionSuperior_ID", "nivAcaEduSup_nombre", tblTituloEducacionSuperior.tblNivelAcademicoEducacionSuperior_ID);
            return View(tblTituloEducacionSuperior);
        }

        public ActionResult BorrarTitulo(long? id)
        {
            tblTituloEducacionSuperior miTitulo = db.tblTituloEducacionSuperior.Find(id);
            db.tblTituloEducacionSuperior.Remove(miTitulo);
            db.SaveChanges();
            return RedirectToAction("FormacionYExperiencia");
        }


        //--Acciones control de Idiomas
        //GET: Adicionar Idioma
        public ActionResult AdicionarIdioma(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.tblHojaVida_ID = new SelectList(db.tblHojaVida, "tblHojaVida_ID", "hojVid_tituloSecundaria");
            var listaIdiomas = from t in db.tblNivelIdioma where t.tblHojaVida_ID == id select t;
            List<tblIdiomas> tabla = new List<tblIdiomas>();
            foreach (var item in db.tblIdiomas)
            {
                bool esMiIdioma = true;
                foreach (var item2 in listaIdiomas)
                {
                    if (item.tblIdiomas_ID == item2.tblIdiomas_ID)
                    {
                        esMiIdioma = false;
                        break;
                    }
                }
                if (esMiIdioma) tabla.Add(item);
            }

            ViewBag.tblIdiomas_ID = new SelectList(tabla, "tblIdiomas_ID", "idi_nombre");
            ViewBag.idHojaVida = id;
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            //ViewBag.idUsuarioPlataforma = from t in db.tblHojaVida where t.tblHojaVida_ID == id select t.tblUsuarioPlataforma_ID;

            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            return View();
        }
        //POST: Adicionar Idioma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarIdioma([Bind(Include = "tblNivelIdioma_ID,tblHojaVida_ID,tblIdiomas_ID,tblNivel_ID")] tblNivelIdioma tblNivelIdioma)
        {
            if (ModelState.IsValid)
            {
                db.tblNivelIdioma.Add(tblNivelIdioma);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblNivelIdioma.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            ViewBag.idHojaVida = tblNivelIdioma.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblNivelIdioma.tblHojaVida.tblUsuarioPlataforma_ID;
            //ViewBag.tblHojaVida_ID = new SelectList(db.tblHojaVida, "tblHojaVida_ID", "hojVid_tituloSecundaria", tblNivelIdioma.tblHojaVida_ID);
            ViewBag.tblIdiomas_ID = new SelectList(db.tblIdiomas, "tblIdiomas_ID", "idi_nombre", tblNivelIdioma.tblIdiomas_ID);
            return View(tblNivelIdioma);
        }
        // GET: NivelIdiomas/Edit/5
        public ActionResult ModificarIdioma(long? id) //Recibe id del NivelIdioma
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNivelIdioma tblNivelIdioma = db.tblNivelIdioma.Find(id);
            if (tblNivelIdioma == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = tblNivelIdioma.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblNivelIdioma.tblHojaVida.tblUsuarioPlataforma_ID;
            //ViewBag.tblIdiomas_ID = new SelectList(db.tblIdiomas, "tblIdiomas_ID", "idi_nombre", tblNivelIdioma.tblIdiomas_ID);
            ViewBag.idIdioma = tblNivelIdioma.tblIdiomas_ID;
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            return View(tblNivelIdioma);
        }
        // POST: NivelIdiomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarIdioma([Bind(Include = "tblNivelIdioma_ID,tblHojaVida_ID,tblIdiomas_ID,tblNivel_ID")] tblNivelIdioma tblNivelIdioma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNivelIdioma).State = EntityState.Modified;
                db.SaveChanges();

                tblHojaVida hojaVida = (from t in db.tblNivelIdioma where t.tblNivelIdioma_ID == tblNivelIdioma.tblNivelIdioma_ID select t.tblHojaVida).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.idHojaVida = tblNivelIdioma.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblNivelIdioma.tblHojaVida.tblUsuarioPlataforma_ID;
            //ViewBag.tblIdiomas_ID = new SelectList(db.tblIdiomas, "tblIdiomas_ID", "idi_nombre", tblNivelIdioma.tblIdiomas_ID);
            ViewBag.idIdioma = tblNivelIdioma.tblIdiomas_ID;
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            return View(tblNivelIdioma);
        }

        public ActionResult BorrarIdioma(long? id)
        {
            tblNivelIdioma miIdioma = db.tblNivelIdioma.Find(id);
            db.tblNivelIdioma.Remove(miIdioma);
            db.SaveChanges();
            return RedirectToAction("FormacionYExperiencia");
        }


        //--Acciones control de Lenguas
        //GET: Adicionar Lengua
        public ActionResult AdicionarLengua(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listaLenguas = from t in db.tblNivelLengua where t.tblHojaVida_ID == id select t;
            List<tblLenguas> tabla = new List<tblLenguas>();
            foreach (var item in db.tblLenguas)
            {
                bool esMiLengua = true;
                foreach (var item2 in listaLenguas)
                {
                    if (item.tblLenguas_ID == item2.tblLenguas_ID)
                    {
                        esMiLengua = false;
                        break;
                    }
                }
                if (esMiLengua) tabla.Add(item);
            }

            ViewBag.tblLenguas_ID = new SelectList(tabla, "tblLenguas_ID", "len_nombre");
            ViewBag.idHojaVida = id;

            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            return View();
        }
        //POST: Adicionar Lengua
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AdicionarLengua([Bind(Include = "tblNivelLengua_ID,tblHojaVida_ID,tblLenguas_ID,tblNivel_ID")] tblNivelLengua tblNivelLengua)
        {
            if (ModelState.IsValid)
            {
                db.tblNivelLengua.Add(tblNivelLengua);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblNivelLengua.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            //ViewBag.tblHojaVida_ID = new SelectList(db.tblHojaVida, "tblHojaVida_ID", "hojVid_tituloSecundaria", tblNivelIdioma.tblHojaVida_ID);
            ViewBag.tblLenguas_ID = new SelectList(db.tblLenguas, "tblLenguas_ID", "len_nombre", tblNivelLengua.tblLenguas_ID);
            return View(tblNivelLengua);
        }
        // GET: NivelIdiomas/Edit/5
        public ActionResult ModificarLengua(long? id) //Recibe id del NivelLengua
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNivelLengua tblNivelLengua = db.tblNivelLengua.Find(id);
            if (tblNivelLengua == null)
            {
                return HttpNotFound();
            }
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            ViewBag.idHojaVida = tblNivelLengua.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblNivelLengua.tblHojaVida.tblUsuarioPlataforma_ID;
            //ViewBag.tblLenguas_ID = new SelectList(db.tblLenguas, "tblLenguas_ID", "len_nombre", tblNivelLengua.tblLenguas_ID);
            ViewBag.idLengua = tblNivelLengua.tblLenguas_ID;
            return View(tblNivelLengua);
        }
        // POST: NivelIdiomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarLengua([Bind(Include = "tblNivelLengua_ID,tblHojaVida_ID,tblLenguas_ID,tblNivel_ID")] tblNivelLengua tblNivelLengua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNivelLengua).State = EntityState.Modified;
                db.SaveChanges();

                tblHojaVida hojaVida = (from t in db.tblNivelLengua where t.tblNivelLengua_ID == tblNivelLengua.tblNivelLengua_ID select t.tblHojaVida).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("FormacionYExperiencia");
            }
            ViewBag.tblNivel_ID = new SelectList(db.tblNivel, "tblNivel_ID", "tblNivel_nivel");
            ViewBag.idHojaVida = tblNivelLengua.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblNivelLengua.tblHojaVida.tblUsuarioPlataforma_ID;
            //ViewBag.tblIdiomas_ID = new SelectList(db.tblLenguas, "tblLenguas_ID", "len_nombre", tblNivelLengua.tblLenguas_ID);
            ViewBag.idLengua = tblNivelLengua.tblLenguas_ID;
            return View(tblNivelLengua);
        }

        public ActionResult BorrarLengua(long? id)
        {
            tblNivelLengua miLengua = db.tblNivelLengua.Find(id);
            db.tblNivelLengua.Remove(miLengua);
            db.SaveChanges();
            return RedirectToAction("FormacionYExperiencia");
        }


        //----Acciones de la Vista Investigacion------
        public ActionResult Investigacion()
        {
            var idUsuario = AspNetUsers.GetUserId(User.Identity.Name);
            tblHojaVida hojaVida = (from q in db.tblHojaVida where q.tblUsuarioPlataforma_ID == idUsuario select q).FirstOrDefault();
            if (hojaVida == null)
            {
                hojaVida = new tblHojaVida();
                hojaVida.tblUsuarioPlataforma_ID = idUsuario;
                db.tblHojaVida.Add(hojaVida);
                db.SaveChanges();
            }

            return View(hojaVida);
        }
        //--Acciones control de Proyectos
        // GET: ExperienciaProyectos/Create
        public ActionResult AdicionarProyecto(long? id)// Recibe id de la hoja de vida
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = id;
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            return View();
        }
        // POST: ExperienciaProyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarProyecto([Bind(Include = "tblExperienciaProyectos_ID,exppro_tituloProyecto,exppro_anoTerminacion,tblHojaVida_ID")] tblExperienciaProyectos tblExperienciaProyectos)
        {
            if (ModelState.IsValid)
            {
                db.tblExperienciaProyectos.Add(tblExperienciaProyectos);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblExperienciaProyectos.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblExperienciaProyectos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblExperienciaProyectos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblExperienciaProyectos);
        }
        //GET: Modificar Proyecto
        // GET: ExperienciaProyectos/Edit/5
        public ActionResult ModificarProyecto(long? id) //Recibe id del Proyecto
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblExperienciaProyectos tblExperienciaProyectos = db.tblExperienciaProyectos.Find(id);
            if (tblExperienciaProyectos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = tblExperienciaProyectos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblExperienciaProyectos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblExperienciaProyectos);
        }
        // POST: ExperienciaProyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarProyecto([Bind(Include = "tblExperienciaProyectos_ID,exppro_tituloProyecto,exppro_anoTerminacion,tblHojaVida_ID")] tblExperienciaProyectos tblExperienciaProyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblExperienciaProyectos).State = EntityState.Modified;
                db.SaveChanges();

                tblHojaVida hojaVida = (from t in db.tblExperienciaProyectos where t.tblExperienciaProyectos_ID == tblExperienciaProyectos.tblExperienciaProyectos_ID select t.tblHojaVida).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblExperienciaProyectos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblExperienciaProyectos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblExperienciaProyectos);
        }

        public ActionResult BorrarExperienciaProyecto(long? id)
        {
            tblExperienciaProyectos miExperienciaProy = db.tblExperienciaProyectos.Find(id);
            db.tblExperienciaProyectos.Remove(miExperienciaProy);
            db.SaveChanges();
            return RedirectToAction("Investigacion");
        }


        //--Acciones control de Productos Academicos
        // GET: Productos Academicos/Create
        public ActionResult AdicionarProductoAcademico(long? id)// Recibe id de la hoja de vida
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = id;
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblCategoriaProductos_ID = new SelectList(db.tblCategoriaProductos, "tblCategoriaProductos_ID", "catpro_nombre");

            return View();
        }
        // POST: ExperienciaProyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarProductoAcademico([Bind(Include = "tblProductosAcademicos_ID,proaca_tituloProducto,proaca_anoTerminacion,tblCategoriaProductos_ID,tblHojaVida_ID")] tblProductosAcademicos tblProductosAcademicos)
        {
            if (ModelState.IsValid)
            {
                db.tblProductosAcademicos.Add(tblProductosAcademicos);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblProductosAcademicos.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblProductosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblProductosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblCategoriaProductos_ID = new SelectList(db.tblCategoriaProductos, "tblCategoriaProductos_ID", "catpro_nombre");

            return View(tblProductosAcademicos);
        }
        //GET: Modificar Proyecto
        // GET: ExperienciaProyectos/Edit/5
        public ActionResult ModificarProductoAcademico(long? id) //Recibe id del Producto Académico
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductosAcademicos tblProductosAcademicos = db.tblProductosAcademicos.Find(id);
            if (tblProductosAcademicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = tblProductosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblProductosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblCategoriaProductos_ID = new SelectList(db.tblCategoriaProductos, "tblCategoriaProductos_ID", "catpro_nombre");
            return View(tblProductosAcademicos);
        }
        // POST: ExperienciaProyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarProductoAcademico([Bind(Include = "tblProductosAcademicos_ID,proaca_tituloProducto,proaca_anoTerminacion,tblCategoriaProductos_ID,tblHojaVida_ID")] tblProductosAcademicos tblProductosAcademicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProductosAcademicos).State = EntityState.Modified;
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblProductosAcademicos.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblProductosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblProductosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            ViewBag.tblCategoriaProductos_ID = new SelectList(db.tblCategoriaProductos, "tblCategoriaProductos_ID", "catpro_nombre");
            return View(tblProductosAcademicos);
        }

        public ActionResult BorrarProductoAcademica(long? id)
        {
            tblProductosAcademicos miProductoAcademico = db.tblProductosAcademicos.Find(id);
            db.tblProductosAcademicos.Remove(miProductoAcademico);
            db.SaveChanges();
            return RedirectToAction("Investigacion");
        }


        //--Acciones control de Eventos Académicos
        public ActionResult AdicionarEventoAcademico(long? id)// Recibe id de la hoja de vida
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == id select t).FirstOrDefault();
            if (hojaVida == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = id;
            ViewBag.idUsuarioPlataforma = hojaVida.tblUsuarioPlataforma_ID;
            return View();
        }
        // POST: ExperienciaProyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarEventoAcademico([Bind(Include = "tblEventosAcademicos_ID,eveaca_tituloEvento,eveaca_evento,eveaca_lugarEvento,eveaca_anoTerminacion,tblHojaVida_ID")] tblEventosAcademicos tblEventosAcademicos)
        {
            if (ModelState.IsValid)
            {
                db.tblEventosAcademicos.Add(tblEventosAcademicos);
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblEventosAcademicos.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblEventosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblEventosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblEventosAcademicos);
        }
        //GET: Modificar Proyecto
        // GET: ExperienciaProyectos/Edit/5
        public ActionResult ModificarEventoAcademico(long? id) //Recibe id del Evento Académico
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEventosAcademicos tblEventosAcademicos = db.tblEventosAcademicos.Find(id);
            if (tblEventosAcademicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHojaVida = tblEventosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblEventosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblEventosAcademicos);
        }
        // POST: ExperienciaProyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarEventoAcademico([Bind(Include = "tblEventosAcademicos_ID,eveaca_tituloEvento,eveaca_evento,eveaca_lugarEvento,eveaca_anoTerminacion,tblHojaVida_ID")] tblEventosAcademicos tblEventosAcademicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEventosAcademicos).State = EntityState.Modified;
                db.SaveChanges();
                tblHojaVida hojaVida = (from t in db.tblHojaVida where t.tblHojaVida_ID == tblEventosAcademicos.tblHojaVida_ID select t).FirstOrDefault();
                if (hojaVida == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Investigacion");
            }
            ViewBag.idHojaVida = tblEventosAcademicos.tblHojaVida_ID;
            ViewBag.idUsuarioPlataforma = tblEventosAcademicos.tblHojaVida.tblUsuarioPlataforma_ID;
            return View(tblEventosAcademicos);
        }

        public ActionResult BorrarEventoAcademica(long? id)
        {
            tblEventosAcademicos miEventoAcademico = db.tblEventosAcademicos.Find(id);
            db.tblEventosAcademicos.Remove(miEventoAcademico);
            db.SaveChanges();
            return RedirectToAction("Investigacion");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}