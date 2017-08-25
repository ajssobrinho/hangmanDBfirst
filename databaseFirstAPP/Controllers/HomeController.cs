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
        public ActionResult hangmanGame(Nullable<int> category_field, Nullable<int> difficulty_field)
        {
            if (category_field == null || difficulty_field == null)
            {
                return View("index");
            }
            else
            {

                if (category_field <= -1 || difficulty_field <= -1)
                {

                    var words_match = (
                                       from word in db.words

                                       select word.word1

                                       ).ToList();


                    Random rand_number = new Random();




                    if (words_match.Count() != 0)
                    {

                        hangmanDataModel HangmanDataModel = new hangmanDataModel();

                        HangmanDataModel.Word = words_match.ElementAt(rand_number.Next(words_match.Count()));

                        HangmanDataModel.Word_Expl = HangmanDataModel.Word.ToCharArray();
                        HangmanDataModel.Unknown_letters = new char[HangmanDataModel.Word_Expl.Length];

                        for (int i = 0; i < HangmanDataModel.Word_Expl.Length; i++)
                        {

                            HangmanDataModel.Unknown_letters[i] = '?';

                        }


                        HangmanDataModel.Nr_tries = 5;

                        return View("gameBoard", HangmanDataModel);
                    }

                    else
                    {
                        ViewBag.Erro = "Sorry, our database doesn't have a word with the selected parameter.";

                        return View("index", ViewBag);

                    }


                }

                else
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




                    if (words_match.Count() != 0)
                    {

                        hangmanDataModel HangmanDataModel = new hangmanDataModel();

                        HangmanDataModel.Word = words_match.ElementAt(rand_number.Next(words_match.Count()));

                        HangmanDataModel.Word_Expl = HangmanDataModel.Word.ToCharArray();
                        HangmanDataModel.Unknown_letters = new char[HangmanDataModel.Word_Expl.Length];

                        for (int i = 0; i < HangmanDataModel.Word_Expl.Length; i++)
                        {

                            HangmanDataModel.Unknown_letters[i] = '?';

                        }


                        HangmanDataModel.Nr_tries = 5;

                        HangmanDataModel.Win = 0;

                        return View("gameBoard", HangmanDataModel);
                    }

                    else
                    {
                        ViewBag.Erro = "Sorry, our database doesn't have a word with the selected parameter.";

                        return View("index", ViewBag);

                    }


                }
            }
        }




        public ActionResult gameBoard(String Word, char[] hid_letter_array, List<char> letras_usadas, Nullable<int> Nr_tries , String Letter)
        {

            hangmanDataModel HangmanDataModel = new hangmanDataModel();
            //re-setting the var in their propper places in the model.

            HangmanDataModel.Word_Expl = Word.ToCharArray();
            HangmanDataModel.Unknown_letters = hid_letter_array;
            HangmanDataModel.Used_letters = letras_usadas;
            HangmanDataModel.Word = Word;

            bool mistake = true;

            for (int i = 0; i < HangmanDataModel.Word_Expl.Length; i++)
            {
                if (HangmanDataModel.Word_Expl[i] == Letter[0])
                {

                    HangmanDataModel.Unknown_letters[i] = Letter[0];

                    mistake = false;
                }
            }

            bool Winning = false;


            for (int i = 0; i < HangmanDataModel.Unknown_letters.Length; i++)
            {

                if (HangmanDataModel.Unknown_letters[i] != '?')
                {

                    Winning = true;

                }
                else {
                    Winning = false;
                }


            }

            if (mistake == true)
            {

                HangmanDataModel.Nr_tries = HangmanDataModel.Nr_tries - 1;

            }


            if (Winning == true) {

                HangmanDataModel.Win = 1;
            }



            //checked and working----------------------------------------

            if (HangmanDataModel.Used_letters != null)
            {

                if (HangmanDataModel.Used_letters.Contains(Letter[0]) == true)
                {
                    HangmanDataModel.Error_msg_word_al_inserted = "The letter was already tried...";
                }
                else
                {
                    HangmanDataModel.Used_letters.Add(Letter[0]);
                }

            }
            else
            {
                List<char> u_letters = new List<char>();

                HangmanDataModel.Used_letters = u_letters;

                HangmanDataModel.Used_letters.Add(Letter[0]);
            }
            ModelState.Clear();


            return View("gameBoard", HangmanDataModel);
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