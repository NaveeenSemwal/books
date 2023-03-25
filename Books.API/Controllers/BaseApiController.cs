using Books.API.ActionFilters;
using Books.Core;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json",new string[] { })]
    public abstract class BaseApiController : ControllerBase
    {
        
    }
}