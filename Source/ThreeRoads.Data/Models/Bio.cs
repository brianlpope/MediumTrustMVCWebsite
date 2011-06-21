using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ThreeRoads.Data.Models
{
    public class Bio
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        [UIHint("Html")]
        [DataType(DataType.Html)]
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Position { get; set; }
        public string Topic { get; set; }
        public DateTime? PublishedOn { get; set; }
        public virtual Category Category { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}