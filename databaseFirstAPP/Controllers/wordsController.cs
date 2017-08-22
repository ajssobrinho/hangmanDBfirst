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
    public class wordsController : Controller
    {
        private hangman_DBfirstEntities db = new hangman_DBfirstEntities();

        // GET: words
        public ActionResult Index()
        {
            var words = db.words.Include(w => w.dificulty).Include(c => c.word_category);




            return View(words.ToList()  );
        }

        // GET: words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word word = db.words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // GET: words/Create
        public ActionResult Create()
        {
            ViewBag.difficulty_ID = new SelectList(db.dificulties, "dificulty_ID", "description");
            return View();
        }

        // POST: words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "word_ID,word1,difficulty_ID")] word word)
        {
            if (ModelState.IsValid)
            {
                db.words.Add(word);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.difficulty_ID = new SelectList(db.dificulties, "dificulty_ID", "description", word.difficulty_ID);
            return View(word);
        }

        // GET: words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word word = db.words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            ViewBag.difficulty_ID = new SelectList(db.dificulties, "dificulty_ID", "description", word.difficulty_ID);
            return View(word);
        }

        // POST: words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "word_ID,word1,difficulty_ID")] word word)
        {
            if (ModelState.IsValid)
            {
                db.Entry(word).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.difficulty_ID = new SelectList(db.dificulties, "dificulty_ID", "description", word.difficulty_ID);
            return View(word);
        }

        // GET: words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word word = db.words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            word word = db.words.Find(id);
            db.words.Remove(word);
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
