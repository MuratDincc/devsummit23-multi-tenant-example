using Commerce.Infrastructure.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Infrastructure.Repositories;

public class CommerceDbContext : DbContext
{
    // dotnet ef migrations add v1.0.0 -c Commerce.Infrastructure.Repositories.CommerceDbContext -p ../Commerce.Infrastructure
    // dotnet ef migrations script 0 v1.0.0 -c Commerce.Infrastructure.Repositories.CommerceDbContext -p ../Commerce.Infrastructure  -o ../../../sql/app_db.sql
    
    public string ConnectionString { get; set; }
    
    public CommerceDbContext(DbContextOptions<CommerceDbContext> options, IWorkContext workContext) : base(options)
    {
        ConnectionString = workContext.ConnectionString;
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Entities.Product> Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrEmpty(ConnectionString))
        {
            optionsBuilder.UseNpgsql(ConnectionString,
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Commerce.Infrastructure");
                    optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }).UseSnakeCaseNamingConvention();
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommerceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
