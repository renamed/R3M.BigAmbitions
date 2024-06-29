using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigAmbitions.Repository.Contexts;
public sealed class BigAmbitionContext : DbContext, IBigAmbitionContext
{
    public BigAmbitionContext(DbContextOptions<BigAmbitionContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigEmployees(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigEmployees(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(e =>
        {
            ConfigRegister(e);
        });
    }

    private static EntityTypeBuilder<T> ConfigRegister<T>(EntityTypeBuilder<T> e)
        where T : class, IRegister
    {
        e.HasKey(i => i.Id);

        e.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        return e;

    }
}
