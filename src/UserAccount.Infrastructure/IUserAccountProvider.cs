using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure.Dtos;

namespace UserAccount.Infrastructure
{
    public interface IUserAccountProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task UpdateUserAccount(UserAccountAllParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<IEnumerable<UserAccountAllParam>> GetUserAccountListById(long idUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="surName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserAccountAllParam> GetUserAccountListByLogin(string surName, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task PostUserWhithAllParam(UserAccountAllParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task DeleteUserAccount(long idUser);
    }
}
