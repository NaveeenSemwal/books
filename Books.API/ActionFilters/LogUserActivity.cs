using AutoMapper;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.Core.Extensions;
using Books.Core.Repositories.Abstract;
using Books.Core.Repositories.Implementation.EntityFramework;
using Microsoft.AspNetCore.Mvc.Filters;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LogUserActivity(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // This means the Task is completed and it will return ActionExecutedContext.
            // That means you can do your stuff after the execution of API task.
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            object userId = resultContext.HttpContext.User.GetUserId();

            //var service = resultContext.HttpContext.RequestServices.GetRequiredService<IUsersService>();

            var user = await _unitOfWork.UserRepository.GetAsync(userId);

            user.LastActive = DateTime.UtcNow;

            await _unitOfWork.Complete();
        }
    }
}
