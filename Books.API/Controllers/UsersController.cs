using Books.API.Entities;
using Books.API.Models;
using Books.API.Models.Dto;
using Books.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
    [Route("api/users")]
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

        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _usersService.GetAll();       
        }
    }
}
