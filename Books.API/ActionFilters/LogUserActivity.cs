using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.Core.Extensions;
using Books.Core.Repositories.Abstract;
using Books.Core.Repositories.Implementation.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.API.ActionFilters
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public LogUserActivity(UserManager<ApplicationUser> userManager)
        {

            _userManager = userManager;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // This means the Task is completed and it will return ActionExecutedContext.
            // That means you can do your stuff after the execution of API task.
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            int userId = Convert.ToInt32(resultContext.HttpContext.User.GetUserId());

            if (userId != 0)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                user.LastActive = DateTime.UtcNow;

                await _userManager.UpdateAsync(user);
            }
            else
            {
                context.Result = new BadRequestObjectResult("Error occured while fetching user from Httpcontext");
                return;
            }
        }
    }
}
