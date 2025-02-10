namespace Application.Interface
{
    public interface IUserProvider
    {
        bool IsAuthenticated { get; }

        Guid UserId { get; }

        string UserName { get; }
    }
}