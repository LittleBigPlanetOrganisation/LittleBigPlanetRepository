using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Domain.UserAccount.Domain.Models;

namespace UserAccount.Api.ViewModels
{
    public class UserAccountViewModel
    {
        /// <summary>
        /// idUser
        /// </summary>
        /// <exemple>1</exemple>
        public long IdUser { get; set; }
        /// <summary>
        /// Firstname of the user
        /// </summary>
        /// <exemple>Sarah</exemple>
        public string FirstName { get; set; }
        /// <summary>
        /// Lastname of the contact
        /// </summary>
        /// <exemple>Croche</exemple>
        public string LastName { get; set; }
        /// <summary>
        /// Surname of the contact
        /// </summary>
        /// <exemple>Dring</exemple>
        public string SurName { get; set; }
        /// <summary>
        /// The postal adress of the contact
        /// </summary>
        /// <exemple>1 rue du metal</exemple>
        public string PostalAddress { get; set; }
        /// <summary>
        /// The postal code 
        /// </summary>
        /// <exemple>33000</exemple>
        public string PostalCode { get; set; }
        /// <summary>
        /// city
        /// </summary>
        /// <exemple>Bordeaux</exemple>
        public string City { get; set; }
        /// <summary>
        /// country
        /// </summary>
        /// <exemple>France</exemple>
        public string Country { get; set; }
        /// <summary>
        /// The email adress of the contact
        /// </summary>
        /// <exemple>sarahcroche@gmail.com</exemple>
        public string Email { get; set; }
        /// <summary>
        /// The password whitch use the user to log on
        /// </summary>
        /// <exemple>sarah123</exemple>
        public string Password { get; set; }
        /// <summary>
        /// if the contact is admin of the application
        /// </summary>
        /// <exemple>True</exemple>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// the profil picture of the user
        /// </summary>
        /// <exemple>htttpmypicture</exemple>
        public string UrlPicture { get; set; }
       // public DateTime UpdateDate { get; set; }

    }
}
