using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using databaseFirstAPP;

namespace databaseFirstAPP.Controllers
{
    public class dificultiesController : Controller
    {
        private hangman_DBfirstEntities db = new hangman_DBfirstEntities();

        // GET: dificulties
        public ActionResult Index()
        {
            return View(db.dificulties.ToList());
        }

        // GET: dificulties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dificulty dificulty = db.dificulties.Find(id);
            if (dificulty == null)
            {
                return HttpNotFound();
            }
            return View(dificulty);
        }

        // GET: dificulties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: dificulties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dificulty_ID,description")] dificulty dificulty)
        {
            if (ModelState.IsValid)
            {
                db.dificulties.Add(dificulty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dificulty);
        }

        // GET: dificulties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dificulty dificulty = db.dificulties.Find(id);
            if (dificulty == null)
            {
                return HttpNotFound();
            }
            return View(dificulty);
        }

        // POST: dificulties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dificulty_ID,description")] dificulty dificulty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dificulty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dificulty);
        }

        // GET: dificulties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dificulty dificulty = db.dificulties.Find(id);
            if (dificulty == null)
            {
                return HttpNotFound();
            }
            return View(dificulty);
        }

        // POST: dificulties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dificulty dificulty = db.dificulties.Find(id);
            db.dificulties.Remove(dificulty);
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
