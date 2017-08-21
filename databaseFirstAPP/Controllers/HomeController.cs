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
    public class HomeController : Controller
    {

        private hangman_DBfirstEntities db = new hangman_DBfirstEntities();


        public ActionResult Index()
        {

            ViewBag.categories_LIST = new SelectList(db.categories, "category_ID", "name");
            return View();
        }

        //POST from selecting difficulty and category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "category_ID,difficulty_ID")] word dific, category categ )
        {

            var words_match = (from word in db.words
                               join cat in db.word_category
                               on word.word_ID equals cat.word_ID

                               where word.difficulty_ID == dific.difficulty_ID
                               where cat.category_ID == categ.category_ID
                               select  word.word1
                               ).ToList();

            var hangmanModel = new Models.hangmanDataModel();

            Random rand_number = new Random();           
            
            int array_pos = rand_number.Next(words_match.Count());

            hangmanModel.Word_ID = words_match.ElementAt(array_pos);



            return View("gameBoard", hangmanModel );
            


        }



        public ActionResult gameBoard()
        {
            ViewBag.Message = "The game is here!";

            return View();
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}