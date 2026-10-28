using Support.Domain;
using Xunit;

namespace Support.UnitTests;

public class SupportTicketTests
{
    [Fact]
    public void ChangeStatus_NewToInProgress_Succeeds()
    {
        // Arrange
        var ticket = new SupportTicket(Guid.NewGuid(), "Тестовая заявка");

        // Act
        ticket.ChangeStatus(TicketStatus.InProgress);

        // Assert
        Assert.Equal(TicketStatus.InProgress, ticket.Status);
    }

    [Fact]
    public void ChangeStatus_NewToClosed_Throws()
    {
        // Arrange
        var ticket = new SupportTicket(Guid.NewGuid(), "Тестовая заявка");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => 
            ticket.ChangeStatus(TicketStatus.Closed));
    }
}