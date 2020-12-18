using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Infrastructure;

namespace UserAccount.Api
{
    public class UserAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        private IUserAccountProvider _userAccountProvider;

        /// <summary>
        /// 
        /// </summary>
        private ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userAccountProvider"></param>
        public UserAccountService(ILogger logger, IUserAccountProvider userAccountProvider)
        {
            _userAccountProvider = userAccountProvider ?? throw new ArgumentNullException(nameof(userAccountProvider));
            _logger = logger;
        }
    }
}
