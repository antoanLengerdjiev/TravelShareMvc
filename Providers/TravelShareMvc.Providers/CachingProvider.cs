using System;
using System.Web.Caching;
using Bytes2you.Validation;
using TravelShareMvc.Providers.Contracts;

namespace TravelShareMvc.Providers
{
    public class CachingProvider : ICachingProvider
    {
        private readonly IHttpContextProvider httpContextProvider;

        public CachingProvider(IHttpContextProvider httpContextProvider)
        {
            Guard.WhenArgument<IHttpContextProvider>(httpContextProvider, "Http context provider cannot be null.")
                .IsNull()
                .Throw();

            this.httpContextProvider = httpContextProvider;
        }

        public void AddItem(string key, object value, DateTime expirationDateTime)
        {
            var cache = this.httpContextProvider.CurrentCache;

            cache.Add(key, value, null, expirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void InsertItem(string key, object value)
        {
            var cache = this.httpContextProvider.CurrentCache;

            cache.Insert(key, value);
        }

        public object GetItem(string key)
        {
            var cache = this.httpContextProvider.CurrentCache;

            return cache.Get(key);
        }
    }
}
