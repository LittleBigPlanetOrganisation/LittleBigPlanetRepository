using Microsoft.AspNetCore.Http;
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
    [ApiController] // valide le model
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
        /// Get user by his Id
        /// </summary>
        /// <param name="idUser"></param>
        /// <response code="200">Success, the user's params has been returned</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>User by his identifier </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        //[Route("{idUser}")]
        [Route ("{idUser}", Name = "GetuserAccountById")]
         public async Task<IActionResult> GetuserAccountById([FromRoute][Required] long idUser)
         {
            try
            {
                var result = await _userAccountService.GetUserAccountListById(idUser).ConfigureAwait(false);
                if (result == null)
                {
                    return NotFound();
                }
                //return Ok(result.ToGetUserAccountListById());
                return new ObjectResult(result.ToGetUserAccountListById());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
         }
        /// <summary>
        /// Get User Params by surname and password
        /// </summary>
        /// <param name="surName"></param>
        /// <param name="password"></param>
        /// <response code="200">Success, the user's params has been returned</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>User by his surname and password</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Create a new user with all params
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <response code="201">The new user has been Successfully created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>A new user with all params</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserAccountViewModel userAccountViewModel)
        {
            try
            {
                await _userAccountService.PostUserWhithAllParam(userAccountViewModel).ConfigureAwait(false);
                //return Ok();
               return CreatedAtRoute("GetuserAccountById", new UserAccountViewModel { IdUser = userAccountViewModel.IdUser }, userAccountViewModel);
            }
            // nameOf : passer le parametre de la route sans ecrire une chaine de caractere en dur
            catch (Exception e)
            {
                return StatusCode(500, e);
                //test
            }
        }

        /// <summary>
        /// Update the user's params
        /// </summary>
        /// <param name="userAccountViewModel"></param>
        /// <response code="200">Success, the user's params has been updated</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>the user's new params</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Delete all user's params
        /// </summary>
        /// <param name="idUser"></param>
        /// <response code="200">Success, the user's params has been deleted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>void</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
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

