using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace databaseFirstAPP.Models
{
    public class hangmanDataModel
    {
        
        public int Nr_tries { get; set; }

        public Array Undelines { get; set; }

        public string Word_ID { get; set; }

        public Array Word { get; set; }
        
    }
}