namespace Infrastructure.Configuration
{
    public record CloudinaryConfig(string CloudName,
        string ApiKey, 
        string ApiSecret, 
        string UploadFolder);
}