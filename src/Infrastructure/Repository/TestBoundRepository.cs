using Core.Entities.Test;
using Core.Interfaces.Repository.Test;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TestBoundRepository(EfApplicationDbContext dbContext) : ITestBoundRepository
    {
        public async Task<EntityBoundTest> AddAsync(EntityBoundTest entity)
        {
            await dbContext.TestCase.AddAsync(entity);
            return entity;
        }

        public async Task<IReadOnlyCollection<EntityBoundTest>> GetAllAsync()
        {
            var result = await dbContext.TestCase.AsNoTracking().ToListAsync();
            return result;
        }
    }
}