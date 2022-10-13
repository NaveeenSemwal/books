using Books.API.Models;
using Books.API.Models.Dto;
using Books.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
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

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        //{
        //    var loginResponse = await _usersService.Login(loginRequest);

        //    if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
        //    {
        //        _aPIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
        //        _aPIResponse.IsSuccess = false;
        //        _aPIResponse.ErrorMessages.Add("Username and Password is incorrect.");

        //        return BadRequest(_aPIResponse);
        //    }

        //    _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
        //    _aPIResponse.IsSuccess = true;
        //    _aPIResponse.Data = loginResponse;

        //    return Ok(_aPIResponse);
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registerationRequest)
        //{
        //    var registerationResponse = await _usersService.Register(registerationRequest);

        //    _aPIResponse.IsSuccess = true;
        //    _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
        //    _aPIResponse.Data = registerationResponse;

        //    return Ok(_aPIResponse);
        //}
    }
}
