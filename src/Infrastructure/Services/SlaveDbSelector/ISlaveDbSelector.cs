namespace Infrastructure.Services.SlaveDbSelector
{
    public interface ISlaveDbSelector
    {
        // Implement with Round-Robin algorithm
        string GetConnectionString();
    }
}