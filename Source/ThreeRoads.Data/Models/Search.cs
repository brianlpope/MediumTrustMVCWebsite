using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeRoads.Data.Models
{
    public class Search
    {
        public int ID { get; set; }
        public string SearchTerm { get; set; }
        public DateTime SearchDate { get; set; }
        public int PagesMatched { get; set; }
    }
}
