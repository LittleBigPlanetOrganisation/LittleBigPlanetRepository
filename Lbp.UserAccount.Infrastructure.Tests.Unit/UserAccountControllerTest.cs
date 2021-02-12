using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAccount.Api.Controllers;
using UserAccount.Api.ViewModels;
using UserAccount.Domain.UserAccount.Domain.Models;
using UserAccount.Infrastructure;
using Xunit;

namespace Lbp.UserAccount.Infrastructure.Tests.Unit
{
    public class UserAccountControllerTest
    {
        /// <summary>
        /// Mocked provider
        /// </summary>
        private readonly Mock<IUserAccountProvider> _mockUserAccountProvider;
        /// <summary>
        /// Micked logger
        /// </summary>
        private readonly Mock<ILogger<UserAccountController>> _mockLogger;
        /// <summary>
        /// controller
        /// </summary>
        private readonly UserAccountController _controller;

        public UserAccountControllerTest()
        {
            _mockUserAccountProvider = new Mock<IUserAccountProvider>();
            _mockLogger = new Mock<ILogger<UserAccountController>>();
            _controller = new UserAccountController(_mockLogger.Object, _mockUserAccountProvider.Object);
        }

        [Fact]
        public async void GetUserAccountById_Return_NotFound()
        {           
            _mockUserAccountProvider.Setup(x => x.GetUserAccountListById(It.IsAny<long>())).Returns(Task.FromResult((IEnumerable<UserAccountAllParam>)null));

            //Act
            var result = await _controller.GetuserAccountById(1).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetUserAccountById_Return_500()
        {
            _mockUserAccountProvider.Setup(x => x.GetUserAccountListById(It.IsAny<long>())).Throws(new Exception());

            //Act
            var result = await _controller.GetuserAccountById(1).ConfigureAwait(false);

            Assert.NotNull(result);
            var response = (ObjectResult)result;
            Assert.Equal(500, response.StatusCode);
        }


    }
}
