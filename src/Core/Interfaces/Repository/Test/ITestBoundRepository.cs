using Core.Entities.Test;

namespace Core.Interfaces.Repository.Test
{
    public interface ITestBoundRepository : IRepository<EntityBoundTest>
    {
        Task<EntityBoundTest> AddAsync(EntityBoundTest entity);
    }
}