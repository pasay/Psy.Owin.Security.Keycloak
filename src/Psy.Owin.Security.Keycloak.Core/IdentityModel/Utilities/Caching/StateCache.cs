using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Psy.Owin.Security.Keycloak.IdentityModel.Utilities.Caching
{
    public class StateCache : MemoryCache
    {
        private const string CachePrefix = "oidc_state_";
        private readonly TimeSpan DefaultCacheLife = new TimeSpan(0, 30, 0); // 30 Minutes

        public StateCache() : this(new MemoryCacheOptions()) { }

        public StateCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public string CreateState(Dictionary<string, object> stateData, TimeSpan? lifeTime = null)
        {
            if (lifeTime == null) lifeTime = DefaultCacheLife;

            var stateKey = CachePrefix + Guid.NewGuid().ToString("N");
            var cache = CreateEntry(stateKey);
            cache.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(lifeTime.Value);
            cache.SetValue(stateData);

            return stateKey;
        }

        public Dictionary<string, object> ReturnState(string stateKey)
        {
            if (TryGetValue(stateKey, out object result))
            {
                Remove(stateKey);
                return result as Dictionary<string, object>;
            }

            return null;
        }
    }
}