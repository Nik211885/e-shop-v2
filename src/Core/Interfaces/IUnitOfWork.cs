using Core.Interfaces.Repository.Test;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // create instance for repository
        ITestBoundRepository TestBoundRepository { get; }
        
        
        
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        /// <summary>
        ///     if you have transaction just call commit it has call save change before commit 
        /// </summary>
        /// <returns></returns>
        Task CommitTransactionAsync();      
    }
}