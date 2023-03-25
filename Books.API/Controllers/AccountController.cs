using Books.Business.Interfaces;
using Books.Business.Model;
using Books.Business.Model.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Books.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json", new string[] { })]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        protected APIResponse _aPIResponse;


        public AccountController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService ??
                throw new ArgumentNullException(nameof(usersService));
            _logger = logger;

            _aPIResponse = new APIResponse();
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var loginResponse = await _usersService.Login(loginRequest);

            if (loginResponse == null || loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _aPIResponse.IsSuccess = false;
                _aPIResponse.ErrorMessages.Add("Username and Password is incorrect.");

                return BadRequest(_aPIResponse);
            }

            _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _aPIResponse.IsSuccess = true;
            _aPIResponse.Data = loginResponse;

            return Ok(_aPIResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registerationRequest)
        {
            var registerationResponse = await _usersService.Register(registerationRequest);

            if (registerationResponse.ErrorMessages.Count == 0)
            {
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                _aPIResponse.IsSuccess = false;
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                _aPIResponse.ErrorMessages = registerationResponse.ErrorMessages;
            }

            _aPIResponse.Data = registerationResponse;

            return Ok(_aPIResponse);
        }
    }
}