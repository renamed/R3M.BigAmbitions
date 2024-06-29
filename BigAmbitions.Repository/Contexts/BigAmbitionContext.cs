using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigAmbitions.Repository.Contexts;
public sealed class BigAmbitionContext : DbContext, IBigAmbitionContext
{
    public BigAmbitionContext(DbContextOptions<BigAmbitionContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigGames(modelBuilder);
        ConfigEmployees(modelBuilder);
        ConfigBusinesses(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigGames(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(e =>
        {
            ConfigRegister(e);
            ConfigName(e);
        });
    }

    private static void ConfigEmployees(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(e =>
        {
            ConfigRegister(e);

            e.HasOne(o => o.Business)
                .WithMany(m => m.Employees)
                .HasForeignKey(o => o.BusinessId);
        });
    }

    private static void ConfigBusinesses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>(e =>
        {
            ConfigRegister(e);
            ConfigName(e);

            e.HasOne(i => i.Game)
                .WithMany(x => x.Businesses)
                .HasForeignKey(f => f.GameId);
        });
    }

    private static void ConfigName<T>(EntityTypeBuilder<T> e)
        where T : class, IName
    {
        e.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

        e.HasIndex(i => i.Name).IsUnique();
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
