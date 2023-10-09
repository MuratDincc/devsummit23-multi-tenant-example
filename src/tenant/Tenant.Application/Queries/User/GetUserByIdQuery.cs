using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Queries.User.Dto;
using Tenant.Domain.Constants;

namespace Tenant.Application.Queries.User;

public record GetUserByIdQuery : IRequest<GetUserDto>
{
    public int Id { get; init; }
}

public record GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserDto>
{
    private readonly IRepository<Entities.TenantUser> _tenantUserRepository;
    private readonly IRepository<Entities.User> _userRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetUserByIdQueryHandler(IRepository<Entities.TenantUser> tenantUserRepository,
        IRepository<Entities.User> userRepository, 
        IStaticCacheManager staticCacheManager)
    {
        _tenantUserRepository = tenantUserRepository;
        _userRepository = userRepository;
        _staticCacheManager = staticCacheManager;
    }

    public async Task<GetUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.UserById), request.Id.ToString());

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            var user = await _userRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);
            if (user == null)
                throw new StatusException(status: StatusCode.NotFound, "User Not Found!");

            var tenantIds = _tenantUserRepository.Table.AsNoTracking().Where(x => x.UserId == user.Id).Select(x => x.TenantId).ToList();
            
            return new GetUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                CreatedOnUtc = user.CreatedOnUtc,
                UpdatedOnUtc = user.UpdatedOnUtc,
                TenantIds = tenantIds
            };
        });
    }
}
