using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ThreeRoads.Data.Models
{
    public class Gallery
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string FileType { get; set; }
        [NotMapped]
        public string GalleryName { get; set; }
    }
}
