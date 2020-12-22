using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MySqlX.XDevAPI.Common;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="surName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserAccountAllParam> GetUserAccountListByLogin(string surName, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surName))
            {
                return null;
            }
            var result = await UserAccountRepository.GetUserAccountByLogin(surName, password).ConfigureAwait(false);
            return result.ToUserAccountAllParam();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task PostUserWhithAllParam(UserAccountAllParam param)
        {
            UserAccountAllParamDto dto = param.ToPostUserWhithAllParam();
            await UserAccountRepository.CreateUserWhithAllParam(dto).ConfigureAwait(false);


        }

        public async Task UpdateUserAccount(UserAccountAllParam param)
        {
            UserAccountAllParamDto dto = param.ToPutUserAccountAllParam();
            await UserAccountRepository.UpdateUserAccount(dto).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task DeleteUserAccount(long idUser)
        {
            await UserAccountRepository.DeleteUserAccount(idUser).ConfigureAwait(false);
        }

    }
}
