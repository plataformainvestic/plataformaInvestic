using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using INI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class contractualController : Controller
    {
        private investicEntities db = new investicEntities();

    
        // GET: contractual
        [Authorize]
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            int equipo = (from e in db.tblEquiposTrabajo where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

            string idequipo = equipo.ToString();

            List<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();

            //List<responsabilidade> listarespo = new List<responsabilidade>();

            


            //var query =  (from r in db.responsabilidades
            //              join u in db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo))
            //              on r.Id_Contratista equals u.Id
            //              select new { r.Id_Contratista, r.Descripcion });


            

           

            var querytwo = db.tblResponsabContratista.Where(x=>x.Id_Coordinador.Equals(currentUser.Id)).Include(r => r.AspNetUsers);



            

            return View(querytwo.ToList());
        }

        // GET: contractual/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
            if (responsabilidade == null)
            {
                return HttpNotFound();
            }
            return View(responsabilidade);
        }

        // GET: contractual/Create
        [Authorize]
        public ActionResult Create()
        {


            if (AspNetUsersRoles.IsUserInRole("Administrador", User.Identity.Name) || AspNetUsersRoles.IsUserInRole("Coordinador", User.Identity.Name))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                int equipo = (from e in db.tblEquiposTrabajo where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

                string idequipo = equipo.ToString();

                List<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();


                //      List<AspNetUser> usuarios = db.AspNetUsers.Where(x => !x.Cargo.Equals("coordinador")).ToList();

                ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");



                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }

        // POST: contractual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Contratista,Descripcion")] tblResponsabContratista responsabilidade)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                //(from c in cxt.pdb_Cotizacions
                // orderby c.Consecutivo descending
                // select c.Consecutivo).FirstOrDefault();

                

                int consecutivo = (from c in db.tblResponsabContratista
                                   where c.Id_Contratista.Equals(responsabilidade.Id_Contratista)
                                   orderby c.Consecutivo descending
                                   select c.Consecutivo).FirstOrDefault();             
          
                responsabilidade.Id_Coordinador = currentUser.Id;

                responsabilidade.Consecutivo = consecutivo + 1;

                responsabilidade.IdentificadorResponsa = "R" + responsabilidade.Consecutivo.ToString();
                
                db.tblResponsabContratista.Add(responsabilidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", responsabilidade.Id_Contratista);
            return View(responsabilidade);
        }

        // GET: contractual/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
            if (responsabilidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", responsabilidade.Id_Contratista);
            return View(responsabilidade);
        }

        // POST: contractual/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Descripcion")] tblResponsabContratista responsabilidade)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                string idcontratista = (from i in db.tblResponsabContratista where i.Id.Equals(responsabilidade.Id) select i.Id_Contratista).SingleOrDefault();
                responsabilidade.Id_Contratista = idcontratista;
                responsabilidade.Id_Coordinador = currentUser.Id;
                db.Entry(responsabilidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", responsabilidade.Id_Contratista);
            return View(responsabilidade);
        }

        // GET: contractual/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
            if (responsabilidade == null)
            {
                return HttpNotFound();
            }
            return View(responsabilidade);
        }

        // POST: contractual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
            db.tblResponsabContratista.Remove(responsabilidade);
            db.SaveChanges();
            return RedirectToAction("Index");
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
