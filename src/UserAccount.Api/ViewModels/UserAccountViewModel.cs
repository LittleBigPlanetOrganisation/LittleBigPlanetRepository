using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Domain.UserAccount.Domain.Models;

namespace UserAccount.Api.ViewModels
{
    public class UserAccountViewModel
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

    }
}
