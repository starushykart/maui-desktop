using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackathon.ApiService.Persistence.Entities;

public class DocumentInfo
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Name { get; set; }
    public long Size { get; set; } 
}

public class DocumentInfoTypeConfiguration : IEntityTypeConfiguration<DocumentInfo>
{
    public void Configure(EntityTypeBuilder<DocumentInfo> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(x => x.Id);
    }
}