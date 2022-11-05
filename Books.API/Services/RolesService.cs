using Books.Core.Entities;
using Books.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public Task<Guid> AddRole(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            return _rolesRepository.GetAllAsync();
        }
    }
}
