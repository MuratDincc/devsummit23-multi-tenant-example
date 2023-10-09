using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Infrastructure.Repositories.Concretes;

public class LinqToSqlRepository<T> : RepositoryBase<T> where T : class
{
    public LinqToSqlRepository(CommerceDbContext dbContext) : base(dbContext)
    {
    }
}
