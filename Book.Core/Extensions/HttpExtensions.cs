using Books.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Books.Core.Extensions
{
    /// <summary>
    /// https://stackoverflow.com/questions/25673089/why-is-access-control-expose-headers-needed
    /// </summary>
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            // By default its pascalCase
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
