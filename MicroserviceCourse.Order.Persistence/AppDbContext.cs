using MicroserviceCourse.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Order.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Order> Type { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssembly).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}