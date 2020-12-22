using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Infrastructure;

namespace UserAccount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccountController : ControllerBase
    {

        private readonly ILogger<UserAccountController> _logger;

        private readonly UserAccountService _userAccountService;

        public UserAccountController(ILogger<UserAccountController> logger, IUserAccountProvider userAccountProvider)
        {
            _logger = logger;
            if(userAccountProvider != null)
            {
                _userAccountService = new UserAccountService(_logger, userAccountProvider);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       // [HttpGet]

    }
}
