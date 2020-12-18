using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Api.ViewModels;
using UserAccount.Domain.UserAccount.Domain.Models;

namespace UserAccount.Api.Assemblers
{
    internal static class UserParamAssembler
    {
        internal static IEnumerable<UserAccountViewModel> ToGetUserAccountListById(this IEnumerable<UserAccountAllParam> userAccountAllParams)
        {
            var result = new List<UserAccountViewModel>();
            if (userAccountAllParams == null)
            {
                return result;
            }
            foreach( var item in userAccountAllParams)
            {
                result.Add(new UserAccountViewModel
                {
                    IdUser = item.IdUser,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    SurName = item.SurName,
                    Email = item.Email,
                    PostalAddress = item.PostalAddress,
                    City = item.City,
                    Country = item.Country,
                    PostalCode = item.PostalCode,
                    Password = item.Password,
                    UrlPicture = item.UrlPicture,
                    IsAdmin = item.IsAdmin
                });
            }
            return result;
        }

        internal static UserAccountViewModel ToGetUserAccountByLogin(this UserAccountAllParam userAccountAllParams)
        {
            if (userAccountAllParams == null)
            {
                return null;
            }
            return new UserAccountViewModel
            {
                IdUser = userAccountAllParams.IdUser,
                FirstName = userAccountAllParams.FirstName,
                LastName = userAccountAllParams.LastName,
                SurName = userAccountAllParams.SurName,
                Email = userAccountAllParams.Email,
                PostalAddress = userAccountAllParams.PostalAddress,
                City = userAccountAllParams.City,
                Country = userAccountAllParams.Country,
                PostalCode = userAccountAllParams.PostalCode,
                Password = userAccountAllParams.Password,
                UrlPicture = userAccountAllParams.UrlPicture,
                IsAdmin = userAccountAllParams.IsAdmin
            };
        }

        internal static UserAccountViewModel ToPostUserWhithAllParam (this UserAccountAllParam userAccountAllParams)
        {
            if (userAccountAllParams == null)
            {
                return null;
            }
            return new UserAccountViewModel
            {
                IdUser = userAccountAllParams.IdUser,
                FirstName = userAccountAllParams.FirstName,
                LastName = userAccountAllParams.LastName,
                SurName = userAccountAllParams.SurName,
                Email = userAccountAllParams.Email,
                PostalAddress = userAccountAllParams.PostalAddress,
                City = userAccountAllParams.City,
                Country = userAccountAllParams.Country,
                PostalCode = userAccountAllParams.PostalCode,
                Password = userAccountAllParams.Password,
                UrlPicture = userAccountAllParams.UrlPicture,
                IsAdmin = userAccountAllParams.IsAdmin
            };
        }

        internal static UserAccountViewModel ToUpdateUserAccount(this UserAccountAllParam userAccountAllParams)
        {
            if (userAccountAllParams == null)
            {
                return null;
            }
            return new UserAccountViewModel
            {
                IdUser = userAccountAllParams.IdUser,
                FirstName = userAccountAllParams.FirstName,
                LastName = userAccountAllParams.LastName,
                SurName = userAccountAllParams.SurName,
                Email = userAccountAllParams.Email,
                PostalAddress = userAccountAllParams.PostalAddress,
                City = userAccountAllParams.City,
                Country = userAccountAllParams.Country,
                PostalCode = userAccountAllParams.PostalCode,
                Password = userAccountAllParams.Password,
                UrlPicture = userAccountAllParams.UrlPicture,
                IsAdmin = userAccountAllParams.IsAdmin
            };
        }
    }
}
