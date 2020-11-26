using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UserAccount.Infrastructure.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private readonly Func<IDbConnection> _connection;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// The cache configuration
        /// </summary>
        //private readonly CacheConfiguration _configuration;


        public UserAccountRepository(IOptions<DbClientOptions> connection, IMemoryCache cache /*CacheConfiguration configuration*/)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            /* _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));*/
            _connection = connection?.Value?.LittleBigPlanetData ?? throw new ArgumentNullException(nameof(connection));
        }
    }
}
