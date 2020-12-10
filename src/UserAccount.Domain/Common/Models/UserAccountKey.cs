using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccount.Domain.Models
{
    public class UserAccountKey : ValueObjet
    {
        public long UserAccountId { get; private set; }

        public UserAccountKey()
        {

        }

        public UserAccountKey(long UserAccountId)
        {
            this.UserAccountId = UserAccountId;
        }
    }
}
