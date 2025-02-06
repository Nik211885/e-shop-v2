using Core.Entities.Test;
using Core.Interfaces.Repository.Test;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class TestBoundRepository(EfApplicationDbContext dbContext) : ITestBoundRepository
    {
        public async Task<EntityBoundTest> AddAsync(EntityBoundTest entity)
        {
            await dbContext.TestCase.AddAsync(entity);
            return entity;
        }
    }
}