using Microsoft.Extensions.Caching.Memory;
using System;

namespace ReflectionTask
{
    public class TypeFromCache : IGetTypeByParameter
    {
        private readonly IMemoryCache cache;

        public TypeFromCache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public Type GetTypeByName(string name)
        {
            if (cache.TryGetValue(name, out Type cashedType))
                return cashedType;

            return null;
        }
    }
}
