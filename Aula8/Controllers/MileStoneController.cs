using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aula8.DAL;
using Aula8.Models;

namespace Aula8.Controllers
{
    public class MileStoneController : Controller
    {
        private GerenciamentoDeProjetosContext db = new GerenciamentoDeProjetosContext();

        // GET: MileStone
        public ActionResult Index()
        {
            return View(db.Milestone.ToList());
        }

        // GET: MileStone/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MileStone mileStone = db.Milestone.Find(id);
            if (mileStone == null)
            {
                return HttpNotFound();
            }
            return View(mileStone);
        }

        // GET: MileStone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MileStone/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StatusDaTarefa,DescrisaoDeMetas")] MileStone mileStone)
        {
            if (ModelState.IsValid)
            {
                db.Milestone.Add(mileStone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mileStone);
        }

        // GET: MileStone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MileStone mileStone = db.Milestone.Find(id);
            if (mileStone == null)
            {
                return HttpNotFound();
            }
            return View(mileStone);
        }

        // POST: MileStone/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StatusDaTarefa,DescrisaoDeMetas")] MileStone mileStone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mileStone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mileStone);
        }

        // GET: MileStone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MileStone mileStone = db.Milestone.Find(id);
            if (mileStone == null)
            {
                return HttpNotFound();
            }
            return View(mileStone);
        }

        // POST: MileStone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MileStone mileStone = db.Milestone.Find(id);
            db.Milestone.Remove(mileStone);
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
