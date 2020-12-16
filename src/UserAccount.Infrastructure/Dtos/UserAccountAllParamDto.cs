using System;
using System.Collections.Generic;
using System.Text;
using UserAccount.Domain.UserAccount.Domain.Models;

namespace UserAccount.Infrastructure.Dtos
{
    public class UserAccountAllParamDto
    {
        public long idUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
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

    }
}