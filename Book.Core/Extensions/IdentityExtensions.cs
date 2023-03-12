using System.Linq;
using System.Security.Claims;

namespace Books.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            return userName;
        }

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            return userId;
        }
    }
}
