using AutoMapper;
using Books.API.Services.Abstract;
using Books.Core.Entities;
using Books.Core.Helpers;
using Books.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Services.Implementation
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        public Task<Guid> AddRole(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ApplicationRole>> GetRolesAsync(SearchParams searchParams)
        {
            return _unitOfWork.RolesRepository.GetAllAsync(searchParams);
        }
    }
}
