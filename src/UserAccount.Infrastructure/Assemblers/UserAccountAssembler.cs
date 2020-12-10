using System;
using System.Collections.Generic;
using System.Text;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure.Dtos;

namespace UserAccount.Infrastructure.Assemblers
{
    public static class UserAccountAssembler
    {
        internal static IEnumerable<UserAccountAllParam> ToUserAccountAllParamList(this IEnumerable<UserAccountAllParamDto> param)
        {
            var result = new List<UserAccountAllParam>();
            if(param != null)
            {
                foreach (var item in param)
                {
                    result.Add(new UserAccountAllParam(item.idUser, item.firstName, item.surName, item.lastname, item.password, item.email, item.postalAddress,
                        item.postalCode, item.country, item.urlPicture, item.isAdmin, item.updateDate));
                }
            }
            return result;
        }
    }
}
