using System;
using System.Collections.Generic;
using System.Text;
using UserAccount.Domain.Common.Model;

namespace UserAccount.Domain.UserAccount.Domain.Models
{
   public class UserAccountAllParam
    {

        public long IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string UrlPicture { get; set; }
        public DateTime UpdateDate { get; set; }


        public UserAccountAllParam(long idUser, string firstName, string surName, string lastname, string password, string email, string postalAddress,
            string postalCode,string city, string country, string urlPicture, bool isAdmin, DateTime updateDate)
        {
            IdUser = idUser;
            FirstName = firstName;
            SurName = surName;
            LastName = lastname;
            Password = password;
            Email = email;
            PostalAddress = postalAddress;
            PostalCode = postalCode;
            City = city;
            Country = country;
            UrlPicture = urlPicture;
            IsAdmin = isAdmin;
            UpdateDate = updateDate;
        }


        public UserAccountAllParam()
        {

        }

        // /////////////////////////////////////////////

        //protected override bool EqualsCore(ValueObject other)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override int GetHashCodeCore()
        //{
        //    throw new NotImplementedException();
        //}


    }

}
