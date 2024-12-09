namespace Hackathon.App.Models;

public class AuditLog
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
}