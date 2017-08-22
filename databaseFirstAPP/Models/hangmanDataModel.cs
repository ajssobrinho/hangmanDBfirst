using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace databaseFirstAPP.Models
{
    public class hangmanDataModel
    {

        public int Nr_tries { get; set; }

        public char[] Undelines { get; set; }
        
        public char[] Word_Expl { get; set; }

        public String Word { get; set; }

        public char[] UsedLetters { get; set; }
    }
    

}