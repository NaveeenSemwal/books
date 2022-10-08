using System.Collections.Generic;
using System.Net;

namespace Books.API.Models
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }

        public object Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

    }
}
