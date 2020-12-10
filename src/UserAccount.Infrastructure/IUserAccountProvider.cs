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
        Task<IEnumerable<UserAccountAllParam>> GetUserAccountListById(long idUser);
    }
}
