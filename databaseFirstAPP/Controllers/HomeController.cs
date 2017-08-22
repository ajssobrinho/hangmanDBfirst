using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using databaseFirstAPP;
using databaseFirstAPP.Models;
using databaseFirstAPP.View_Models;

namespace databaseFirstAPP.Controllers
{

    public class HomeController : Controller
    {

        private hangman_DBfirstEntities db = new hangman_DBfirstEntities();


        public ActionResult Index()
        {

            //ViewBag.categories_LIST = new SelectList(db.categories, "category_ID", "name");

            hangmanViewModel hangmanModelInst = new hangmanViewModel()
            {

                Category = db.categories.ToList(),

                Difficulty = db.dificulties.ToList()

            };

            return View(hangmanModelInst);
        }

        //POST from selecting difficulty and category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult hangmanGame(int category_field, int difficulty_field)
        {

            var words_match = (

                                   from word in db.words

                                   join cat in db.word_category
                                   on word.word_ID equals cat.word_ID


                                   where word.difficulty_ID == difficulty_field
                                   where cat.category_ID == category_field

                                   select word.word1

                                   ).ToList();


            Random rand_number = new Random();

            ViewBag.dados = words_match.ElementAt(rand_number.Next(words_match.Count()));


            hangmanDataModel HangmanDataModel = new hangmanDataModel();

            HangmanDataModel.Word = words_match.ElementAt(rand_number.Next(words_match.Count()));




            return View("gameBoard", HangmanDataModel);



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