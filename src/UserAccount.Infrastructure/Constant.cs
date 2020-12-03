using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccount.Infrastructure
{
    public static class Constant
    {
        public static class StoredProcedure
        {
            public static class UserAccount
            {
                /// <summary>
                /// 
                /// </summary>
                public static readonly string CreateUserAccount = "createUserAccount";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string GetUserAccount = "createUserAccount";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string SetUserAccount = "createUserAccount";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string DeleteUserAccount = "createUserAccount";
            }

        }

        public static class Cache
        {
            /// <summary>
            /// 
            /// </summary>
            public static readonly string UserAccountKey = "UserAccount_{0}";
            /// <summary>
            /// 
            /// </summary>
            public static readonly long UserAccountMediumSize = 200;
        }
    }
}
