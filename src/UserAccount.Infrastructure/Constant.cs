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
                public static readonly string CreateUserAccount = "CreateUserAccount";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string GetUserAccountById = "GetUserAccountById";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string GetUserAccountByLogin = "GetUserAccountByLogin";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string SetUserAccount = "SetUserAccount";
                /// <summary>
                /// 
                /// </summary>
                public static readonly string DeleteUserAccount = "DeleteUserAccount";
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
