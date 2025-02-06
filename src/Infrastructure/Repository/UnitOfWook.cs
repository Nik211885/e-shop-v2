using Core.Interfaces;
using Core.Interfaces.Repository.Test;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repository
{
    public class UnitOfWork(EfApplicationDbContext dbContext) : IUnitOfWork
    {
        // variable to transaction
        private IDbContextTransaction? _transaction;
        private ITestBoundRepository? _testBoundRepository;
        public ITestBoundRepository TestBoundRepository => _testBoundRepository ??= new TestBoundRepository(dbContext);
        public async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public  async Task BeginTransactionAsync()
        {
            _transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null)
            {
                throw new Exception("Don't have a transaction make commit");
            }
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null)
            {
                throw new Exception("Don't have a transaction make commit");
            }
            try
            {
                await dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}