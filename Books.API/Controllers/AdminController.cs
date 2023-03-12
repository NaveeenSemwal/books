using Books.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
    [Route("api/admin")]
    public class AdminController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users.OrderBy(x => x.UserName).Select(x => new
            {
                x.Id,
                x.UserName,
                Roles = x.UserRoles.Select(x => x.Role.Name).ToList()

            }).ToListAsync();

            return Ok(users);
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select atleast one role");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            // In case admin has added new roles
            var rolesToAdd = selectedRoles.Except(userRoles);

            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);

            if (!result.Succeeded) return BadRequest("Failed to add roles");

            // In case admin has deleted existing roles
            var rolesToDelete = userRoles.Except(selectedRoles);

            result = await _userManager.RemoveFromRolesAsync(user, rolesToDelete);

            if (!result.Succeeded) return BadRequest("Failed to remove roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult> GetPhotosForModeration()
        {
            return Ok("Admins or moderators can see this");
        }


    }
}
