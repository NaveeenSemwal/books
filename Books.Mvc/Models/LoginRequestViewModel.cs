using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Books.Mvc.Models
{
    public class LoginRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
