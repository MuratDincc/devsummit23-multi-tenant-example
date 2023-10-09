using MediatR;
using Rubic.Caching;

namespace Tenant.Application.Events.Caching;

public record DeleteCacheKeysEvent : INotification
{
    public List<string> Keys { get; init; }
}

public class DeleteCacheKeysEventHandler : INotificationHandler<DeleteCacheKeysEvent>
{
    private readonly IStaticCacheManager _staticCacheManager;

    public DeleteCacheKeysEventHandler(IStaticCacheManager staticCacheManager)
    {
        _staticCacheManager = staticCacheManager;
    }

    public async Task Handle(DeleteCacheKeysEvent request, CancellationToken cancellationToken)
    {
        if (request.Keys == null || !request.Keys.Any())
            return;

        foreach (string cacheKey in request.Keys)
        {
            await _staticCacheManager.RemoveAsync(new CacheKey(cacheKey));
        }
    }
}
