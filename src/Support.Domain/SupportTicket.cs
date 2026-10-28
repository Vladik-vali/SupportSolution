namespace Support.Domain;

public class SupportTicket
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public TicketStatus Status { get; private set; }

    private SupportTicket() { }

    public SupportTicket(Guid id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Заголовок не может быть пустым", nameof(title));

        Id = id;
        Title = title;
        Status = TicketStatus.New;
    }

    public void ChangeStatus(TicketStatus newStatus)
    {
        if (Status == TicketStatus.Closed)
            throw new InvalidOperationException("Закрытую заявку нельзя изменить");

        if (Status == newStatus)
            throw new InvalidOperationException("Статус уже установлен");

        if (Status == TicketStatus.New && newStatus == TicketStatus.Closed)
            throw new InvalidOperationException("Нельзя закрыть заявку без начала работы");

        Status = newStatus;
    }
}