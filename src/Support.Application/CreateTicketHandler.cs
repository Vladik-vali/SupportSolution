using Support.Domain;

namespace Support.Application;

public class CreateTicketHandler
{
    private readonly ITicketRepository _repository;

    public CreateTicketHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<SupportTicket> HandleAsync(string title, CancellationToken cancellationToken)
    {
        var ticket = new SupportTicket(Guid.NewGuid(), title);
        await _repository.SaveAsync(ticket, cancellationToken);
        return ticket;
    }
}