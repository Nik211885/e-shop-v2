using Core.Interfaces.Repository.Test;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        ITestBoundRepository TestBoundRepository { get; }
    }
}