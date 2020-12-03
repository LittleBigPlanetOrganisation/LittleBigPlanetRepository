using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccount.Infrastructure.Cache
{
    public class CacheHelper
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// CacheHelper Ctor
        /// </summary>
        /// <param name="cache">IMemory Cache</param>
        public CacheHelper(IMemoryCache cache)
        {
            this._cache = cache;
        }
    }
}
