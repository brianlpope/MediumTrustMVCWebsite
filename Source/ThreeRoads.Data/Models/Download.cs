using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ThreeRoads.Data.Models
{
    public class Download
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public string FileType { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime? UploadDate { get; set; }
        public string Version { get; set; }
        public string Topic { get; set; }
        public string DisplayArea { get; set; }
    }
}