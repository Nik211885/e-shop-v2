namespace Application.Interface
{
    public interface ISendNotification
    {
        Task SendAsync(string to, string body, string subject, string? nameTo = null, bool isLink = false);
    }
}