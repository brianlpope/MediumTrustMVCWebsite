using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThreeRoads.MVC.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "You need to fill in a name")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You need to fill in an email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Your email address contains some errors")]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You need to fill in a comment")]
        [DisplayName("Your comment")]
        public string Comment { get; set; }
    }

}