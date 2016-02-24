using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using INI.Models;
using INI.Models.DataBase;

namespace INI.Controllers
{
    //[Authorize(Roles = "Administrator,Administrador")]
    [Authorize]
    public class responsaController : Controller
    {
        private readonly investicEntities db = new investicEntities();

        // GET: responsa
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<tblResponsabContratista> responsabilidades =
                db.tblResponsabContratista.Where(r => r.Id_Contratista.Equals(currentUser.Id));
            return View(responsabilidades.ToList());
        }

        public ActionResult lista(int id = 0)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());
            IQueryable<tblResponsabContratista> responsabilidades = db.tblResponsabContratista.Where(r => r.Id_Contratista.Equals(currentUser.Id));
            return View(responsabilidades.ToList());
        }

        // GET: responsa/Details/5
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

        // GET: responsa/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser currentUser = manager.FindById(User.Identity.GetUserId());

            IQueryable<AspNetUsers> usuarios = db.AspNetUsers.Where(x => x.UserName.Equals(currentUser.UserName));

            ViewBag.Id_Contratista = new SelectList(usuarios, "Id", "UserName");
            return View();
        }

        // POST: responsa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Contratista,Descripcion")] tblResponsabContratista responsabilidade)
        {
            if (ModelState.IsValid)
            {
                db.tblResponsabContratista.Add(responsabilidade);
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
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Id_Contratista,Descripcion")] tblResponsabContratista responsabilidade)
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
            tblResponsabContratista responsabilidade = db.tblResponsabContratista.Find(id);
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