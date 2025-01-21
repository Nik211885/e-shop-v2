using Core.Entities.Test;
using Core.Interfaces.Repository.Test;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class TestBoundRepository(EfApplicationDbContext dbContext) : ITestBoundRepository
    {
        public Task<EntityBoundTest> AddAsync(EntityBoundTest entity)
        {
            return Task.FromResult(entity);
        }
    }
}