using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SEG.Models;
using SEG.Models.DataBase;

namespace SEG.Controllers
{
    public class responsaController : Controller
    {
        private readonly Entities db = new Entities();

        // GET: responsa
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<responsabilidade> responsabilidades =
                db.responsabilidades.Where(r => r.Id_Contratista.Equals(currentUser.Id));
            return View(responsabilidades.ToList());
        }

        public ActionResult lista(int id = 0)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<responsabilidade> responsabilidades = db.responsabilidades.Where(r => r.Id_Contratista.Equals(currentUser.Id));
            return View(responsabilidades.ToList());
        }

        // GET: responsa/Details/5
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

        // GET: responsa/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUser> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");
            return View();
        }

        // POST: responsa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Contratista,Descripcion")] responsabilidade responsabilidade)
        {
            if (ModelState.IsValid)
            {
                db.responsabilidades.Add(responsabilidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", responsabilidade.Id_Contratista);
            return View(responsabilidade);
        }

        // GET: responsa/Edit/5
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

        // POST: responsa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Descripcion")] responsabilidade responsabilidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsabilidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Contratista = new SelectList(db.AspNetUsers, "Id", "UserName", responsabilidade.Id_Contratista);
            return View(responsabilidade);
        }

        // GET: responsa/Delete/5
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

        // POST: responsa/Delete/5
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