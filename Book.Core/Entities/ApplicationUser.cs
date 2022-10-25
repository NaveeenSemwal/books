using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.API.Entities
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
