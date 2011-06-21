using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeRoads.Data.Models
{
    public class SearchResult
    {
        public string ResultType { get; set; }
        [AllowHtml]
        [UIHint("Html")]
        public string Value { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? PublishedOn { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public int MatchCount { get; set; }
    }
}