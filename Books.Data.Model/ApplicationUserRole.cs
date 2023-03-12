using Microsoft.AspNetCore.Identity;

namespace Books.Data.Model
{
    /// <summary>
    /// M:M relationship b/w ApplicationUser amd ApplicationRole
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUser User { get; set; }

        public ApplicationRole Role { get; set; }
    }
}
