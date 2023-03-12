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