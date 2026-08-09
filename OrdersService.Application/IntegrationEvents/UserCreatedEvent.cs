namespace OrdersService.Application.IntegrationEvents;

public class UserCreatedEvent
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public DateTime CreatedAtUtc { get; set; }
}