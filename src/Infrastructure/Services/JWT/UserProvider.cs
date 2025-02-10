using Application.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services.JWT
{
    public class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
    {
        public bool IsAuthenticated
        {
            get
            {
                bool result = false;
                try
                {
                    if (httpContextAccessor.HttpContext?.User.Identity != null)
                    {
                        result = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                    }
                }
                catch
                {
                    // ignored
                }

                return result;
            }
        }

        public Guid UserId
        {
            get
            {
                Guid result = Guid.Empty;
                try
                {
                    var value = httpContextAccessor.HttpContext?.User.FindFirst(x => x.Type.Equals(ClaimTypes.Name))
                        ?.Value ?? throw new NullReferenceException("Not find user identity");
                }
                catch
                {
                    // ignored
                }

                return result;
            }
        }

        public string UserName
        {
            get
            {
                string result = string.Empty;
                try
                {
                    var value = httpContextAccessor.HttpContext?.User
                        .FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier))
                        ?.Value ?? throw new NullReferenceException("Not find user name");
                }
                catch
                {
                    // ignored
                }

                return result;
            }
        }
    }
}