using System.Collections.Concurrent;
using Support.Application;
using Support.Domain;

namespace Support.Infrastructure;

public class InMemoryTicketRepository : ITicketRepository
{
    private readonly ConcurrentDictionary<Guid, SupportTicket> _store = new();

    public Task<SupportTicket?> GetAsync(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(_store.TryGetValue(id, out var ticket) ? ticket : null);

    public Task SaveAsync(SupportTicket ticket, CancellationToken cancellationToken)
    {
        _store[ticket.Id] = ticket;
        return Task.CompletedTask;
    }
}