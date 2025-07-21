using KafeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KafeApi.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Category> Categories { get; set; }
}
