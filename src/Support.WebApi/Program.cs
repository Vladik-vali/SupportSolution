using Support.Application;
using Support.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем зависимости (DI)
builder.Services.AddSingleton<ITicketRepository, InMemoryTicketRepository>();
builder.Services.AddSingleton<CreateTicketHandler>();

var app = builder.Build();

// Эндпоинт создания заявки
app.MapPost("/tickets", async (CreateTicketRequest req, CreateTicketHandler handler, CancellationToken ct) =>
{
    var ticket = await handler.HandleAsync(req.Title, ct);
    return Results.Created($"/tickets/{ticket.Id}", new 
    { 
        ticket.Id, 
        ticket.Title, 
        Status = ticket.Status.ToString() 
    });
});

// Эндпоинт получения заявки
app.MapGet("/tickets/{id:guid}", async (Guid id, ITicketRepository repo, CancellationToken ct) =>
{
    var ticket = await repo.GetAsync(id, ct);
    return ticket is null
        ? Results.NotFound()
        : Results.Ok(new 
        { 
            ticket.Id, 
            ticket.Title, 
            Status = ticket.Status.ToString() 
        });
});

app.Run();

// DTO для входящего запроса
public record CreateTicketRequest(string Title);