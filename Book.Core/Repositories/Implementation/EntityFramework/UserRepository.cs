using Books.API.Contexts;
using Books.API.Entities;
using Books.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Repositories.Implementation.EntityFramework
{
    public class UserRepository : Repository<LocalUser>, IUserRepository
    {
        private BookContext _context;
        private readonly string secretkey;

        public UserRepository(BookContext context, ILogger<UserRepository> logger, IConfiguration configuration) : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.UserName.Equals(userName));

            if (user == null)
            {
                return true;
            }

            return false;
        }

        public async Task<(LocalUser LocalUser, string Token)> Login(LocalUser loginRequestDto)
        {
            var user = await _context.LocalUsers.FirstOrDefaultAsync(x => x.UserName == loginRequestDto.UserName && x.Password == loginRequestDto.Password);

            if (user == null)
            {
                return (null, string.Empty);
            }

            return (user, GenerateJwtToken(user)); 
        }

        public async Task<LocalUser> Register(LocalUser registerationRequestDto)
        {
            LocalUser localUser = new()
            {
                Name = registerationRequestDto.Name,
                UserName = registerationRequestDto.UserName,
                Password = registerationRequestDto.Password,
                Role = registerationRequestDto.Role
            };

            _context.LocalUsers.Add(localUser);
            await _context.SaveChangesAsync();

            return localUser;
        }

        public string GenerateJwtToken(LocalUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

       
    }
}
