using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PivoGo.Domain.OrderAggregate;
using PivoGo.Domain.ProductAggregate;
using PivoGo.Domain.UserAggregate;

namespace PivoGo.Infrastructure.Database;

public class PivoGoContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public PivoGoContext(DbContextOptions<PivoGoContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Product Configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
        });

    }
}
