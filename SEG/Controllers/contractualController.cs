using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEG.Models.DataBase;
using SEG.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SEG.Controllers
{
    public class contractualController : Controller
    {
        private Entities db = new Entities();

        // GET: contractual
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            int equipo = (from e in db.equipos where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

            string idequipo = equipo.ToString();

            List<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();

            //List<responsabilidade> listarespo = new List<responsabilidade>();

            


            //var query =  (from r in db.responsabilidades
            //              join u in db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo))
            //              on r.Id_Contratista equals u.Id
            //              select new { r.Id_Contratista, r.Descripcion });


            

           

            var querytwo = db.responsabilidades.Where(x=>x.Id_Coordinador.Equals(currentUser.Id)).Include(r => r.AspNetUser);



            

            return View(querytwo.ToList());
        }

        // GET: contractual/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            responsabilidade responsabilidade = db.responsabilidades.Find(id);
            if (responsabilidade == null)
            {
                return HttpNotFound();
            }
            return View(responsabilidade);
        }

        // GET: contractual/Create
        public ActionResult Create()
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            int equipo = (from e in db.equipos where e.Id_Coordinador.Equals(currentUser.Id) select e.Id_Equipo).SingleOrDefault();

            string idequipo = equipo.ToString();

            List<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.Equipo.Equals(idequipo)).ToList();  


      //      List<AspNetUser> usuarios = db.AspNetUsers.Where(x => !x.Cargo.Equals("coordinador")).ToList();

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "LastName");


           
            return View();
        }

        // POST: contractual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Contratista,Descripcion")] responsabilidade responsabilidade)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                responsabilidade.Id_Coordinador = currentUser.Id;
                responsabilidade.IdentificadorResponsa = "R"+responsabilidade.Id;

                db.responsabilidades.Add(responsabilidade);
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
            responsabilidade responsabilidade = db.responsabilidades.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Descripcion")] responsabilidade responsabilidade)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

                string idcontratista = (from i in db.responsabilidades where i.Id.Equals(responsabilidade.Id) select i.Id_Contratista).SingleOrDefault();
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
            responsabilidade responsabilidade = db.responsabilidades.Find(id);
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
            responsabilidade responsabilidade = db.responsabilidades.Find(id);
            db.responsabilidades.Remove(responsabilidade);
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
