using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Books.Core.Entities;
using Books.Core.Helpers;

namespace Books.API.Services.Abstract
{
    public interface IRolesService
    {
        Task<PagedList<ApplicationRole>> GetRolesAsync(SearchParams searchParams);

        Task<Guid> AddRole(ApplicationRole role);
    }
}
