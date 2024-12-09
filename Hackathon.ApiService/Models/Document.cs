namespace Hackathon.ApiService.Models;

public class Document
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required long Size { get; set; }
}