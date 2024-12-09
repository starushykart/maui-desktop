namespace Hackathon.App.Models;

public class OfflineTask
{
    public int Id { get; set; }
    public Type Type { get; set; }
    public Guid DocumentId { get; set; }
    
    public required string DocumentName { get; set; }
    public required DateTime CreatedAt { get; set; }
}

public enum Type
{
    None,
    Upload,
    Update,
    Remove
}