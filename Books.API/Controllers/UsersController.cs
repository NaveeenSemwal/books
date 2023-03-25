using Books.Business.Interfaces;
using Books.Business.Model;
using Books.Business.Model.Request;
using Books.Core;
using Books.Core.Extensions;
using Books.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
  
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        protected APIResponse _aPIResponse;


        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService ??
                throw new ArgumentNullException(nameof(usersService));
            _logger = logger;

            _aPIResponse = new APIResponse();
        }

        /// <summary>
        /// [FromQuery] to retrive parameters from query string
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<PagedList<MemberDto>> GetAll([FromQuery] UserParams searchParams)
        {
            return await _usersService.GetAll(searchParams);
        }

        /// <summary>
        /// The Route data by default is string. So no need of {username : string}
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{username}")]
        public async Task<MemberDto> GetbyUserName(string username)
        {
            return await _usersService.Get(username);
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var isUpdated = await _usersService.UpdateUser(memberUpdateDto);

            if (isUpdated)
            {
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                _aPIResponse.IsSuccess = true;
            }

            _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

            return Ok(_aPIResponse);
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<APIResponse>> AddPhoto(IFormFile file)
        {
            var photoDto = await _usersService.AddPhoto(file);

            if (photoDto != null)
            {
               return CreatedAtAction(nameof(GetbyUserName), new { username = "member" },photoDto );
            }

            _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

            return Ok(_aPIResponse);


        }
    }
}
