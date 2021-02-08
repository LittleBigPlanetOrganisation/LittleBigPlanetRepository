using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Api.Assemblers;
using UserAccount.Api.ViewModels;
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
        [HttpGet]
        [Route ("{idUser}")]
         public async Task<IActionResult> GetuserAccountById([FromRoute][Required] long idUser)
         {
            try
            {
                var result = await _userAccountService.GetUserAccountListById(idUser).ConfigureAwait(false);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result.ToGetUserAccountListById());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="surName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{surName}/{password}")]
        public async Task<IActionResult> GetUserAccountByLogin([FromRoute][Required] string surName, [FromRoute][Required]
        string password)
        {
            try
            {
                var result = await _userAccountService.GetUserAccountListByLogin(surName, password).ConfigureAwait(false);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result.ToGetUserAccountByLogin());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserAccountViewModel userAccountViewModel)
        {
            try
            {
                await _userAccountService.PostUserWhithAllParam(userAccountViewModel).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUserAccount([FromBody] UserAccountViewModel userAccountViewModel)
        {
            try
            {
                await _userAccountService.UpdateUserAccount(userAccountViewModel).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpDelete("{idUser}")]
        public async Task<IActionResult> DeleteUseraccount([FromRoute] long idUser)
        {
            try
            {
                if(idUser > 0)
                {
                    await _userAccountService.DeleteUserAccount(idUser).ConfigureAwait(false);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}   

