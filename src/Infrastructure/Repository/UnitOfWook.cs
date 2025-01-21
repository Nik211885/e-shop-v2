using Core.Interfaces;
using Core.Interfaces.Repository.Test;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UnitOfWork(EfApplicationDbContext dbContext) : IUnitOfWork
    {
        private ITestBoundRepository? _testBoundRepository;
        public ITestBoundRepository TestBoundRepository => _testBoundRepository ??= new TestBoundRepository(dbContext);
    }
}