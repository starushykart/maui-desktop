using ByteSizeLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackathon.ApiService.Persistence.Entities;

public class DocumentInfo
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Name { get; set; }
    public ByteSize Size { get; set; } 
}

public class DocumentInfoTypeConfiguration : IEntityTypeConfiguration<DocumentInfo>
{
    public void Configure(EntityTypeBuilder<DocumentInfo> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Size).HasConversion(
            x => x.Bytes,
            x => ByteSize.FromBytes(x));
    }
}