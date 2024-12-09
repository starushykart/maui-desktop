using Hackathon.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.App.Services.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<OfflineTask> OfflineTasks { get; set; }
    
    public AppDbContext()
    {
        SQLitePCL.Batteries_V2.Init();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks5.db3");
        optionsBuilder
            .UseSqlite($"Filename={dbPath}");
    }
}