using Microsoft.EntityFrameworkCore;

namespace Tenant.Infrastructure.Repositories;

public class TenantDbContext : DbContext
{
    // dotnet ef migrations add v1.0.0 -c Tenant.Infrastructure.Repositories.TenantDbContext -p ../Tenant.Infrastructure
    // dotnet ef migrations script 0 v1.0.0 -c Tenant.Infrastructure.Repositories.TenantDbContext -p ../Tenant.Infrastructure  -o ../../../sql/tenant_db.sql

    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Entities.Pool> Pool { get; set; }
    public DbSet<Entities.PoolDatabase> PoolDatabase { get; set; }
    public DbSet<Entities.Tenant> Tenant { get; set; }
    public DbSet<Entities.TenantUser> TenantUser { get; set; }
    public DbSet<Entities.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantDbContext).Assembly);

        modelBuilder.Entity<Entities.Pool>()
            .HasData(
                new Entities.Pool
                {
                    Id = 1,
                    Title = "Local Pool",
                    Host = "db",
                    Port = "5432",
                    Username = "postgres",
                    Password = "postgres",
                    CreatedOnUtc = DateTimeOffset.UtcNow
                }
            );
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
