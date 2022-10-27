using Books.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Books.API.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Books.Mvc.Dto;

namespace Books.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new RegisterViewModel();

            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new RegisterationRequestDto { Name = model.Name, Email = model.Email, Password = model.Password, Role = "Admin" };

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                string apiURL = "https://localhost:5001/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiURL);

                    //GET Method
                    HttpResponseMessage response = await client.PostAsync("api/users/register", data);
                    if (response.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                        if (result.IsSuccess == true && result.ErrorMessages.Count == 0)
                        {
                            ViewBag.Message = "User Registered sucessfully";
                        }
                        else
                        {
                            AddError(result);
                        }




                        return View();
                    }
                }
            }

            return View(model);
        }

        private void AddError(APIResponse result)
        {
            foreach (var item in result.ErrorMessages)
            {
                ModelState.AddModelError(string.Empty, item);

            }
        }
    }
}
