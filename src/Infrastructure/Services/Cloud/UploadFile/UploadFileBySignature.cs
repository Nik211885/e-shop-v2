using Application.Interface;
using CloudinaryDotNet;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Cloud.UploadFile
{
    public class UploadFileBySignature(CloudinaryConfig cloudinaryConfig) : IUploadFileBySignature
    {
        private readonly CloudinaryConfig _cloudinaryConfig = cloudinaryConfig;
        public string GetUrl()
        {
            var cloudinary = new Cloudinary(new Account()
            {
                Cloud = _cloudinaryConfig.CloudName,
                ApiKey = _cloudinaryConfig.ApiKey,
                ApiSecret = _cloudinaryConfig.ApiSecret
            });
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var parameters = new SortedDictionary<string, object>
            {
                {"folder", _cloudinaryConfig.UploadFolder},
                {"timestamp", timeStamp},
            };
            var stringSign = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var signature = cloudinary.Api.SignParameters(parameters);
            var url =
                $"https://api.cloudinary.com/v1_1/{_cloudinaryConfig.CloudName}/image/upload?api_key={_cloudinaryConfig.ApiKey}&{stringSign}&signature={signature}";
            return url;
        }
    }
}