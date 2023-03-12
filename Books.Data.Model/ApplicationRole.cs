using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Data.Model
{
    [Table("ApplicationRole")]
    public class ApplicationRole : IdentityRole<int>
    {
        // Navigation property
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
