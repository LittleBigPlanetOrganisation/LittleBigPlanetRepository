using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure.Assemblers;
using UserAccount.Infrastructure.Cache;
using UserAccount.Infrastructure.Dtos;
using UserAccount.Infrastructure.Repository;

namespace UserAccount.Infrastructure
{
    public class UserAccountProvider : IUserAccountProvider
    {
        public IUserAccountRepository UserAccountRepository { get; set; }

        public UserAccountProvider(IOptions<DbClientOptions> sqlOptions, IMemoryCache cache, CacheConfiguration configuration)
        {
            UserAccountRepository = new UserAccountRepository(sqlOptions, cache, configuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAccountAllParam>> GetUserAccountListById(long idUser)
        {
            var result = await UserAccountRepository.GetUserAccountById(idUser).ConfigureAwait(false);
            return result.ToUserAccountAllParamList();
        }

    }
}
