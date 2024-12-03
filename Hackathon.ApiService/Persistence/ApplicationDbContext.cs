using System.Reflection;
using Hackathon.ApiService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    public DbSet<DocumentInfo> Documents => Set<DocumentInfo>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}