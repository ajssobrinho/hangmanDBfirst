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
    public class word_categoryController : Controller
    {
        private hangman_DBfirstEntities db = new hangman_DBfirstEntities();

        // GET: word_category
        public ActionResult Index()
        {
            var word_category = db.word_category.Include(w => w.category).Include(w => w.word);
            return View(word_category.ToList());
        }

        // GET: word_category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word_category word_category = db.word_category.Find(id);
            if (word_category == null)
            {
                return HttpNotFound();
            }
            return View(word_category);
        }

        // GET: word_category/Create
        public ActionResult Create()
        {
            ViewBag.category_ID = new SelectList(db.categories, "category_ID", "name");
            ViewBag.word_ID = new SelectList(db.words, "word_ID", "word1");
            return View();
        }

        // POST: word_category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "word_ID,category_ID,word_category_ID")] word_category word_category)
        {
            if (ModelState.IsValid)
            {
                db.word_category.Add(word_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_ID = new SelectList(db.categories, "category_ID", "name", word_category.category_ID);
            ViewBag.word_ID = new SelectList(db.words, "word_ID", "word1", word_category.word_ID);
            return View(word_category);
        }

        // GET: word_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word_category word_category = db.word_category.Find(id);
            if (word_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_ID = new SelectList(db.categories, "category_ID", "name", word_category.category_ID);
            ViewBag.word_ID = new SelectList(db.words, "word_ID", "word1", word_category.word_ID);
            return View(word_category);
        }

        // POST: word_category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "word_ID,category_ID,word_category_ID")] word_category word_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(word_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_ID = new SelectList(db.categories, "category_ID", "name", word_category.category_ID);
            ViewBag.word_ID = new SelectList(db.words, "word_ID", "word1", word_category.word_ID);
            return View(word_category);
        }

        // GET: word_category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            word_category word_category = db.word_category.Find(id);
            if (word_category == null)
            {
                return HttpNotFound();
            }
            return View(word_category);
        }

        // POST: word_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            word_category word_category = db.word_category.Find(id);
            db.word_category.Remove(word_category);
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
