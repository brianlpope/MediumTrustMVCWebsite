using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ThreeRoads.Data.Models
{
    [Table("QA")]
    public class QA
    {
        public int ID { get; set; }
        public string Question { get; set; }
        [AllowHtml]
        [UIHint("Html")]
        [DataType(DataType.Html)]
        [Column(Name = "Answer", TypeName = "ntext")]
        public string Answer { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }

    }
}
