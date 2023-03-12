using Books.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Data.Model
{
    [Table("ApplicationUser")]
    public class ApplicationUser : IdentityUser<int>
    {

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Introducation { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<Photo> Photos { get; set; } = new();

        // Navigation property
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }

    }
}
