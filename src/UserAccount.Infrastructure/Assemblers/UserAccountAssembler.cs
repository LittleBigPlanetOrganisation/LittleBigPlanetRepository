using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure.Dtos;

namespace UserAccount.Infrastructure.Assemblers
{
    public static class UserAccountAssembler
    {
        // Not IEnumerable en retour
        internal static IEnumerable<UserAccountAllParam> ToUserAccountAllParamList(this IEnumerable<UserAccountAllParamDto> param)
        {
            var result = new List<UserAccountAllParam>();
            if(param != null)
            {
                foreach (var item in param)
                {
                    result.Add(new UserAccountAllParam(item.idUser, item.firstName, item.surName, item.lastName, item.password, item.email, item.postalAddress,
                        item.postalCode, item.city, item.country, item.urlPicture, item.isAdmin, item.updateDate));
                }
            }
            return result;
        }

        internal static UserAccountAllParam ToUserAccountAllParam(this UserAccountAllParamDto param) 
        {
            if(param == null)
            {
                return null;
            }
            return new UserAccountAllParam
            {
                IdUser = param.idUser,
                FirstName = param.firstName,
                SurName = param.surName,
                LastName = param.lastName,
                Password = param.password,
                Email = param.email,
                PostalAddress = param.postalAddress,
                PostalCode = param.postalCode,
                City = param.city,
                Country = param.country,
                UrlPicture = param.urlPicture, 
                IsAdmin = param.isAdmin 

            };
        }

        public static UserAccountAllParamDto ToPostUserWhithAllParam(this UserAccountAllParam param)
        {
            if (param == null)
            {
                return null;
            }
            return new UserAccountAllParamDto
            {
                idUser = param.IdUser,
                firstName = param.FirstName,
                surName = param.SurName,
                lastName = param.LastName,
                password = param.Password,
                email = param.Email,
                postalAddress = param.PostalAddress,
                postalCode = param.PostalCode,
                city = param.City,
                country = param.Country,
                urlPicture = param.UrlPicture,
                isAdmin = param.IsAdmin
            };
        }

        public static UserAccountAllParamDto ToPutUserAccountAllParam(this UserAccountAllParam param)
        {
            if (param == null)
            {
                return null;
            }
            return new UserAccountAllParamDto
            {
                idUser = param.IdUser,
                firstName = param.FirstName,
                surName = param.SurName,
                lastName = param.LastName,
                password = param.Password,
                email = param.Email,
                postalAddress = param.PostalAddress,
                postalCode = param.PostalCode,
                city = param.City,
                country = param.Country,
                urlPicture = param.UrlPicture,
                isAdmin = param.IsAdmin,
                updateDate = DateTime.Now

            };
        }
    }
}
