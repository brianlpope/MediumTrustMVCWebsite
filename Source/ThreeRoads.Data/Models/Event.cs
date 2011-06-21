using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ThreeRoads.Data.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public bool AllDay { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public bool Editable { get; set; }
        [AllowHtml]
        [UIHint("Html")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Topic { get; set; }
        public virtual Category Category { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string DisplayArea { get; set; }
        public string Location { get; set; }
    }
}