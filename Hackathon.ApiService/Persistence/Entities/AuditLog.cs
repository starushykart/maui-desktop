namespace Hackathon.ApiService.Persistence.Entities;

public class AuditLog
{
    public Guid Id { get; set; }
    
    public required string Action { get; set; }
}