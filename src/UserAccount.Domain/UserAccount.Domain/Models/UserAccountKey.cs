using System;
using System.Collections.Generic;
using System.Text;
using UserAccount.Domain.Common.Model;

namespace UserAccount.Domain.UserAccount.Domain.Models
{
    public class UserAccountKey : ValueObject
    {
        public long UserAccountId { get; private set; }

        public UserAccountKey()
        {

        }

        public UserAccountKey(long UserAccountId)
        {
            this.UserAccountId = UserAccountId;
        }

        protected override bool EqualsCore(ValueObject other)
        {
            if (other is UserAccountKey key)
            {
                return key.UserAccountId == this.UserAccountId;
            }
            return false;
        }

        ///<inheritdoc cref="object"/>
        protected override int GetHashCodeCore()
        {
            return this.UserAccountId.GetHashCode();
        }
    }
}
