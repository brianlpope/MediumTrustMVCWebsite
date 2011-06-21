using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeRoads.Data.Models
{
    public class Article 
    {
        public int ID { get; set; }
        public string Topic { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("Html")]
        [Column(Name="Body", TypeName="ntext")]
        public string Body { get; set; }
        public DateTime? PublishedOn { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}