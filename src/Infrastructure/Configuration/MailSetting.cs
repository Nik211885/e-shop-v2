namespace Infrastructure.Configuration
{
    public record MailSetting(string EmailId, 
        string Name,
        string UserName,
        string Password,
        string Host, 
        int Port,
        bool UseSsl);
}