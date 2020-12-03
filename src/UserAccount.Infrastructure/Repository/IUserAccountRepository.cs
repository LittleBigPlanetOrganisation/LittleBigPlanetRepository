using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Infrastructure.Dtos;

namespace UserAccount.Infrastructure.Repository
{
    public interface IUserAccountRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task CreateUserWhithAllParam(UserAccountAllParamDto param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task UpdateUserAccount(UserAccountAllParamDto param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<IEnumerable<UserAccountAllParamDto>> GetUserAccountById(long idUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task DeleteUserAccount(long idUser);
    }
}
