namespace Contracts;

public class Document
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required double Size { get; set; }
}