using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Api.Assemblers;
using UserAccount.Api.ViewModels;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure;

namespace UserAccount.Api
{
    public class UserAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        private IUserAccountProvider _userAccountProvider;

        /// <summary>
        /// 
        /// </summary>
        private ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userAccountProvider"></param>
        public UserAccountService(ILogger logger, IUserAccountProvider userAccountProvider)
        {
            _userAccountProvider = userAccountProvider ?? throw new ArgumentNullException(nameof(userAccountProvider));
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAccountAllParam>> GetUserAccountListById(long idUser)
        {
            var result = await _userAccountProvider.GetUserAccountListById(idUser).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserAccountAllParam> GetUserAccountListByLogin(string surName, string password)
        {
            var result = await _userAccountProvider.GetUserAccountListByLogin(surName, password).ConfigureAwait(false);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <returns></returns>
        public async Task PostUserWhithAllParam(UserAccountViewModel userAccountViewModel)
        {
            var creation = userAccountViewModel.ToPostUserWhithAllParam();
            await _userAccountProvider.PostUserWhithAllParam(creation).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <returns></returns>
        public async Task UpdateUserAccount(UserAccountViewModel userAccountViewModel)
        {
            await _userAccountProvider.UpdateUserAccount(userAccountViewModel.ToUpdateUserAccount()).ConfigureAwait(false);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task DeleteUserAccount(long idUser)
        {
            await _userAccountProvider.DeleteUserAccount(idUser).ConfigureAwait(false);
        }
    }
}
