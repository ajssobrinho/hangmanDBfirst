using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace databaseFirstAPP.Models
{
    public class hangmanDataModel
    {

        public int Nr_tries { get; set; }

        public char[] Unknown_letters { get; set; }
        
        public char[] Word_Expl { get; set; }

        public String Word { get; set; }

        public List<char> Used_letters { get; set; }

        public String Error_msg_word_al_inserted { get; set; }

    }
    

}