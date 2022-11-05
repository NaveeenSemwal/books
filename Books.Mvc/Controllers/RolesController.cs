using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Books.Mvc.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Books.Mvc.Controllers
{
    public class RolesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string apiURL = "https://localhost:5001/";

            using var client = new HttpClient();

            client.BaseAddress = new Uri(apiURL);
            var response = await client.GetAsync("api/roles");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                if (result.IsSuccess == true && result.ErrorMessages.Count == 0)
                {
                    var roles = JsonConvert.DeserializeObject<List<IdentityRole>>(result.Data.ToString());

                    return View(roles);
                }
            }



            return View();
        }
    }
}
