using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.API.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(LogUserActivity))]
    public class BaseApiController : ControllerBase
    {
        
    }
}