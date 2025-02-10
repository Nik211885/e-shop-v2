using Microsoft.Extensions.Caching.Memory;

namespace Application.Interface
{
    public interface IMemoryCacheManager
    {
        IMemoryCache GetMemoryCache(string key);
    }
}