using Rubic.EntityFramework.Repositories.Abstracts;

namespace Tenant.Infrastructure.Repositories.Concretes;

public class LinqToSqlRepository<T> : RepositoryBase<T> where T : class
{
    public LinqToSqlRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }
}
