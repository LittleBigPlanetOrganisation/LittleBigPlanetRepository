using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccount.Domain.Models
{
    class UserAccountAllParam : Entity<UserAccountKey>
    {
        public string firstName { get; set; }
        public long idUser { get; set; }
        public string lastname { get; set; }
        public string surName { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public string urlPicture { get; set; }
        public DateTime updateDate { get; set; }

        public static UserAccountAllParam CreateNew(UserAccountKey key, string firstName, string lastName, string email, string password,
    string urlPicture, string surName)
        {
            UserAccountAllParam userAccountAllParam = new UserAccountAllParam()
                .WithKey(key)
                .withFirstName(firstName)
                .withLastName(lastName)
                .withEmain(email)
                .withPassword(password)
                .withUrlPicture(urlPicture)
                .withSurName(surName);
            return userAccountAllParam;
        }
    }
}
