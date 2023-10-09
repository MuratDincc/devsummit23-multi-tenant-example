using MediatR;
using Rubic.Caching;

namespace Commerce.Application.Events.Caching;

public record DeleteAllCacheKeysEvent : INotification
{
    public string Prefix { get; init; }
}

public class DeleteAllCacheKeysEventHandler : INotificationHandler<DeleteAllCacheKeysEvent>
{
    private readonly IStaticCacheManager _staticCacheManager;

    public DeleteAllCacheKeysEventHandler(IStaticCacheManager staticCacheManager)
    {
        _staticCacheManager = staticCacheManager;
    }

    public async Task Handle(DeleteAllCacheKeysEvent request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Prefix))
            return;

        await _staticCacheManager.RemoveByPrefixAsync(request.Prefix);
    }
}
