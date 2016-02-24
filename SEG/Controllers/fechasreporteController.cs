using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SEG.Models.DataBase;

namespace SEG.Controllers
{
    public class fechasreporteController : Controller
    {
        private readonly Entities db = new Entities();

        // GET: fechasreporte
        public ActionResult Index()
        {
            return View(db.fechasreportes.ToList());
        }

        // GET: fechasreporte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fechasreporte fechasreporte = db.fechasreportes.Find(id);
            if (fechasreporte == null)
            {
                return HttpNotFound();
            }
            return View(fechasreporte);
        }

        // GET: fechasreporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id_SemanaTrabajo,Fecha_Ini,Fecha_Fin,Activa")] fechasreporte fechasreporte)
        {
            if (ModelState.IsValid)
            {
                db.fechasreportes.Add(fechasreporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fechasreporte);
        }

        // GET: fechasreporte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fechasreporte fechasreporte = db.fechasreportes.Find(id);
            if (fechasreporte == null)
            {
                return HttpNotFound();
            }
            return View(fechasreporte);
        }

        // POST: fechasreporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id_SemanaTrabajo,Fecha_Ini,Fecha_Fin,Activa")] fechasreporte fechasreporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fechasreporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fechasreporte);
        }

        // GET: fechasreporte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fechasreporte fechasreporte = db.fechasreportes.Find(id);
            if (fechasreporte == null)
            {
                return HttpNotFound();
            }
            return View(fechasreporte);
        }

        // POST: fechasreporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fechasreporte fechasreporte = db.fechasreportes.Find(id);
            db.fechasreportes.Remove(fechasreporte);
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