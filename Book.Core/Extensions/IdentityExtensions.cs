using Books.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            return userName;
        }
    }
}
